using FluentArch.Arch;
using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using FluentArch.Utils;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class CustomRule
    {
        private ICompleteRule _builder;

        public CustomRule(ICompleteRule builder)
        {
            _builder = builder;
        }
        public IConcatRules ExecuteCustomRule(ICustomRule customRule)
        {
            foreach(var type in _builder.GetTypes())
            {
                var result = customRule.DefineCustomRule(type);
                if (!result)
                {     
                    var violacao = new ViolationDto 
                    { 
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Adapt<EntityDto>() },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CUSTOM_RULE, [type.Name])
                    };
                    _builder.AddResults(new ConditionResult(false, violacao));
                }          
                
            }

            return new Rules(_builder);
        }
    }
}
