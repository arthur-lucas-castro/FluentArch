﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Utils
{
    public static class VisitorUtils
    {
        public static bool EhTipoPrimitivo(string tipo)
        {
            //TODO: Criar exclusionList
            var tipoPrimitivo = new HashSet<string>
            {
                "int", "int32", "String", "string", "float", "double", "decimal", "bool", "char", "byte", "short", "long", "object", "var", "string[]", "byte[]"
            };
            return tipoPrimitivo.Contains(tipo);
        }
    }
}
