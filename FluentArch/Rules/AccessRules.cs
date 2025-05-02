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
    public class AccessRules
    {
        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public List<ViolationDto> CannotAccess(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Funcoes.SelectMany(f => f.Acessos);

                var acessosQueViolamRegra = todosAcessos.Where(criacao => criacao.CompareClassAndNamespace(todasEntityDto));
                if (!acessosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = acessosQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        public List<ViolationDto> AccessOnly(IEnumerable<TypeEntityDto> types, ILayer layer)
        {
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Funcoes.SelectMany(f => f.Acessos);

                var acessosQueViolamRegra = todosAcessos.Where(criacao => !criacao.CompareClassAndNamespace(todasEntityDto));
                if (!acessosQueViolamRegra.Any())
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = acessosQueViolamRegra.ToList() });
            }

            return violacoes;
        }

        //TODO: Validar
        public List<ViolationDto> MustAccess(IEnumerable<TypeEntityDto> types, ILayer layer)
        {  
            var todasEntityDto = layer.GetTypes().Select(x => x.Adapt<EntityDto>());

            var violacoes = new List<ViolationDto>();
            foreach (var type in types)
            {
                var todosAcessos = type.Funcoes.SelectMany(f => f.Criacoes);

                var typeAcessaTarget = todosAcessos.Any(acessos => acessos.CompareClassAndNamespace(todasEntityDto));
                if (typeAcessaTarget)
                {
                    continue;
                }

                violacoes.Add(new ViolationDto { ClassName = type.Nome, Violations = new List<EntityDto>() });
            }

            return violacoes;
        }
    }
}
