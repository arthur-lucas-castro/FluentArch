using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentArch.DTO
{
    public class TypeEntityDto : EntityDto
    {
        public bool IsInterface { get; set; } = false;
        public List<FunctionEntityDto> Functions { get; set; } = new List<FunctionEntityDto>();
        public List<EntityDto> Properties { get; set; } = new List<EntityDto>();
        public EntityDto? Inheritance { get; set; }
        public List<EntityDto> Interfaces { get; set; } = new List<EntityDto>();
        public string AccessibilityLevel { get; set; } = string.Empty;
        public List<EntityDto> Annotations { get; set; } = new List<EntityDto>();
    }
}
