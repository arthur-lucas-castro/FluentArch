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
        public List<ViolationDto> CannotCreate(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Funcoes.SelectMany(f => f.Criacoes);

                var criacoesQueViolamRegra = todasCriacoes.Where(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (!criacoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = criacoesQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        public List<ViolationDto> CreateOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Funcoes.SelectMany(f => f.Criacoes);

                var criacoesQueViolamRegra = todasCriacoes.Where(criacao => !criacao.CompareClassAndNamespace(todasEntityDto));
                if (!criacoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = criacoesQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        //TODO: Validar
        public List<ViolationDto> MustCreate(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Funcoes.SelectMany(f => f.Criacoes);

                var typeCriaTarget = todasCriacoes.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (typeCriaTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto>() });
            }

            return violacoes;

        }
    }
}
