using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.DTO
{
    public class ViolationDto
    {
        public string ClassThatVioletesRule = string.Empty;
        public List<EntityDto> Violations = new();
        public string ViolationReason = string.Empty;
    }
}
