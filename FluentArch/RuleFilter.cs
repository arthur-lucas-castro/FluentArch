using FluentArch.Arch.Layer;
using FluentArch.Result;
using FluentArch.Rules;
using FluentArch.Rules.Interfaces;
using FluentArch.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FluentArch
{
    public class RuleFilter : IFilters
    {
        private readonly ICompleteRule _builder;

        public RuleFilter(ICompleteRule builder) 
        {
            _builder = builder;
        }

        public ILayer ResideInNamespace(string namespacePath)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => classe.Namespace.NamespaceCompare(namespacePath));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder);
        }

        public ILayer HaveNameStartingWith(string startingName)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => classe.Name.StartsWith(startingName));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder); ;
        }
        public ILayer HaveNameStartingWith(string startingName, StringComparison stringComparison)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => classe.Name.StartsWith(startingName, stringComparison));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder);
        }

        public ILayer HaveNameEndingWith(string endingName)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => classe.Name.EndsWith(endingName));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder);
        }
        public ILayer HaveNameEndingWith(string endingName, StringComparison stringComparison)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => classe.Name.EndsWith(endingName, stringComparison));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder);
        }
        public ILayer HaveNameMatchingWith(string pattern, RegexOptions regexOptions)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => Regex.IsMatch(classe.Name, pattern, regexOptions));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder);
        }
        public ILayer HaveNameMatchingWith(string pattern)
        {
            var classesFiltradas = _builder.GetTypes().Where(classe => Regex.IsMatch(classe.Name, pattern));
            _builder.UpdateTypes(classesFiltradas.ToList());
            return new Layer(_builder.GetTypes(), _builder);
        }

    }
}
