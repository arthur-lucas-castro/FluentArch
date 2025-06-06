﻿using FluentArch.Arch;
using FluentArch.ASTs;
using FluentArch.Layers;
using FluentArch.Result;
using Mapster;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
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



                //var controllerLayer = arch.Classes().That().ResideInNamespace("SimpleAPI.Controllers.*").And().HaveNameEndingWith("Controller").DefineAsLayer();
                //var repositoryLayer = arch.Classes().That().ResideInNamespace("Repositorios").DefineAsLayer();
                //var servicoLayer = arch.Classes().That().ResideInNamespace("Servicos").DefineAsLayer();

                //var arch = Architecture.Build(solution);
                //arch.All().ResideInNamespace("Entidades").UseCustomRule(new TypeCannotHaveFunctionsRule()).GetResult();

                var arch = Architecture.Build(solution);
                ILayer camadaApi = arch.All().ResideInNamespace("N_Tier.API.*").As("Api layer");
                ILayer camadaApplication = arch.All().ResideInNamespace("N_Tier.Application.*").As("Application layer");
                ILayer camadaDataAccess = arch.All().ResideInNamespace("N_Tier.DataAccess.*").As("DataAccess layer");
                ILayer camadaCore = arch.All().ResideInNamespace("N_Tier.Core.*").As("Core layer");
                ILayer camadaShared = arch.All().ResideInNamespace("N_Tier.Shared.*").As("Shared layer");
                ILayer camadaModels = arch.All().ResideInNamespace("N_Tier.Application.Models.*").As("Models layer");

                var listaResultados = new List<ConditionResult>();
                listaResultados.Add(camadaApi.OnlyCan().Depend(camadaApplication).Check());
                listaResultados.Add(camadaApplication.OnlyCan().Depend(camadaDataAccess).Check());
                listaResultados.Add(camadaDataAccess.Cannot().Depend(camadaApplication).And().Cannot().Depend(camadaApi).Check());
                listaResultados.Add(camadaCore.Cannot().Depend(camadaDataAccess).Check());
                listaResultados.Add(camadaApplication.And().ResideInNamespace("N_Tier.Application.Services.*").Cannot().Create(camadaDataAccess).Check());
                listaResultados.Add(camadaApplication.And().ResideInNamespace("N_Tier.Application.Exceptions").Must().Extends("System").Check());
                listaResultados.Add(camadaModels.UseCustomRule(new TypeCannotHaveFunctionsRule()).Check());
                listaResultados = arch.Check().ToList();
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

