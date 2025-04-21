using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO.Rules
{
    public class AcessosPorClasseDto
    {
        public string ClassName { get; set; } = string.Empty;
        public List<EntityDto> Access { get; set; } = new List<EntityDto>();
    }
}
