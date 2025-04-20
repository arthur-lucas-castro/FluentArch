using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO.Rules
{
    public class CriacoesPorClasseDto
    {
        public string ClassName { get; set; } = string.Empty;
        public List<EntityDto> Criacoes { get; set; } = new List<EntityDto>();
    }
}
