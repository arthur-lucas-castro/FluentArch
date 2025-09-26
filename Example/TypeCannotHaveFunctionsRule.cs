using FluentArch.Conditions.Interfaces;
using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class TypeCannotHaveFunctionsRule : ICustomRule
    {
        public bool DefineCustomRule(TypeEntityDto type)
        {
            return !type.Functions.Any();
        }
    }
}
