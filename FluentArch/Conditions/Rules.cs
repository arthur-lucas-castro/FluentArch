using FluentArch.Result;
using FluentArch.Conditions.Interfaces;
using FluentArch.Conditions.Interfaces.Restrictions;
using FluentArch.Conditions.Restrictions;


namespace FluentArch.Conditions
{
    public class Rules : IRules, IConcatRules
    {

        private IRuleBuilder _builder;

        public Rules(IRuleBuilder builder)
        {
            _builder = builder;
        }
        
        public IRestrictions Must()
        {
            return new MustRules(_builder);
        }

        public IRestrictions Cannot()
        {
            return new CannotRules(_builder);
        }
        public IRestrictions OnlyCan()
        {
            return new OnlyCanRules(_builder);
        }
        public IRestrictions CanOnly()
        {
            return new CanOnlyRules(_builder);
        }

        public IConcatRules UseCustomRule(ICustomRule customRule)
        {
            return new CustomRule(_builder).ExecuteCustomRule(customRule);
        }

        public IRules And()
        {
            return this;
        }

        public ConditionResult Check()
        {
            var allResults = _builder.GetResults();

            return new ConditionResult(!allResults.Any(result => !result.IsSuccessful), allResults.SelectMany(result => result.Violations));
        }
    }
}
