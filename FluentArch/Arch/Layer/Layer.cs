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
            return _builder.GetTypes();
        }

        public IFilters And()
        {
            return new RuleFilter(_builder);
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
    }
}
