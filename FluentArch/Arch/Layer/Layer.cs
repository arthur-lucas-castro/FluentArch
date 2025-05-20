using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch.Layer
{
    public class Layer : ILayer
    {
        private string _name = string.Empty;
        public readonly IEnumerable<TypeEntityDto> _classes;
        private ICompleteRule _builder;
        private IRules _rules;

        public Layer(IEnumerable<TypeEntityDto> classes, ICompleteRule builder)
        {
            _classes = classes;
            _builder = builder;
            _rules = new Rules.Rules(builder);
        }

        public List<TypeEntityDto> GetTypes()
        {
            return _classes.ToList();
        }

        public ILayer As(string name)
        {
            this._name = name;
            return this;
        }
        public IFilters And()
        {
            var builder = new RuleBuilder();
            builder.UpdateTypes(_classes.ToList());

            return new RuleFilter(builder);
        }

        public IMustRules Must()
        {

            return _rules.Must();
        }

        public ICanOnlyRules CanOnly()
        {
            return _rules.CanOnly();
        }

        public ICannotRules Cannot()
        {
            return _rules.Cannot();
        }

        public IOnlyCanRules OnlyCan()
        {
            return _rules.OnlyCan();
        }

        public IConcatRules UseCustomRule(ICustomRule customRule)
        {
            return new CustomRule(_builder).ExecuteCustomRule(customRule);
        }

        public string GetName()
        {
            return _name;
        }
    }
}
