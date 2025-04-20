using FluentArch.DTO;
using FluentArch.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch
{
    public class ClassArch
    {
        private readonly IEnumerable<ClassEntityDto> _classes;

        public ClassArch(IEnumerable<ClassEntityDto> classes)
        {
            _classes = classes;
        }

        public Filter That()
        {
            return new Filter(_classes);
        }
    }
}
