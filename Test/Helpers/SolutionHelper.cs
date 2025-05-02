using FluentArch.ASTs;
using FluentArch.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public static class SolutionHelper
    {
        public static List<TypeEntityDto> ObterDadosDaClasse(List<string> classes)
        {
            var workspace = new AdhocWorkspace();

            var solutionInfo = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default);
            var solution = workspace.AddSolution(solutionInfo);

            // Cria projeto A
            var projectAId = ProjectId.CreateNewId("ProjetoA");
            var projectA = ProjectInfo.Create(
                projectAId,
                VersionStamp.Default,
                name: "ProjetoA",
                assemblyName: "ProjetoA",
                language: LanguageNames.CSharp
            );

            solution = solution.AddProject(projectA);
            var randNum = new Random();

            foreach (var classe in classes)
            {
                solution = solution.AddDocument(
                    DocumentId.CreateNewId(projectAId),
                    $"Class{randNum.Next()}.cs",
                    SourceText.From(classe));
            }

            workspace.TryApplyChanges(solution);

            var classVisitor = new ClassVisitor();

            var dadosClasse = new List<TypeEntityDto>();
            foreach (var project in solution.Projects)
            {
                dadosClasse.AddRange(classVisitor.ObterDadosDasClasses(project));
            }
            return dadosClasse;
        }

        public static Solution MontarSolution(List<string> classes)
        {
            var workspace = new AdhocWorkspace();

            var solutionInfo = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default);
            var solution = workspace.AddSolution(solutionInfo);

            // Cria projeto A
            var projectAId = ProjectId.CreateNewId("ProjetoA");
            var projectA = ProjectInfo.Create(
                projectAId,
                VersionStamp.Default,
                name: "ProjetoA",
                assemblyName: "ProjetoA",
                language: LanguageNames.CSharp
            );

            solution = solution.AddProject(projectA);
            var randNum = new Random();

            foreach (var classe in classes)
            {
                solution = solution.AddDocument(
                    DocumentId.CreateNewId(projectAId),
                    $"Class{randNum.Next()}.cs",
                    SourceText.From(classe));
            }

            workspace.TryApplyChanges(solution);

           return solution;
        }
    }
}
