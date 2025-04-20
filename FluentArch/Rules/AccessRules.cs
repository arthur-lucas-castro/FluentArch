using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.DTO.Rules;
using FluentArch.Utils;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class AccessRules
    {
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public bool Access(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());
            var todosAcessos = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Acessos));
            var resultado = todosAcessos.Any(acesso => acesso.CompareClassAndNamespace(todasEntityDto.ToList()));
            return resultado;
        }

        public bool AccessOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());
            var todosAcessos = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Acessos));
            var resultado = todosAcessos.All(acesso => acesso.CompareClassAndNamespace(todasEntityDto.ToList()));
            return resultado;
        }

        //TODO: Validar
        public bool MustAccess(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var acessosPorClasse = classes.Select(classe => new AcessosPorClasseDto
            {
                Nome = classe.Nome,
                Acessos = classe.Funcoes.SelectMany(funcao => funcao.Acessos).ToList(),
            });
            
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());
            var resultado = acessosPorClasse.All(classe => classe.Acessos.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto)));

            return resultado;
        }
    }
}
