using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentArch.DTO
{
    public class FunctionEntityDto : EntityDto
    {
        public List<EntityDto> Parametros { get; set; } = new List<EntityDto>();
        public List<EntityDto> Declaracoes { get; set; } = new List<EntityDto>();
        public List<EntityDto> Acessos { get; set; } = new List<EntityDto>();
        public List<EntityDto> Criacoes { get; set; } = new List<EntityDto>();
        public List<EntityDto> Lancamentos { get; set; } = new List<EntityDto>();
        public EntityDto? Retorno { get; set; }
        public int QuantidadeLinhas { get; set; }
        public string NivelAcesso { get; set; } = string.Empty; //TODO: Criar enum de nivel de acesso
        public EntityDto? Anotacao { get; set; }
        public string Bloco { get; set; } //TODO: Trocar tipo do bloco
    }
}
