using FluentArch.Layers;
using System.Text.RegularExpressions;

namespace FluentArch.Rules.Interfaces
{
    public interface IFilters
    {
        ILayer ResideInNamespace(string namespacePath);
        ILayer HaveNameStartingWith(string startingName);
        ILayer HaveNameStartingWith(string startingName, StringComparison stringComparison);
        ILayer HaveNameEndingWith(string endingName);
        ILayer HaveNameEndingWith(string endingName, StringComparison stringComparison);
        ILayer HaveNameMatchingWith(string pattern, RegexOptions regexOptions);
        ILayer HaveNameMatchingWith(string pattern);
    }
}
