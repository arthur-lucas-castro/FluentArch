using FluentArch.DTO;
using Mapster;
using FluentArch.Utils;
using FluentArch.Layers;

namespace FluentArch.Conditions
{
    internal class ExtendsRules
    {
        private string _dependecyType = "Extends";
        public ExtendsRules() { }
        public ExtendsRules(string dependecyType)
        {
            _dependecyType = dependecyType;
        }
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
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }
        public List<ViolationDto> CannotExtends(IEnumerable<TypeEntityDto> types, string fullName)
        {

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.FullName.NamespaceCompare(fullName);

                if (!typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Inheritance },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CANNOT_DESCRIPTION, [_dependecyType, fullName, type.Name])
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
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }
        public List<ViolationDto> ExtendsOnly(IEnumerable<TypeEntityDto> types, string fullName)
        {
            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.FullName.NamespaceCompare(fullName);

                if (typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Inheritance },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [_dependecyType, fullName, type.Name])
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
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_MUST_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
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

                var typeHerdaTarget = type.Inheritance.FullName.NamespaceCompare(namespacePath);

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
                       ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_CAN_ONLY_DESCRIPTION, [_dependecyType, namespacePath, type.Name])
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
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [_dependecyType, layer.GetName(), type.Name])
                    });
            }

            return violacoes;
        }
        public List<ViolationDto> OnlyCanExtends(IEnumerable<TypeEntityDto> types, string namespacePath)
        {

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                if (type.Inheritance is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Inheritance.FullName.NamespaceCompare(namespacePath);

                if (!typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(
                    new ViolationDto
                    {
                        ClassThatVioletesRule = type.Name,
                        Violations = new List<EntityDto> { type.Inheritance },
                        ViolationReason = ErrorDescriptionFormarter.FormatarErrorDescription(ErrorReasons.ERROR_ONLY_CAN_DESCRIPTION, [_dependecyType, namespacePath, type.Name])
                    });
            }

            return violacoes;
        }
    }
}
