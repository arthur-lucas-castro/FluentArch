using FluentArch.DTO;
using FluentArch.Result;

namespace FluentArch.Conditions.Interfaces
{
    public interface IRuleBuilder
    {
        void UpdateTypes(IEnumerable<TypeEntityDto> newTypes);
        void AddResults(ConditionResult conditionResult);
        List<TypeEntityDto> GetTypes();
        List<ConditionResult> GetResults();
    }
}
