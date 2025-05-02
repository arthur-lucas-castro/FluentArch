using FluentArch.Arch.Layer;
using FluentArch.DTO.Rules;
using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using FluentArch.Utils;

namespace FluentArch.Rules
{
    internal class ExtendsRules
    {
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public List<ViolationDto> CannotExtends(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                if (type.Heranca is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Heranca.CompareClassAndNamespace(todasEntityDto);

                if (!typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto> { type.Heranca } });
            }

            return violacoes;
        }

        public List<ViolationDto> ExtendsOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            //TODO: Criar mapper
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                if (type.Heranca is null)
                {
                    continue;
                }

                var typeHerdaTarget = type.Heranca.CompareClassAndNamespace(todasEntityDto);

                if (typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto> { type.Heranca } });
            }

            return violacoes;
        }

        //TODO: Validar
        public List<ViolationDto> MustExtends(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                if (type.Heranca is null)
                {
                    violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto>() });
                    continue;
                }

                var typeHerdaTarget = type.Heranca.CompareClassAndNamespace(todasEntityDto);

                if (typeHerdaTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto> { type.Heranca } });
            }

            return violacoes;
        }
    }
}
