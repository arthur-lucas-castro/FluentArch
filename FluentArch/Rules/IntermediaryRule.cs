using FluentArch.DTO;
using FluentArch.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class IntermediaryRule
    {
        private readonly IEnumerable<ClassEntityDto> _classes;
        private readonly List<PreResult> _preResults;
        public IntermediaryRule(IEnumerable<ClassEntityDto> classes, List<PreResult> preResults) 
        {
            _classes = classes;
            _preResults = preResults;
        }

        public Rules And() 
        {
            return new Rules(_classes, _preResults);
        }

        public void GetResult()
        {

        }
    }
}
