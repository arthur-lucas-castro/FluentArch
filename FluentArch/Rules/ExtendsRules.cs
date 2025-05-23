using FluentArch.DTO.Rules;
using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using FluentArch.Utils;
using System.Reflection.Emit;
using FluentArch.Layer;

namespace FluentArch.Rules
{
    internal class ExtendsRules
    {
        private const string DEPENDECY_TYPE = "Extends";
        public List<ViolationDto> CannotExtends(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.CompareClassAndNamespace(todasEntityDto);

                if (!typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Inheritance },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }

        public List<ViolationDto> ExtendsOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.CompareClassAndNamespace(todasEntityDto);

                if (typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Inheritance },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }

        public List<ViolationDto> MustExtends(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    violacoes.Add(new ViolationDto { ClassThatVioletesRule = type.Name, Violations = new List<EntityDto>() });
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.CompareClassAndNamespace(todasEntityDto);

                if (typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = new List<EntityDto> (),
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_MUST_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                   });
            }

            return violacoes;
        }

        public List<ViolationDto> MustExtends(IEnumerable<TypeEntityDto> types, string namespacePath)
        {

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    violacoes.Add(new ViolationDto { ClassThatVioletesRule = type.Name, Violations = new List<EntityDto>() });
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.Namespace.NamespaceCompare(namespacePath);

                if (typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassThatVioletesRule = type.Name, Violations = new List<EntityDto> { type.Inheritance } });
                violacoes.Add(
                   new ViolationDto
                   {
                       ClassThatVioletesRule = type.Name,
                       Violations = new List<EntityDto> { type.Inheritance },
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [DEPENDECY_TYPE, namespacePath, type.Name])
                   });
            }

            return violacoes;
        }

        public List<ViolationDto> OnlyCanExtends(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.CompareClassAndNamespace(todasEntityDto);

                if (!typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Inheritance },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [DEPENDECY_TYPE, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }
    }
}
