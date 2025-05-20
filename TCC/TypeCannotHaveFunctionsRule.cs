using FluentArch.DTO;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC
{
    public class TypeCannotHaveFunctionsRule : ICustomRule
    {
        public bool DefineCustomRule(TypeEntityDto type)
        {
            return !type.Functions.Any();
        }
    }
}
