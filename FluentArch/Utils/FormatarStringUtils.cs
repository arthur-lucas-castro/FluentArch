using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentArch.Utils
{
    public static class FormatarStringUtils
    {
        public static string FormatarLocalizacaoLinha(Location localizacao)
        {
            return $"{localizacao.GetLineSpan().Path} - Line: {localizacao.GetLineSpan().StartLinePosition.Line + 1}";
        }
    }
}
