using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.ASTs;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (MSBuildLocator.CanRegister)
            {
                MSBuildLocator.RegisterDefaults();
            }

            using (var workspace = MSBuildWorkspace.Create())
            {
                string path = args.First();
                if (path is null)
                {
                    throw new Exception("Path invalido");
                }
                var solution = await workspace.OpenSolutionAsync(path);

                var servicos = solution.Projects.FirstOrDefault(p => p.Name == "Servicos");

                var classVisitor = new ClassVisitor();

                var arch = Architecture.Build(solution);
                
                var controllerLayer = arch.Classes().That().ResideInNamespace("SimpleAPI.Controllers.*").DefineAsLayer();
                var repositoryLayer = arch.Classes().That().ResideInNamespace("Repositorios").DefineAsLayer();
                var servicoLayer = arch.Classes().That().ResideInNamespace("Servicos").DefineAsLayer();

                var teste = controllerLayer.Should().MustCreate(repositoryLayer);
                var teste2 = controllerLayer.Should().OnlyCanCreate("Repositorios");
                classVisitor.ObterDadosDasClasses(servicos);

            }
        }

       
    }
}
