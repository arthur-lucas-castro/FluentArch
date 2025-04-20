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
        public bool Implements(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todasInterfaces = classes.SelectMany(classe => classe.Interfaces);

            var resultado = todasInterfaces.Any(interfaceAnalisada => interfaceAnalisada.CompareClassAndNamespace(todasEntityDto.ToList()));

            return resultado;
        }

        public bool ImplementsOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todasInterfaces = classes.SelectMany(classe => classe.Interfaces);

            var resultado = todasInterfaces.All(interfaceAnalisada => interfaceAnalisada.CompareClassAndNamespace(todasEntityDto.ToList()));

            return resultado;
        }

        //TODO: Validar
        public bool MustImplements(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var resultado = classes.All(classe => classe.Interfaces.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto)));

            return resultado;
        }
    }
}
