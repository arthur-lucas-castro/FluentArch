using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO.Rules
{
    internal class LancamentosPorClasseDto
    {
        public string Nome { get; set; } = string.Empty;
        public List<EntityDto> Lancamentos { get; set; } = new List<EntityDto>();
    }
}
