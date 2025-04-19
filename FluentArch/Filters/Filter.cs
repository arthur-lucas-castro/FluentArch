using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.Rules;
using FluentArch.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Filters
{
    public class Filter
    {
        public readonly IEnumerable<ClassEntityDto> _classes;
        public Filter(IEnumerable<ClassEntityDto> classes) 
        {
            _classes = classes;
        }

        public Layer ResideInNamespace(string namespacePath)
        {
            //TODO: Criar regra de *
            var classesFiltradas = _classes.Where(classe => classe.Namespace.NamespaceCompare(namespacePath));
            return new Layer(classesFiltradas, "");
        }
    }
}
