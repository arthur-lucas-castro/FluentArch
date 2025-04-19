using FluentArch.DTO;
using FluentArch.Filters;
using FluentArch.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch.Layer
{
    public class Layer
    {
        public readonly IEnumerable<ClassEntityDto> _classes;
        private string _name;

        public Layer(IEnumerable<ClassEntityDto> classes, string name)
        {
            _classes = classes;
            _name = name;
        }
        public Filter That()
        {
            return new Filter(_classes);
        }

        public Rules.Rules Should()
        {
            return new Rules.Rules(_classes, isNegative: false);
        }
        public Rules.Rules ShouldNot()
        {
            return new Rules.Rules(_classes, isNegative: true);
        }

    }
}
