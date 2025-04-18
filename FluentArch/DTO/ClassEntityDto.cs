using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentArch.DTO
{
    public class ClassEntityDto : EntityDto
    {
        public List<FunctionEntityDto> Funcoes { get; set; } = new List<FunctionEntityDto>();
        public List<EntityDto> Propriedades { get; set; } = new List<EntityDto>();
        public EntityDto? Heranca { get; set; }
        public List<EntityDto> Interfaces { get; set; } = new List<EntityDto>(); //TODO: Criar tipo para interface
        public int QuantidadeLinhas { get; set; }
        public string NivelAcesso { get; set; } = string.Empty;
        public EntityDto? Anotacao { get; set; } 
    }
}
