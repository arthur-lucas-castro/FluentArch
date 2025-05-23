using FluentArch.Arch;
using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using FluentArch.Utils;
using Mapster;

namespace FluentArch.Rules
{
    public class CustomRule
    {
        private IRuleBuilder _builder;

        public CustomRule(IRuleBuilder builder)
        {
            _builder = builder;
        }
        public IConcatRules ExecuteCustomRule(ICustomRule customRule)
        {
            var violations = new List<ViolationDto>();
            foreach(var type in _builder.GetTypes())
            {
                var result = customRule.DefineCustomRule(type);
                if (!result)
                {     
                    var violation = new ViolationDto 
                    { 
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Adapt<EntityDto>() },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CUSTOM_RULE, [type.Name])
                    };
                    violations.Add(violation);
                }          
                
            }
            _builder.AddResults(new ConditionResult(!violations.Any(), violations));
            return new Rules(_builder);
        }
    }
}
