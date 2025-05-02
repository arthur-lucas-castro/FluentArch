using FluentArch.DTO;

using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch
{
    public class ClassArch
    {
        private readonly IEnumerable<TypeEntityDto> _classes;

        public ClassArch(IEnumerable<TypeEntityDto> classes)
        {
            _classes = classes;
        }

    }
}
