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
                Name = parameterSymbol.Name,
                Namespace = parameterSymbol.Type.ContainingNamespace.ToString(),
                Location = FormatarStringUtils.FormatarLocalizacaoLinha(parametro.GetLocation())
            };
        }
    }
}
