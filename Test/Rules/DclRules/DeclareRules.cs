using FluentArch.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.Rules.DclRules
{
    public class DeclareRules
    {
        private const string NameOfTargetClass = "TargetClass";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";

        [Fact]
        public void DeclareRules_CannnotDeclareLocalTypesTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var listaDeclaracoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaDeclaracoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Declare(NamespaceTarget).GetResult();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful && result._violacoes.First().ClassName.Equals("ClassSource") && result._violacoes.First().Violations.First().Namespace.Equals(NamespaceTarget));
            #endregion
        }
        [Fact]
        public void DeclareRules_CannnotDeclareLocalTypesTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaDeclaracoes = new List<ClassAndNamespace>
            {
            };

            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaDeclaracoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Cannot().Declare(NamespaceTarget).GetResult();
            #endregion

            #region Assert

            Assert.True(result.IsSuccessful && !result._violacoes.Any());
            #endregion
        }

        [Fact]
        public void DeclareRules_DeclareLocalTypesOnlyTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceThatClasseSourceDeclare = "Project.NamespaceThatClasseSourceDeclare";
            var nameBaseClass = "BaseClass";
            var listaDeclaracoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameBaseClass,
                    NamespacePath = namespaceThatClasseSourceDeclare,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaDeclaracoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classThatClasseSourceDeclare = Classes.GetClassWithOneMethod(namespaceThatClasseSourceDeclare, nameBaseClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget, classThatClasseSourceDeclare }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.CanOnly().Declare(NamespaceTarget).GetResult();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful && result._violacoes.First().ClassName.Equals("ClassSource") && result._violacoes.First().Violations.First().Namespace.Equals(namespaceThatClasseSourceDeclare));
            #endregion
        }

        [Fact]
        public void DeclareRules_DeclareLocalTypesOnlyTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaDeclaracoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaDeclaracoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.CanOnly().Declare(NamespaceTarget).GetResult();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result._violacoes.Any());
            #endregion
        }
        [Fact]
        public void DeclareRules_MustLocalTypesDeclareTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceThatClasseSourceDeclare = "Project.NamespaceThatClasseSourceDeclare";
            var nameBaseClass = "BaseClass";
            var listaDeclaracoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = nameBaseClass,
                    NamespacePath = namespaceThatClasseSourceDeclare,
                },

            };

            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaDeclaracoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classThatClasseSourceDeclare = Classes.GetClassWithOneMethod(namespaceThatClasseSourceDeclare, nameBaseClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Must().Declare(NamespaceTarget).GetResult();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful);
            #endregion
        }

        [Fact]
        public void DeclareRules_MustDeclareLocalTypesTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaDeclaracoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }

            };

            var classSource = ClasseSourceHelper.ClassSourceWithLocalTypes(NamespaceSource, listaDeclaracoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Must().Declare(NamespaceTarget).GetResult();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result._violacoes.Any());
            #endregion
        }
    }
}
