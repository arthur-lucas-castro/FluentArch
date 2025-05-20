using FluentArch.Arch;
using FluentArch.DTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.ASTs
{
    public class FunctionVisitorTest
    {
        private const string NameOfTargetClass = "TargetClass";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";

        [Fact]
        public void ClassSource_CreateClassTarget_Success()
        {
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var targetClass = Classes.GetClassWithOneMethod(NamespaceTarget);

            var solution = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var resultadoEsperado = new EntityDto { Name = NameOfTargetClass, Namespace = NamespaceTarget };


            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var criacao = classeSource!.Functions.SelectMany(x => x.Creations).First();


            Assert.True(criacao.Namespace.Equals(resultadoEsperado.Namespace) && criacao.Name.Equals(resultadoEsperado.Name));

        }

        [Fact]
        public void ClassSource_AccessTargetClass_Success()
        {
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };
            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var targetClass = Classes.GetClassWithOneMethod(NamespaceTarget);

            var solution = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var resultadoEsperado = new EntityDto { Name = NameOfTargetClass, Namespace = NamespaceTarget };


            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var acesso = classeSource!.Functions.SelectMany(x => x.Access).First();


            Assert.True(acesso.Namespace.Equals(resultadoEsperado.Namespace) && acesso.Name.Equals(resultadoEsperado.Name));

        }

        [Fact]
        public void ClassSource_HasParameterOfTargetClass_Success()
        {
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithParametersInMethod(NamespaceSource, listaAcessos);
            var targetClass = Classes.GetClassWithOneMethod(NamespaceTarget);           

            var resultadoEsperado = new EntityDto { Name = NameOfTargetClass, Namespace = NamespaceTarget };

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var parametro = classeSource!.Functions.SelectMany(x => x.Parameters).First();


            Assert.True(parametro.Namespace.Equals(resultadoEsperado.Namespace) && parametro.Name.Equals(resultadoEsperado.Name));

        }

        [Fact]
        public void ClassSource_HasLocalTypeOfTargetClass_Success()
        {
            #region Arrange
            var listaTiposLocais = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };
            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaTiposLocais);
            var targetClass = Classes.GetClassWithOneMethod(NamespaceTarget);

            var resultadoEsperado = new EntityDto { Name = NameOfTargetClass, Namespace = NamespaceTarget };

            #endregion

            #region Act
            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var tipoLocal = classeSource!.Functions.SelectMany(x => x.LocalTypes).First();
            #endregion

            #region Assert

            Assert.True(tipoLocal.Namespace.Equals(resultadoEsperado.Namespace) && tipoLocal.Name.Equals(resultadoEsperado.Name));
            #endregion
        }

        [Fact]
        public void ClassSource_ThrowTypeOfTargetClass_Success()
        {
            var listaLancamentos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };
            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaLancamentos);
            var targetClass = Classes.GetClassException(NamespaceTarget);

            var solution = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var resultadoEsperado = new EntityDto { Name = NameOfTargetClass, Namespace = NamespaceTarget };


            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var tipoLocal = classeSource!.Functions.SelectMany(x => x.Throws).First();


            Assert.True(tipoLocal.Namespace.Equals(resultadoEsperado.Namespace) && tipoLocal.Name.Equals(resultadoEsperado.Name));

        }
    }
}
