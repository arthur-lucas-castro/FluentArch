using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.DTO.Rules;
using FluentArch.Result;
using FluentArch.Utils;
using Mapster;

namespace FluentArch.Rules
{
    public class CreateRules
    {
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public PreResult CannotCreate(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var criacoesPorClasse = classes.Select(classe => new CriacoesPorClasseDto
            {
                ClassName = classe.Nome,
                Criacoes = classe.Funcoes.SelectMany(funcao => funcao.Criacoes).ToList(),
            });

            var violations = criacoesPorClasse
                .Select(classe => new ViolationDto
                {
                    ClassName = classe.ClassName,
                    Violations = classe.Criacoes.Where(c => c.CompareClassAndNamespace(todasEntityDto)).ToList()
                })
                .Where(classe => classe.Violations.Any());

            if(violations.Any()) 
            {
                return new PreResult(isSuccessful: false, violations);
            }
            return new PreResult(isSuccessful: true);
        }

        public PreResult CreateOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var allEntitysLayerTarget = layer._classes.Select(x => x.Adapt<EntityDto>());

            var criacoesPorClasse = classes.Select(classe => new CriacoesPorClasseDto
            {
                ClassName = classe.Nome,
                Criacoes = classe.Funcoes.SelectMany(funcao => funcao.Criacoes).ToList(),
            });

            var violations = criacoesPorClasse
               .Select(classe => new ViolationDto
               {
                   ClassName = classe.ClassName,
                   Violations = classe.Criacoes.Where(c => !c.CompareClassAndNamespace(allEntitysLayerTarget)).ToList()
               })
               .Where(classe => classe.Violations.Any());

            if(violations.Any())
            {
                return new PreResult(isSuccessful: false, violations);
            }
            return new PreResult(isSuccessful: true);
        }

        //TODO: Validar
        public PreResult MustCreate(IEnumerable<ClassEntityDto> classes, Layer layer)
        {
            var criacoesPorClasse = classes.Select(classe => new CriacoesPorClasseDto
            {
                ClassName = classe.Nome,
                Criacoes = classe.Funcoes.SelectMany(funcao => funcao.Criacoes).ToList(),
            });

            var allEntitysLayerTarget = layer._classes.Select(x => x.Adapt<EntityDto>());

            var classesWithoutRequiredCreation = criacoesPorClasse
              .Select(classe => new CriacoesPorClasseDto
              {
                  ClassName = classe.ClassName,
                  Criacoes = classe.Criacoes.Where(c => c.CompareClassAndNamespace(allEntitysLayerTarget)).ToList()
              })
              .Where(classe => !classe.Criacoes.Any());

            if (classesWithoutRequiredCreation.Any())
            {
                var violations = classesWithoutRequiredCreation.Select(classe => classe.Adapt<ViolationDto>());
                return new PreResult(isSuccessful: false, violations);
            }
            return new PreResult(isSuccessful: true);

        }
    }
}
