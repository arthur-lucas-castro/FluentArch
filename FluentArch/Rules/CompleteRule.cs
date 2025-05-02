using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class CompleteRule
    {
        private List<TypeEntityDto> _classes = new List<TypeEntityDto>();
        private List<ConditionResult> _resultsOfRules = new List<ConditionResult>();

        public void UpdateTypes(List<TypeEntityDto> newTypes)
        {
            _classes = newTypes;
        }

        public void AddResults(ConditionResult conditionResult) 
        {
            _resultsOfRules.Add(conditionResult);
        }
        public List<TypeEntityDto> GetTypes()
        {
            return _classes;
        }
        public List<ConditionResult> GetResults()
        {
            return _resultsOfRules;
        }
    }
}
