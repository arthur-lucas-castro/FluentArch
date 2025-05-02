using FluentArch.DTO;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch.Layer
{
    public interface ILayer
    {
        public IFilters And();
        IMustRules Must();
        ICanOnlyRules CanOnly();
        ICannotRules Cannot();
        IOnlyCanRules OnlyCan();
        public List<TypeEntityDto> GetTypes();
    }
}
