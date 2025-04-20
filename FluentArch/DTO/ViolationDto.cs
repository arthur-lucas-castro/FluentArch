using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO
{
    public class ViolationDto
    {
        public string ClassName = string.Empty;
        public List<EntityDto> Violations = new();
    }
}
