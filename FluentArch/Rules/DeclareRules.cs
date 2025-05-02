using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.DTO.Rules;
using FluentArch.Result;
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
        public List<ViolationDto> CannotDeclare(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var declaracoes = todasDeclaracoes.Where(declaracao => declaracao.CompareClassAndNamespace(todasEntityDto));
                if (!declaracoes.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = declaracoes.ToList() });
            }          

            return violacoes;
        }

        public List<ViolationDto> DeclareOnly(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var declaracoesQueQuebramRegra = todasDeclaracoes.Where(declaracao => !declaracao.CompareClassAndNamespace(todasEntityDto));
                if (!declaracoesQueQuebramRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = declaracoesQueQuebramRegra.ToList() });
            }

            return violacoes;
        }

        //TODO: Validar
        public List<ViolationDto> MustDeclare(List<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todasDeclaracoes = ObterTodasDeclaracoes(type);

                var typeDeclaraTarget = todasDeclaracoes.Any(declaracao => declaracao.CompareClassAndNamespace(todasEntityDto));
                if (typeDeclaraTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto>() });
            }

            return violacoes;
        }

        private static List<EntityDto> ObterTodasDeclaracoes(TypeEntityDto type)
        {
            var todasDeclaracoes = new List<EntityDto>();
            todasDeclaracoes.AddRange(type.Funcoes.SelectMany(funcao => funcao.Parametros));
            todasDeclaracoes.AddRange(type.Propriedades);
            todasDeclaracoes.AddRange(type.Funcoes.SelectMany(funcao => funcao.TiposLocais));

            return todasDeclaracoes;
        }
    }
}
