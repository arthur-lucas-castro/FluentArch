using FluentArch.ASTs;
using FluentArch.DTO;
using FluentArch.Filters;
using FluentArch.Rules;
using FluentArch.Rules.Interfaces;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch
{
    public class Architecture
    {
        private static Architecture? _instance;
        private List<TypeEntityDto> _classes = new();
        private Architecture(Solution solution) 
        {
            var classVisitor = new ClassVisitor();

            foreach (var project in solution.Projects)
            {
                _classes.AddRange(classVisitor.ObterDadosDasClasses(project));
            }
        }
        public static Architecture Build(Solution solution)
        {
            if (_instance == null)
            {
                _instance = new Architecture(solution);
            }
            return new Architecture(solution);
        }
        
        public static IEnumerable<TypeEntityDto> GetClasses()
        {
            if (_instance == null)
            {
                return new List<TypeEntityDto>();
            }
            return _instance._classes;
        }

        public RuleFilter Classes()
        {
            var builder = new RuleBuilder();
            builder.UpdateTypes(_classes);

            return new RuleFilter(builder);
        }

        public static Architecture GetInstance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("");
            }
            return _instance;
        }

        public RuleFilter AreClasses()
        {
            var builder = new RuleBuilder();
            builder.UpdateTypes(_classes);

            return new RuleFilter(builder);
        }

        public IFilters All()
        {
            var builder = new RuleBuilder();
            builder.UpdateTypes(_classes);

            return new RuleFilter(builder);
        }
    }

}
