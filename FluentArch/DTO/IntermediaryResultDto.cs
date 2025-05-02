using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO
{
    public class IntermediaryResultDto
    {
        public string ClassName { get; set; } = string.Empty;
        public List<EntityDto> Violations { get; set; } = new List<EntityDto>();
    }
}
