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
        public List<ViolationDto> CannotThrow(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Funcoes.SelectMany(f => f.Lancamentos);

                var lancamentosQueViolamRegra = todosLancamentos.Where(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto));
                if (!lancamentosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = lancamentosQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        public List<ViolationDto> ThrowsOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Funcoes.SelectMany(f => f.Lancamentos);

                var lancamentosQueViolamRegra = todosLancamentos.Where(lancamento => !lancamento.CompareClassAndNamespace(todasEntityDto));
                if (!lancamentosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = lancamentosQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        //TODO: Validar
        public List<ViolationDto> MustThrow(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();

            foreach (var type in types)
            {
                var todosLancamentos = type.Funcoes.SelectMany(f => f.Lancamentos);

                var typeThrowTarget = todosLancamentos.Any(lancamento => lancamento.CompareClassAndNamespace(todasEntityDto));

                if (typeThrowTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto>() });
            }

            return violacoes;
        }
    }
}
