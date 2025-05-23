using FluentArch.DTO;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Layer
{
    public interface ILayer
    {
        string GetName();
        ILayer As(string name);
        IFilters And();
        IMustRules Must();
        ICanOnlyRules CanOnly();
        ICannotRules Cannot();
        IOnlyCanRules OnlyCan();
        IConcatRules UseCustomRule(ICustomRule customRule);
        public List<TypeEntityDto> GetTypes();
    }
}
