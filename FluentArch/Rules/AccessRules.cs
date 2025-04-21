using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.DTO.Rules;
using FluentArch.Result;
using FluentArch.Utils;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class AccessRules
    {
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public PreResult CannotAccess(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var acessosPorClasse = classes.Select(classe => new AcessosPorClasseDto
            {
                ClassName = classe.Nome,
                Access = classe.Funcoes.SelectMany(funcao => funcao.Acessos).ToList(),
            });

            var violations = acessosPorClasse
                .Select(classe => new ViolationDto
                {
                    ClassName = classe.ClassName,
                    Violations = classe.Access.Where(c => c.CompareClassAndNamespace(todasEntityDto)).ToList()
                })
                .Where(classe => classe.Violations.Any());

            if (violations.Any())
            {
                return new PreResult(isSuccessful: false, violations);
            }
            return new PreResult(isSuccessful: true);
        }

        public PreResult AccessOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());
            var todosAcessos = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Acessos));
            var resultado = todosAcessos.All(acesso => acesso.CompareClassAndNamespace(todasEntityDto.ToList()));

            var allEntitysLayerTarget = layer._classes.Select(x => x.Adapt<EntityDto>());

            var acessosPorClasse = classes.Select(classe => new AcessosPorClasseDto
            {
                ClassName = classe.Nome,
                Access = classe.Funcoes.SelectMany(funcao => funcao.Acessos).ToList(),
            });

            //TODO: Pensar em um metodo mais generico;
            var violations = acessosPorClasse
               .Select(classe => new ViolationDto
               {
                   ClassName = classe.ClassName,
                   Violations = classe.Access.Where(c => !c.CompareClassAndNamespace(allEntitysLayerTarget)).ToList()
               })
               .Where(classe => classe.Violations.Any());

            if (violations.Any())
            {
                return new PreResult(isSuccessful: false, violations);
            }
            return new PreResult(isSuccessful: true);
        }

        //TODO: Validar
        public PreResult MustAccess(Layer layer, IEnumerable<ClassEntityDto> classes)
        {  
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var acessosPorClasse = classes.Select(classe => new AcessosPorClasseDto
            {
                ClassName = classe.Nome,
                Access = classe.Funcoes.SelectMany(funcao => funcao.Acessos).ToList(),
            });

            var allEntitysLayerTarget = layer._classes.Select(x => x.Adapt<EntityDto>());

            var classesWithoutRequiredCreation = acessosPorClasse
              .Select(classe => new AcessosPorClasseDto
              {
                  ClassName = classe.ClassName,
                  Access = classe.Access.Where(c => c.CompareClassAndNamespace(allEntitysLayerTarget)).ToList()
              })
              .Where(classe => !classe.Access.Any());

            if (classesWithoutRequiredCreation.Any())
            {
                var violations = classesWithoutRequiredCreation.Select(classe => classe.Adapt<ViolationDto>());
                return new PreResult(isSuccessful: false, violations);
            }
            return new PreResult(isSuccessful: true);
        }
    }
}
