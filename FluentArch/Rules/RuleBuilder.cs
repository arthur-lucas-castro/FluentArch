using FluentArch.Arch;
using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using System.Data;

namespace FluentArch.Rules
{
    public class RuleBuilder : IRuleBuilder
    {
        private ArchRule _archRule;

        public RuleBuilder(IEnumerable<TypeEntityDto> types)
        {
            _archRule = new ArchRule(types.ToList());
            Architecture.AddRule(_archRule);
        }
        
        public void AddResults(ConditionResult conditionResult)
        {
            _archRule.AddResults(conditionResult);
        }

        public List<ConditionResult> GetResults()
        {
            return _archRule.GetResults();
        }

        public List<TypeEntityDto> GetTypes()
        {
            return _archRule.GetTypes();
        }

        public void UpdateTypes(IEnumerable<TypeEntityDto> newTypes)
        {
            _archRule.UpdateTypes(newTypes.ToList());
        }
    }
}
