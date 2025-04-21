using FluentArch.DTO;
using FluentArch.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentArch.ASTs
{
    public class ClassVisitor
    {
        public ClassVisitor() { }

        public List<ClassEntityDto> ObterDadosDasClasses(Project project)
        {
            var dadosClasse = new List<ClassEntityDto>();
            var compilation = project.GetCompilationAsync().Result;

            var trees = compilation!.SyntaxTrees.Where(st => (!st.FilePath.Contains(@"\obj\")));

            foreach (var tree in trees)
            {
                var semanticModel = compilation.GetSemanticModel(tree);
                var classes = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>();
                //TODO: Analisar interfaces
                foreach (var classDeclaration in classes)
                {
                    var classeAnalisada = new ClassEntityDto();
                    classeAnalisada.Nome = classDeclaration.Identifier.Text;
                    var symbol = semanticModel.GetDeclaredSymbol(classDeclaration);
                    if (symbol is INamedTypeSymbol namedTypeSymbol)
                    {
                        classeAnalisada.Namespace = symbol.ContainingNamespace.ToString();
                    }
                    PreencherBase(classDeclaration, classeAnalisada, semanticModel);
                    PreencherAtributosDaClasse(classDeclaration, classeAnalisada, semanticModel);
                    PreencherFuncoes(classDeclaration, classeAnalisada, semanticModel);
                    dadosClasse.Add(classeAnalisada);
                }
            }

            return dadosClasse;
        }
        private static void PreencherBase(ClassDeclarationSyntax classDeclaration, ClassEntityDto dadosDaClasse, SemanticModel semanticModel)
        {
            if (classDeclaration.BaseList is null)
            {
                return;
            }

            var baseTypes = classDeclaration.BaseList.Types.Select(t => t.Type);

            foreach (var baseType in baseTypes)
            {
                var symbol = semanticModel.GetSymbolInfo(baseType).Symbol;

                if (symbol is INamedTypeSymbol namedTypeSymbol)
                {
                    if (namedTypeSymbol.TypeKind == TypeKind.Class)
                    {
                        dadosDaClasse.Heranca = new EntityDto
                        {
                            Nome = namedTypeSymbol.Name,
                            Namespace = namedTypeSymbol.ContainingNamespace.ToString(),
                            Local = FormatarStringUtils.FormatarLocalizacaoLinha(baseType.GetLocation()),
                        };
                        continue;
                    }
                    
                    if (namedTypeSymbol.TypeKind == TypeKind.Interface)
                    {
                        dadosDaClasse.Interfaces.Add(new EntityDto
                        {
                            Nome = namedTypeSymbol.Name,
                            Namespace = namedTypeSymbol.ContainingNamespace.ToString(),
                            Local = FormatarStringUtils.FormatarLocalizacaoLinha(baseType.GetLocation()),
                        });
                    }
                }

            }


        }

        //TODO: validar PropertyDeclarationSyntax;
        private static void PreencherAtributosDaClasse(ClassDeclarationSyntax classDeclaration, ClassEntityDto dadosDaClasse, SemanticModel semanticModel)
        {
            var attributes = classDeclaration.Members.OfType<PropertyDeclarationSyntax>().Where(p => !VisitorUtils.EhTipoPrimitivo(p.Type.ToString()));

            foreach (var attribute in attributes)
            {
                var symbol = semanticModel.GetSymbolInfo(attribute.Type).Symbol;

                if (symbol is null)
                {
                    continue;
                }

                dadosDaClasse.Propriedades.Add(new EntityDto
                {
                    Nome = symbol.Name,
                    Namespace = symbol.ContainingNamespace.ToString(),
                    Local = FormatarStringUtils.FormatarLocalizacaoLinha(attribute.GetLocation())
                });
            }

            var fields = classDeclaration.Members
                                          .OfType<FieldDeclarationSyntax>()
                                          .Where(p =>
                                          {
                                              return !VisitorUtils.EhTipoPrimitivo(p.Declaration.Type.ToString());
                                          });
            foreach (var field in fields)
            {
                var typeSyntax = field.Declaration.Type;
                var symbol = semanticModel.GetSymbolInfo(typeSyntax).Symbol;

                if (symbol is null)
                {
                    return;
                }

                dadosDaClasse.Propriedades.Add(new EntityDto
                {
                    Namespace = symbol.ContainingNamespace.ToString(),
                    Nome = symbol.Name,
                    Local = FormatarStringUtils.FormatarLocalizacaoLinha(field.GetLocation()),
                });

            }
        }

        private static void PreencherFuncoes(ClassDeclarationSyntax classDeclaration, ClassEntityDto dadosDaClasse, SemanticModel semanticModel)
        {
            var methods = classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var method in methods)
            {
                dadosDaClasse.Funcoes.Add(FunctionVisitor.VisitarFuncao(method, semanticModel));
            }
            var constructors = classDeclaration.DescendantNodes().OfType<ConstructorDeclarationSyntax>();
            foreach (var constructor in constructors)
            {
                dadosDaClasse.Funcoes.Add(FunctionVisitor.VisitarConstrutor(constructor, semanticModel));
            }

        }

       
    }
}
