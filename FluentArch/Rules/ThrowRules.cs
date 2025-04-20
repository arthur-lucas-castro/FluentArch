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
    internal class ThrowRules
    {
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public bool Throw(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todosLancamentos = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Lancamentos));

            var resultado = todosLancamentos.Any(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto.ToList()));

            return resultado;
        }

        public bool ThrowsOnly(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var todosLancamentos = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Lancamentos));

            var resultado = todosLancamentos.All(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto.ToList()));

            return resultado;
        }

        //TODO: Validar
        public bool MustThrow(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var acessosPorClasse = classes.Select(classe => new LancamentosPorClasseDto
            {
                Nome = classe.Nome,
                Lancamentos = classe.Funcoes.SelectMany(funcao => funcao.Lancamentos).ToList(),
            });

            var todasEntityDto = layer._classes.Select(x => x.Adapt<EntityDto>());

            var resultado = acessosPorClasse.All(classe => classe.Lancamentos.Any(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto)));

            return resultado;
        }
    }
}
