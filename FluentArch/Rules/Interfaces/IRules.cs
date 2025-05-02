using FluentArch.Arch.Layer;
using FluentArch.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules.Interfaces
{
    public interface IRules
    {
        IMustRules Must();
        ICanOnlyRules CanOnly();
        ICannotRules Cannot();
        IOnlyCanRules OnlyCan();
    }
}
