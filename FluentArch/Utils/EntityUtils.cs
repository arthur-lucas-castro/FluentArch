using FluentArch.DTO;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Utils
{
    public static class EntityUtils
    {
        private static EntityDto PreencherEntidade(SyntaxNode parametro, IParameterSymbol parameterSymbol)
        {
            return new EntityDto
            {
                Nome = parameterSymbol.Name,
                Namespace = parameterSymbol.Type.ContainingNamespace.ToString(),
                Local = FormatarStringUtils.FormatarLocalizacaoLinha(parametro.GetLocation())
            };
        }
    }
}
