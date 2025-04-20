using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO
{
    public class ViolationDto
    {
        public string NameClasse = string.Empty;
        public List<EntityDto> Violations = new();
    }
}
