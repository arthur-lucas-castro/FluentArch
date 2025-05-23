using FluentArch.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules.Interfaces
{
    public interface IConcatRules
    {
        IRules And();
        ConditionResult Check();
    }
}
