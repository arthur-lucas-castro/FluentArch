using FluentArch.DTO;
using FluentArch.Layers;
using FluentArch.Utils;
using Mapster;

namespace FluentArch.Rules
{
    internal class DeclareRules
    {
        private string _dependecyType = "Declare";
        public DeclareRules() { }
        public DeclareRules(string dependecyType)
        {
            _dependecyType = dependecyType;
        }
        public List<ViolationDto> CannotDeclare(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var declaracoesQueViolamRegra = todasDeclaracoes.Where(declaracao => declaracao.CompareClassAndNamespace(todasEntityDto));
                if (!declaracoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = declaracoesQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });
            }         

            return violacoes;
        }

        public List<ViolationDto> DeclareOnly(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var declaracoesQueQuebramRegra = todasDeclaracoes.Where(declaracao => !declaracao.CompareClassAndNamespace(todasEntityDto));
                if (!declaracoesQueQuebramRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = declaracoesQueQuebramRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }

        public List<ViolationDto> MustDeclare(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var typeDeclaraTarget = todasDeclaracoes.Any(declaracao => declaracao.CompareClassAndNamespace(todasEntityDto));
                if (typeDeclaraTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassThatVioletesRule = type.Name, Violations = new List<EntityDto>() });
                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto>(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_MUST_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });

            }

            return violacoes;
        }

        public List<ViolationDto> OnlyCanDeclare(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var declaracoesQueViolamRegra = todasDeclaracoes.Where(declaracao => declaracao.CompareClassAndNamespace(todasEntityDto));
                if (!declaracoesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = declaracoesQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }

        private static List<EntityDto> ObterTodasDeclaracoes(TypeEntityDto type)
        {
            var todasDeclaracoes = new List<EntityDto>();
            todasDeclaracoes.AddRange(type.Functions.SelectMany(funcao => funcao.Parameters));
            todasDeclaracoes.AddRange(type.Properties);
            todasDeclaracoes.AddRange(type.Functions.SelectMany(funcao => funcao.LocalTypes));

            return todasDeclaracoes;
        }
    }
}
