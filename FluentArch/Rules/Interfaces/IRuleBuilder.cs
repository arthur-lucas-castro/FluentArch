﻿using FluentArch.DTO;
using FluentArch.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules.Interfaces
{
    public interface IRuleBuilder
    {
        void UpdateTypes(IEnumerable<TypeEntityDto> newTypes);
        void AddResults(ConditionResult conditionResult);
        List<TypeEntityDto> GetTypes();
        List<ConditionResult> GetResults();
    }
}
