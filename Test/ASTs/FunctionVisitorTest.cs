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

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };


            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var criacao = classeSource!.Funcoes.SelectMany(x => x.Criacoes).First();


            Assert.True(criacao.Namespace.Equals(resultadoEsperado.Namespace) && criacao.Nome.Equals(resultadoEsperado.Nome));

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

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };


            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var acesso = classeSource!.Funcoes.SelectMany(x => x.Acessos).First();


            Assert.True(acesso.Namespace.Equals(resultadoEsperado.Namespace) && acesso.Nome.Equals(resultadoEsperado.Nome));

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

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var parametro = classeSource!.Funcoes.SelectMany(x => x.Parametros).First();


            Assert.True(parametro.Namespace.Equals(resultadoEsperado.Namespace) && parametro.Nome.Equals(resultadoEsperado.Nome));

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

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };

            #endregion

            #region Act
            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var tipoLocal = classeSource!.Funcoes.SelectMany(x => x.TiposLocais).First();
            #endregion

            #region Assert

            Assert.True(tipoLocal.Namespace.Equals(resultadoEsperado.Namespace) && tipoLocal.Nome.Equals(resultadoEsperado.Nome));
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

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };


            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var tipoLocal = classeSource!.Funcoes.SelectMany(x => x.Lancamentos).First();


            Assert.True(tipoLocal.Namespace.Equals(resultadoEsperado.Namespace) && tipoLocal.Nome.Equals(resultadoEsperado.Nome));

        }
    }
}
