using FluentArch.DTO;
using FluentArch.DTO.Rules;
using FluentArch.Layer;
using FluentArch.Result;
using FluentArch.Utils;
using Mapster;

namespace FluentArch.Rules
{
    public class CreateRules
    {
        private const string DEPENDECY_TYPE = "Create";
        public List<ViolationDto> CannotCreate(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Functions.SelectMany(f => f.Creations);

                var criacoesQueViolamRegra = todasCriacoes.Where(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (!criacoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto { 
                        ClassThatVioletesRule = type.Name,
                        Violations = criacoesQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name] )
                    });
            }

            return violacoes;
        }

        public List<ViolationDto> CreateOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Functions.SelectMany(f => f.Creations);

                var criacoesQueViolamRegra = todasCriacoes.Where(criacao => !criacao.CompareClassAndNamespace(todasEntityDto));
                if (!criacoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                  new ViolationDto
                  {
                      ClassThatVioletesRule = type.Name,
                      Violations = criacoesQueViolamRegra.ToList(),
                      ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                  });
            }

            return violacoes;
        }
        public List<ViolationDto> MustCreate(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Functions.SelectMany(f => f.Creations);

                var typeCriaTarget = todasCriacoes.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (typeCriaTarget)
                {
                    continue;
                }

                violacoes.Add(
                  new ViolationDto
                  {
                      ClassThatVioletesRule = type.Name,
                      Violations = new List<EntityDto>(),
                      ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_MUST_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                  });
            }

            return violacoes;
        }
        public List<ViolationDto> OnlyCanCreate(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasCriacoes = type.Functions.SelectMany(f => f.Creations);

                var criacoesQueViolamRegra = todasCriacoes.Where(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (!criacoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = criacoesQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }
    }
}
