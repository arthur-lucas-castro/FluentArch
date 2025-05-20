using FluentArch.Arch.Layer;
using FluentArch.DTO.Rules;
using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using FluentArch.Utils;
using Mapster;

namespace FluentArch.Rules
{
    internal class ImplementsRules
    {
        private const string DEPENDECY_TYPE = "Implements";
        public List<ViolationDto> CannotImplements(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todasInterfaces = types.SelectMany(classe => classe.Interfaces);

                var interfacesQueViolamRegra = todasInterfaces.Where(interfaceAnalisada => interfaceAnalisada.CompareClassAndNamespace(todasEntityDto));

                if (!interfacesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = interfacesQueViolamRegra.ToList(),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }

        public List<ViolationDto> ImplementsOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todasInterfaces = types.SelectMany(classe => classe.Interfaces);

                var interfacesQueViolamRegra = todasInterfaces.Where(interfaceAnalisada => !interfaceAnalisada.CompareClassAndNamespace(todasEntityDto));

                if (!interfacesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = interfacesQueViolamRegra.ToList(),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }

        public List<ViolationDto> MustImplements(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todasInterfaces = types.SelectMany(classe => classe.Interfaces);

                var typeImplementaTarget = todasInterfaces.Any(interfaceAnalisada => interfaceAnalisada.CompareClassAndNamespace(todasEntityDto));

                if (typeImplementaTarget)
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

        public List<ViolationDto> OnlyCanImplements(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todasInterfaces = types.SelectMany(classe => classe.Interfaces);

                var interfacesQueViolamRegra = todasInterfaces.Where(interfaceAnalisada => interfaceAnalisada.CompareClassAndNamespace(todasEntityDto));

                if (!interfacesQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = interfacesQueViolamRegra.ToList(),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }
    }
}
