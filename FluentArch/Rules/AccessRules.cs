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
        private const string DEPENDECY_TYPE = "Access";
        public List<ViolationDto> CannotAccess(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Functions.SelectMany(f => f.Access);

                var acessosQueViolamRegra = todosAcessos.Where(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (!acessosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = acessosQueViolamRegra.ToList(),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }

        public List<ViolationDto> AccessOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Functions.SelectMany(f => f.Access);

                var acessosQueViolamRegra = todosAcessos.Where(criacao => !criacao.CompareClassAndNamespace(todasEntityDto));
                if (!acessosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = acessosQueViolamRegra.ToList(),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }

        public List<ViolationDto> MustAccess(IEnumerable<TypeEntityDto> types, ILayer layer)
        {  
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Functions.SelectMany(f => f.Creations);

                var typeAcessaTarget = todosAcessos.Any(acessos => acessos.CompareClassAndNamespace(todasEntityDto));
                if (typeAcessaTarget)
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

        public List<ViolationDto> OnlyCanAccess(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Functions.SelectMany(f => f.Access);

                var acessosQueViolamRegra = todosAcessos.Where(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (!acessosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = acessosQueViolamRegra.ToList(),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }
    }
}
