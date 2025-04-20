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
        public bool Extends(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todasHerancas = classes.Where(classe => classe.Heranca is not null).Select(classe => classe.Heranca);

            var resultado = todasHerancas.Any(heranca => heranca!.CompareClassAndNamespace(todasEntityDto));

            return resultado;
        }

        public bool ExtendsOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            //TODO: Criar mapper
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todasHerancas = classes.Where(classe => classe.Heranca is not null).Select(classe => classe.Heranca);

            var resultado = todasHerancas.All(heranca => heranca!.CompareClassAndNamespace(todasEntityDto.ToList()));

            return resultado;
        }

        //TODO: Validar
        public bool MustExtends(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasHerancas = classes.Where(classe => classe.Heranca is not null).Select(classe => classe.Heranca);

            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var resultado = classes.All(classe => classe.Heranca is not null && classe.Heranca!.CompareClassAndNamespace(todasEntityDto));

            return resultado;
        }
    }
}
