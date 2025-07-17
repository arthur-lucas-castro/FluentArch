using FluentArch.Arch;
using FluentArch.DTO;
using FluentArch.Result;

namespace FluentArch.Conditions
{
    public class ArchRule
    {
        private List<TypeEntityDto> _classes = new List<TypeEntityDto>();
        private List<ConditionResult> _resultsOfRules = new List<ConditionResult>();

        public ArchRule(List<TypeEntityDto> types)
        {
            _classes = types;
        }

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
