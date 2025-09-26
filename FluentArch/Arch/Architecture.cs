using FluentArch.ASTs;
using FluentArch.DTO;
using FluentArch.Filters;
using FluentArch.Layers;
using FluentArch.Result;
using FluentArch.Conditions;
using FluentArch.Conditions.Interfaces;
using Microsoft.CodeAnalysis;

namespace FluentArch.Arch
{
    public  class Architecture
    {
        private static Architecture? _instance;
        private List<TypeEntityDto> _types = new();

        private static List<ArchRule> _rules = new();
        private Architecture(Solution solution) 
        {
            var classVisitor = new ClassVisitor();

            foreach (var project in solution.Projects)
            {
                _types.AddRange(classVisitor.ObterDadosDasClasses(project));
            }
        }
        public static void Reset()
        {
            _instance = null;
            _rules.Clear();
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
            return _instance._types;
        }
       
        public RuleFilters Classes()
        {
            return new RuleFilters(_types);
        }

        public static Architecture GetInstance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("");
            }
            return _instance;
        }

        public IFilters All()
        {
            return new RuleFilters(_types);
        }

        internal static void AddRule(ArchRule archRule)
        {
            _rules.Add(archRule);
        }

        public IEnumerable<ConditionResult> Check()
        {
            var allRusults = _rules.SelectMany(rule => rule.GetResults());
            return allRusults.Where(x => !x.IsSuccessful);
        }
    }

}
