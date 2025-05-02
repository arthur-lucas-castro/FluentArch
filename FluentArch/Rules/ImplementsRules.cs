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
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
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

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = interfacesQueViolamRegra.ToList() });
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

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = interfacesQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        //TODO: Validar
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

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto>() });
            }

            return violacoes;
        }
    }
}
