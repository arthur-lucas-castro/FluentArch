using FluentArch.DTO;
using FluentArch.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FluentArch.ASTs
{
    public static class FunctionVisitor
    {
        //TODO: traduzir para ingles
        public static FunctionEntityDto VisitarFuncao(MethodDeclarationSyntax method, SemanticModel semanticModel)
        {
            var functionDto = new FunctionEntityDto();
            functionDto.IsConstructor = false;
            var symbol = semanticModel.GetDeclaredSymbol(method);
            if (symbol is IMethodSymbol namedTypeSymbol)
            {
                functionDto.Name = namedTypeSymbol.Name;
                functionDto.Namespace = namedTypeSymbol.ContainingNamespace.ToString();
                functionDto.Location = FormatarStringUtils.FormatarLocalizacaoLinha(method.GetLocation());
            }
            var body = method.Body;
            var parametros = method.ParameterList.Parameters.Where(p => !VisitorUtils.EhTipoPrimitivo(p.Type!.ToString()));

            var acessibilidade = method.Modifiers;
            functionDto.Parameters.AddRange(PreencherParametros(semanticModel, parametros));
            functionDto.LocalTypes.AddRange(PreencherTiposLocais(method, semanticModel));
            functionDto.Access.AddRange(PreencherAcessos(method, semanticModel));
            functionDto.Creations.AddRange(PreencherCriacoes(method, semanticModel));
            functionDto.Throws.AddRange(PreencherLancamentos(method, semanticModel));
            //EntityDto retornoEntity = PreencherRetornos(method, semanticModel, symbol);

            functionDto.AccessibilityLevel = method.Modifiers.Where(mod => mod.IsKind(SyntaxKind.PublicKeyword) ||
                              mod.IsKind(SyntaxKind.PrivateKeyword) ||
                              mod.IsKind(SyntaxKind.ProtectedKeyword) ||
                              mod.IsKind(SyntaxKind.InternalKeyword))
                .Select(mod => mod.Text)
                .FirstOrDefault();
            return functionDto;
        }

        //TODO: Fazer caso do retorno, considerar dictionarys e mais de um retonor (typoe1, type2)
        private static List<EntityDto> PreencherRetornos(MethodDeclarationSyntax method, SemanticModel semanticModel)
        {
            var retorno = method.ReturnType;
            var symbol = semanticModel.GetDeclaredSymbol(retorno);
            var listaRetornos = new List<EntityDto>();
            if (symbol is IParameterSymbol parameterSymbol)
            {
                listaRetornos.Add(new EntityDto
                {
                    Name = parameterSymbol.Name,
                    Namespace = parameterSymbol.Type.ContainingNamespace.ToString(),
                    Location = FormatarStringUtils.FormatarLocalizacaoLinha(retorno.GetLocation())
                });
            }

            return listaRetornos;
        }

        public static FunctionEntityDto VisitarConstrutor(ConstructorDeclarationSyntax construtor, SemanticModel semanticModel)
        {
            var functionDto = new FunctionEntityDto();
            functionDto.IsConstructor = true;

            var symbol = semanticModel.GetDeclaredSymbol(construtor);
            if (symbol is IMethodSymbol namedTypeSymbol)
            {
                functionDto.Namespace = namedTypeSymbol.ContainingNamespace.ToString();
                functionDto.Location = FormatarStringUtils.FormatarLocalizacaoLinha(construtor.GetLocation());
            }
            var body = construtor.Body;
            var parametros = construtor.ParameterList.Parameters.Where(p => !VisitorUtils.EhTipoPrimitivo(p.Type!.ToString()));

            var acessibilidade = construtor.Modifiers;
            functionDto.Parameters.AddRange(PreencherParametros(semanticModel, parametros));
            functionDto.LocalTypes.AddRange(PreencherTiposLocais(construtor, semanticModel));
            functionDto.Access.AddRange(PreencherAcessos(construtor, semanticModel));
            functionDto.Creations.AddRange(PreencherCriacoes(construtor, semanticModel));
            functionDto.Throws.AddRange(PreencherLancamentos(construtor, semanticModel));
            functionDto.Bloco = construtor.Body;

            functionDto.AccessibilityLevel = construtor.Modifiers.Where(mod => mod.IsKind(SyntaxKind.PublicKeyword) ||
                              mod.IsKind(SyntaxKind.PrivateKeyword) ||
                              mod.IsKind(SyntaxKind.ProtectedKeyword) ||
                              mod.IsKind(SyntaxKind.InternalKeyword))
                .Select(mod => mod.Text)
                .FirstOrDefault();
            return functionDto;
        }

        private static List<EntityDto> PreencherLancamentos(SyntaxNode method, SemanticModel semanticModel)
        {
            var throwStatements = method.DescendantNodes()
             .OfType<ThrowStatementSyntax>();

            var listaExcecao = new List<EntityDto>();
            foreach (var throwStatement in throwStatements)
            {
                var throwExpression = throwStatement.Expression;
                if (throwExpression is null)
                {
                    continue;
                }
                var typeInfo2 = semanticModel.GetTypeInfo(throwStatement.Expression!);

                var type = typeInfo2.Type;
                listaExcecao.Add(new EntityDto
                {
                    Name = type!.Name,
                    Namespace = type.ContainingNamespace.ToString(),
                    Location = FormatarStringUtils.FormatarLocalizacaoLinha(throwStatement.GetLocation())
                });
            }
            return listaExcecao;
        }

        private static List<EntityDto> PreencherCriacoes(SyntaxNode method, SemanticModel semanticModel)
        {
            var objectCreationDeclarations = method.DescendantNodes()
                         .OfType<ObjectCreationExpressionSyntax>();
            var listaCriacoes = new List<EntityDto>();
            foreach (var objectCreation in objectCreationDeclarations)
            {
                var symbol2 = semanticModel.GetSymbolInfo(objectCreation).Symbol;
                if (symbol2 is not null)
                {
                    if (VisitorUtils.EhTipoPrimitivo(symbol2.ContainingType.Name))
                    {
                        continue;
                    }

                    listaCriacoes.Add(new EntityDto
                    {
                        Name = symbol2.ContainingType.Name,
                        Namespace = symbol2.ContainingNamespace.ToString(),
                        Location = FormatarStringUtils.FormatarLocalizacaoLinha(objectCreation.GetLocation())
                    });

                }
            }
            return listaCriacoes;
        }

        private static List<EntityDto> PreencherAcessos(SyntaxNode method, SemanticModel semanticModel)
        {
            var memberAcessDeclarations = method.DescendantNodes()
              .OfType<MemberAccessExpressionSyntax>();
            var listaAcessos = new List<EntityDto>();
            foreach (var memberAcess in memberAcessDeclarations)
            {
                var symbol2 = semanticModel.GetSymbolInfo(memberAcess).Symbol;
                if (symbol2 is not null)
                {
                    if (VisitorUtils.EhTipoPrimitivo(symbol2.ContainingType.Name))
                    {
                        continue;
                    }

                    listaAcessos.Add(new EntityDto
                    {
                        Name = symbol2.ContainingType.Name,
                        Namespace = symbol2.ContainingNamespace.ToString(),
                        Location = FormatarStringUtils.FormatarLocalizacaoLinha(memberAcess.GetLocation())
                    });

                }
            }
            return listaAcessos;
        }

        private static List<EntityDto> PreencherTiposLocais(SyntaxNode method, SemanticModel semanticModel)
        {
            var variableDeclarations = method.DescendantNodes()
               .OfType<VariableDeclaratorSyntax>()
               .Where(v => v.Parent is VariableDeclarationSyntax);

            var listaDeclaracoes = new List<EntityDto>();
            foreach (var variableDeclaration in variableDeclarations)
            {
                var declaredSymbol = semanticModel.GetDeclaredSymbol(variableDeclaration);

                if (declaredSymbol is ILocalSymbol localSymbol)
                {
                    if (VisitorUtils.EhTipoPrimitivo(localSymbol.Type.Name) || VisitorUtils.EhTipoPrimitivo(localSymbol.Type.ToString()))
                    {
                        continue;
                    }

                    listaDeclaracoes.Add(new EntityDto
                    {
                        Name = localSymbol.Type.Name.Equals(string.Empty) ? localSymbol.Type.ToString() : localSymbol.Type.Name,
                        Namespace = localSymbol.Type.ContainingNamespace is null ? string.Empty: localSymbol.Type.ContainingNamespace.ToString(),
                        Location = FormatarStringUtils.FormatarLocalizacaoLinha(variableDeclaration.GetLocation())
                    });
                }
            }
            return listaDeclaracoes;
        }

        private static List<EntityDto> PreencherParametros(SemanticModel semanticModel, IEnumerable<ParameterSyntax> parametros)
        {
            var listaEntidades = new List<EntityDto>();
            foreach (var parametro in parametros)
            {
                var symbol1 = semanticModel.GetDeclaredSymbol(parametro);
                if (symbol1 is IParameterSymbol parameterSymbol)
                {
                    listaEntidades.Add(new EntityDto
                    {
                        Name = parameterSymbol.Type.Name,
                        Namespace = parameterSymbol.Type.ContainingNamespace.ToString(),
                        Location = FormatarStringUtils.FormatarLocalizacaoLinha(parametro.GetLocation())
                    });
                }
            }
            return listaEntidades;
        }
    }
}
