using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;


namespace FluentArch.Rules
{
    public class Rules : IRules, IConcatRules
    {

        private ICompleteRule _builder;

        public Rules(ICompleteRule builder)
        {
            _builder = builder;
        }
        
        public IMustRules Must()
        {
            return new MustRules(_builder);
        }

        public ICannotRules Cannot()
        {
            return new CannotRules(_builder);
        }
        public IOnlyCanRules OnlyCan()
        {
            return new OnlyCanRules(_builder);
        }
        public ICanOnlyRules CanOnly()
        {
            return new CanOnlyRules(_builder);
        }

        #region Custom Rule
        public IConcatRules UseCustomRule(ICustomRule customRule)
        {
            return new CustomRule(_builder).ExecuteCustomRule(customRule);
        }

        public IRules And()
        {
            return this;
        }
        #endregion

        public ConditionResult GetResult()
        {
            var allResults = _builder.GetResults();

            return new ConditionResult(!allResults.Any(result => !result.IsSuccessful), allResults.SelectMany(result => result.Violations));
        }
    }
}
