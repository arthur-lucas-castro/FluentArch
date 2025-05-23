using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules;
using FluentArch.Filters;
using FluentArch.Rules.Interfaces;
using FluentArch.Arch;
using FluentArch.Rules.Interfaces.Restrictions;
namespace FluentArch.Layers
{
    public class Layer : ILayer
    {
        private string _name = string.Empty;
        public readonly IEnumerable<TypeEntityDto> _types;


        public Layer(IEnumerable<TypeEntityDto> types)
        {
            _types = types;
        }

        public List<TypeEntityDto> GetTypes()
        {
            return _types.ToList();
        }

        public ILayer As(string name)
        {
            _name = name;
            return this;
        }
        public IFilters And()
        {
            return new RuleFilter(_types.ToList());
        }

        public IRestrictions Must()
        {
            var builder = new RuleBuilder(_types);
            
            var rule = new Rules.Rules(builder);

            return rule.Must();
        }

        public IRestrictions CanOnly()
        {
            var builder = new RuleBuilder(_types);
            var rule = new Rules.Rules(builder);
            return rule.CanOnly();
        }

        public IRestrictions Cannot()
        {
            var builder = new RuleBuilder(_types);
            var rule = new Rules.Rules(builder);
            return rule.Cannot();
        }

        public IRestrictions OnlyCan()
        {
            var builder = new RuleBuilder(_types);
            var rule = new Rules.Rules(builder);
            return rule.OnlyCan();
        }

        public IConcatRules UseCustomRule(ICustomRule customRule)
        {
            var builder = new RuleBuilder(_types);
            return new CustomRule(builder).ExecuteCustomRule(customRule);
        }

        public string GetName()
        {
            return _name;
        }
    }
}
