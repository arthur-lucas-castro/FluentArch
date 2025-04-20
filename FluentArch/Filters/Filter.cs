using FluentArch.DTO;
using FluentArch.Utils;
using System.Text.RegularExpressions;

namespace FluentArch.Filters
{
    public class Filter
    {
        public readonly IEnumerable<ClassEntityDto> _classes;
        public Filter(IEnumerable<ClassEntityDto> classes) 
        {
            _classes = classes;
        }

        public IntermediaryFilter ResideInNamespace(string namespacePath)
        {
            var classesFiltradas = _classes.Where(classe => classe.Namespace.NamespaceCompare(namespacePath));
            return new IntermediaryFilter(classesFiltradas);
        }

        public IntermediaryFilter HaveNameStartingWith(string startingName)
        {
            var classesFiltradas = _classes.Where(classe => classe.Nome.StartsWith(startingName));
            return new IntermediaryFilter(classesFiltradas);
        }
        public IntermediaryFilter HaveNameStartingWith(string startingName, StringComparison stringComparison)
        {
            var classesFiltradas = _classes.Where(classe => classe.Nome.StartsWith(startingName, stringComparison));
            return new IntermediaryFilter(classesFiltradas);
        }
        public IntermediaryFilter HaveNameEndingWith(string endingName)
        {
            var classesFiltradas = _classes.Where(classe => classe.Nome.EndsWith(endingName));
            return new IntermediaryFilter(classesFiltradas);
        }
        public IntermediaryFilter HaveNameMatchingWith(string pattern, RegexOptions regexOptions)
        {
            var classesFiltradas = _classes.Where(classe => Regex.IsMatch(classe.Nome, pattern, regexOptions));
            return new IntermediaryFilter(classesFiltradas);
        }
        public IntermediaryFilter HaveNameMatchingWith(string pattern)
        {
            var classesFiltradas = _classes.Where(classe => Regex.IsMatch(classe.Nome, pattern));
            return new IntermediaryFilter(classesFiltradas);
        }
    }
}
