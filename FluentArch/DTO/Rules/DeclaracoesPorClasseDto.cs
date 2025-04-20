using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO.Rules
{
    public class DeclaracoesPorClasseDto
    {
        public string Nome { get; set; } = string.Empty;
        public List<EntityDto> Declaracoes { get; set; } = new List<EntityDto>();
    }
}
