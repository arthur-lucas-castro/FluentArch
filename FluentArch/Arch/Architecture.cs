using FluentArch.ASTs;
using FluentArch.DTO;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch
{
    public class Architecture
    {
        private static Architecture _instance;
        private List<ClassEntityDto> _classes = new();
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
            return _instance;
        }
        public static IEnumerable<ClassEntityDto> GetClasses()
        {
            if (_instance == null)
            {
                return new List<ClassEntityDto>();
            }
            return _instance._classes;
        }
        public void DefineAsLayer()
        {

        }

        public void ResideInNamespace(string namespacePath)
        {

        }

        public ClassArch Classes()
        {
            return new ClassArch(_classes);
        }


    }

    public class Predicados
    {
        private List<ClassEntityDto> _classes;
        public Predicados(List<ClassEntityDto> classes)
        {
            _classes = classes;
        }
    }
}
