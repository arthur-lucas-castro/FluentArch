using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentArch.DTO
{
    public class FunctionEntityDto : EntityDto
    {
        public List<EntityDto> Parameters { get; set; } = new List<EntityDto>();
        public List<EntityDto> LocalTypes { get; set; } = new List<EntityDto>();
        public List<EntityDto> Access { get; set; } = new List<EntityDto>();
        public List<EntityDto> Creations { get; set; } = new List<EntityDto>();
        public List<EntityDto> Throws { get; set; } = new List<EntityDto>();
        public List<EntityDto> ReturnTypes { get; set; } = new List<EntityDto>();
        public bool IsConstructor { get; set; }
        public string AccessibilityLevel { get; set; } = string.Empty;
        public List<EntityDto> Annotations { get; set; } = new List<EntityDto>();
        public BlockSyntax? Bloco { get; set; }
    }
}
