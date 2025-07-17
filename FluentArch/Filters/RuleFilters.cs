using FluentArch.Arch;
using FluentArch.DTO;
using FluentArch.Layers;
using FluentArch.Conditions.Interfaces;
using FluentArch.Utils;
using System.Text.RegularExpressions;

namespace FluentArch.Filters
{
    public class RuleFilters : IFilters
    {
        private List<TypeEntityDto> _types;

        public RuleFilters(List<TypeEntityDto> types)
        {
            _types = types;
        }

        public ILayer ResideInNamespace(string namespacePath)
        {
            var classesFiltradas = _types.Where(classe => classe.Namespace.NamespaceCompare(namespacePath));
            return new Layer(classesFiltradas);
        }

        public ILayer HaveNameStartingWith(string startingName)
        {
            var classesFiltradas = _types.Where(classe => classe.Name.StartsWith(startingName));
            return new Layer(_types); ;
        }
        public ILayer HaveNameStartingWith(string startingName, StringComparison stringComparison)
        {
            var classesFiltradas = _types.Where(classe => classe.Name.StartsWith(startingName, stringComparison));
            return new Layer(_types);
        }

        public ILayer HaveNameEndingWith(string endingName)
        {
            var classesFiltradas = _types.Where(classe => classe.Name.EndsWith(endingName));
            return new Layer(_types);
        }
        public ILayer HaveNameEndingWith(string endingName, StringComparison stringComparison)
        {
            var classesFiltradas = _types.Where(classe => classe.Name.EndsWith(endingName, stringComparison));
            return new Layer(_types);
        }
        public ILayer HaveNameMatchingWith(string pattern, RegexOptions regexOptions)
        {
            var classesFiltradas = _types.Where(classe => Regex.IsMatch(classe.Name, pattern, regexOptions));
            return new Layer(_types);
        }
        public ILayer HaveNameMatchingWith(string pattern)
        {
            var classesFiltradas = _types.Where(classe => Regex.IsMatch(classe.Name, pattern));
            return new Layer(_types);
        }

    }
}
