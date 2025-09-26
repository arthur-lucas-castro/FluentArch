using Example;
using FluentArch.Arch;
using FluentArch.ASTs;
using FluentArch.Layers;
using FluentArch.Result;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
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

                var arch = Architecture.Build(solution);

                ILayer camadaApi = arch.All().ResideInNamespace("N_Tier.API.*").As("Api layer");
                ILayer camadaUnitTest = arch.All().ResideInNamespace("N_Tier.Application.UnitTests.*");
                ILayer camadaIntegrationTest = arch.All().ResideInNamespace("N_Tier.Api.IntegrationTests.*");
                ILayer camadaApplication = arch.All().ResideInNamespace("N_Tier.Application.*").As("Application layer");
                ILayer camadaDataAccess = arch.All().ResideInNamespace("N_Tier.DataAccess.*").As("DataAccess layer");
                ILayer camadaCore = arch.All().ResideInNamespace("N_Tier.Core.*").As("Core layer");
                ILayer camadaShared = arch.All().ResideInNamespace("N_Tier.Shared.*").As("Shared layer");
                ILayer camadaModels = arch.All().ResideInNamespace("N_Tier.Application.Models.*").As("Models layer");

                camadaApi.And(camadaUnitTest).And(camadaIntegrationTest).OnlyCan().Depend(camadaApplication);
                camadaApplication.OnlyCan().Depend(camadaDataAccess);
                camadaDataAccess.Cannot().Depend(camadaApplication).And().Cannot().Depend(camadaApi);
                camadaCore.Cannot().Depend(camadaDataAccess);
                camadaApplication.And().ResideInNamespace("N_Tier.Application.Services.*").Cannot().Create(camadaDataAccess);
                camadaApplication.And().ResideInNamespace("N_Tier.Application.Exceptions").Must().Extends("System.Exception");
                camadaModels.UseCustomRule(new TypeCannotHaveFunctionsRule()).Check();
                var listaResultados = arch.Check().ToList();

                Console.WriteLine("Results: ");
                var index = 1;
                foreach (var item in listaResultados.Where(x => !x.IsSuccessful))
                {

                    foreach (var violacao in item.Violations)
                    {
                        Console.WriteLine($"{index}. {violacao.ViolationReason}\n");

                        index++;
                    }
                }

            }
        }


    }
}

