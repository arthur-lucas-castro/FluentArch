using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO.Rules
{
    public class AcessosPorClasseDto
    {
        public string Nome { get; set; } = string.Empty;
        public List<EntityDto> Acessos { get; set; } = new List<EntityDto>();
    }
}
