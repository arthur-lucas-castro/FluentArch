using FluentArch.ASTs;
using FluentArch.DTO;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch
{
    public class Architecture
    {
        private readonly List<ClassEntityDto> _classes = new();
        public Architecture(Solution solution) 
        {
            var classVisitor = new ClassVisitor();

            foreach (var project in solution.Projects)
            {
                _classes.AddRange(classVisitor.ObterDadosDasClasses(project));
            }
        }

    }
}
