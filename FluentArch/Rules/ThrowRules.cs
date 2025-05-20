using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.Utils;
using Mapster;

namespace FluentArch.Rules
{
    internal class ThrowRules
    {
        private const string DEPENDECY_TYPE = "Throws";
        public List<ViolationDto> CannotThrow(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Functions.SelectMany(f => f.Throws);

                var lancamentosQueViolamRegra = todosLancamentos.Where(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto));
                if (!lancamentosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = lancamentosQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }

        public List<ViolationDto> ThrowsOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Functions.SelectMany(f => f.Throws);

                var lancamentosQueViolamRegra = todosLancamentos.Where(lancamento => !lancamento.CompareClassAndNamespace(todasEntityDto));
                if (!lancamentosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = lancamentosQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }

        public List<ViolationDto> MustThrow(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Functions.SelectMany(f => f.Throws);

                var typeThrowTarget = todosLancamentos.Any(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto));

                if (typeThrowTarget)
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
        public List<ViolationDto> OnlyCanThrow(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Functions.SelectMany(f => f.Throws);

                var lancamentosQueViolamRegra = todosLancamentos.Where(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto));
                if (!lancamentosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = lancamentosQueViolamRegra.ToList(),
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }
    }
}
