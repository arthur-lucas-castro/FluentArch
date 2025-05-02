using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class RuleBuilder : ICompleteRule
    {
        private CompleteRule _completeRule;
        public RuleBuilder() 
        {
            _completeRule = new CompleteRule();
        }
        public RuleBuilder(List<TypeEntityDto> types)
        {
            _completeRule = new CompleteRule();
            _completeRule.UpdateTypes(types);
        }
        public void AddResults(ConditionResult conditionResult)
        {
            _completeRule.AddResults(conditionResult);
        }

        public List<ConditionResult> GetResults()
        {
            return _completeRule.GetResults();
        }

        public List<TypeEntityDto> GetTypes()
        {
            return _completeRule.GetTypes();
        }

        public void UpdateTypes(List<TypeEntityDto> newTypes)
        {
            _completeRule.UpdateTypes(newTypes);
        }
    }
}
