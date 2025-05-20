using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules.Interfaces
{
    public interface ICustomRule
    {
        public bool DefineCustomRule(TypeEntityDto type);
    }
}
