using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.ASTs
{
    public class ClassVisitorTest
    {
        private const string NameOfTargetClass = "TargetClass";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";

        [Fact]
        public void ClassSource_ExtendsTargetClass_Success()
        {
            #region Arrange
            var tipoHeranca = new ClassAndNamespace
            {
                Name = NameOfTargetClass,
                NamespacePath = NamespaceTarget,
            };


            var classSource = ClasseSourceHelper.ClassSourceExtends(NamespaceSource, tipoHeranca);
            var targetClass = Classes.GetClassEmpty(NamespaceTarget);

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };

            #endregion

            #region Arrange
            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var heranca = classeSource!.Heranca;
            #endregion

            #region Arrange

            Assert.True(heranca!.Namespace.Equals(resultadoEsperado.Namespace) && heranca!.Nome.Equals(resultadoEsperado.Nome));
            #endregion
        }

        [Fact]
        public void ClassSource_ImplementsInterfaceTarget_Success()
        {
            #region Arrange
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplements(NamespaceSource, listaInterfaces);
            var targetClass = Classes.GetIClassEmpty(NamespaceTarget);

            var resultadoEsperado = new EntityDto { Nome = $"I{NameOfTargetClass}", Namespace = NamespaceTarget };

            #endregion

            #region Arrange
            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var @interface = classeSource!.Interfaces.First();
            #endregion

            #region Arrange

            Assert.True(@interface.Namespace.Equals(resultadoEsperado.Namespace) && @interface.Nome.Equals(resultadoEsperado.Nome));
            #endregion
        }

        [Fact]
        public void ClassSource_WithFieldOfTypeTargetClass_Success()
        {
            #region Arrange

            var listaFields = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };
            var classSource = ClasseSourceHelper.ClassSourceWithFields(NamespaceSource, listaFields);
            var targetClass = Classes.GetClassWithOneMethod(NamespaceTarget);

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };

            #endregion

            #region Arrange
            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var field = classeSource!.Propriedades.First();
            #endregion

            #region Arrange

            Assert.True(field.Namespace.Equals(resultadoEsperado.Namespace) && field.Nome.Equals(resultadoEsperado.Nome));
            #endregion
        }

        [Fact]
        public void ClassSource_WithPropertyOfTypeTargetClass_Success()
        {
            #region Arrange
            var listaProperties = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithProperties(NamespaceSource, listaProperties);
            var targetClass = Classes.GetClassWithOneMethod(NamespaceTarget);

            var resultadoEsperado = new EntityDto { Nome = NameOfTargetClass, Namespace = NamespaceTarget };

            #endregion

            #region Arrange
            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, targetClass });

            var classeSource = dadosClasse.FirstOrDefault(c => c.Namespace.Equals(NamespaceSource));

            var property = classeSource!.Propriedades.First();
            #endregion

            #region Arrange

            Assert.True(property.Namespace.Equals(resultadoEsperado.Namespace) && property.Nome.Equals(resultadoEsperado.Nome));
            #endregion
        }
    }
}
