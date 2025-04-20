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
    internal class DeclareRules
    {

        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public bool Declare(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todasDeclaracoes = ObterTodasDeclaracoes(classes);

            var resultado = todasDeclaracoes.Any(declaracao => declaracao.CompareClassAndNamespace(todasEntityDto));
            return resultado;
        }

        public bool DeclareOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());
            var todasDeclaracoes = ObterTodasDeclaracoes(classes);
            var resultado = todasDeclaracoes.All(acesso => acesso.CompareClassAndNamespace(todasEntityDto.ToList()));
            return resultado;
        }

        //TODO: Validar
        public bool MustDeclare(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var acessosPorClasse = classes.Select(classe => new DeclaracoesPorClasseDto
            {
                Nome = classe.Nome,
                Declaracoes = ObterTodasDeclaracoes(new List<ClassEntityDto> { classe }),
            });

            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());
            var resultado = acessosPorClasse.All(classe => classe.Declaracoes.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto)));

            return resultado;
        }

        private static List<EntityDto> ObterTodasDeclaracoes(IEnumerable<ClassEntityDto> classes)
        {
            var todasDeclaracoes = new List<EntityDto>();
            todasDeclaracoes.AddRange(classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Parametros)));
            todasDeclaracoes.AddRange(classes.SelectMany(classe => classe.Propriedades));
            todasDeclaracoes.AddRange(classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.TiposLocais)));

            return todasDeclaracoes;
        }
    }
}
