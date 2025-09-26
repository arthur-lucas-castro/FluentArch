using FluentArch.Arch;
using FluentArch.Conditions;
using FluentArch.Layers;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.Rules.DclRules
{
    public class ThrowRulesTest
    {
        private const string NameOfTargetException = "TargetException";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";

        public ThrowRulesTest()
        {
            Architecture.Reset();
        }

        [Fact]
        public void ThrowRules_CannotThrowTargetException_RuleIsNotValid()
        {
            #region Arrange
            var listaThrows = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetException,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaThrows);
            var exceptionTarget = Classes.GetException(NamespaceTarget, NameOfTargetException);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, exceptionTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Throws(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.First().ClassThatVioletesRule.Equals("ClassSource"));
            #endregion
        }

        [Fact]
        public void ThrowRules_CannotThrowTargetException_RuleIsValid()
        {
            #region Arrange
            var listaThrows = new List<ClassAndNamespace>();

            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaThrows);
            var exceptionTarget = Classes.GetException(NamespaceTarget, NameOfTargetException);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, exceptionTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Throws(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ThrowRules_ThrowsOnlyTargetException_RuleIsNotValid()
        {
            #region Arrange
            var namespaceOther = "Project.NamespaceOther";
            var nameOtherException = "OtherException";
            var listaThrows = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetException,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameOtherException,
                    NamespacePath = namespaceOther,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaThrows);
            var exceptionTarget = Classes.GetException(NamespaceTarget, NameOfTargetException);
            var exceptionOther = Classes.GetException(namespaceOther, nameOtherException);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, exceptionTarget, exceptionOther }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.CanOnly().Throws(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ThrowRules_ThrowsOnlyTargetException_RuleIsValid()
        {
            #region Arrange
            var listaThrows = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetException,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaThrows);
            var exceptionTarget = Classes.GetException(NamespaceTarget, NameOfTargetException);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, exceptionTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.CanOnly().Throws(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ThrowRules_MustThrowTargetException_RuleIsNotValid()
        {
            #region Arrange
            var namespaceOther = "Project.NamespaceOther";
            var nameOtherException = "OtherException";
            var listaThrows = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = nameOtherException,
                    NamespacePath = namespaceOther,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaThrows);
            var exceptionTarget = Classes.GetException(NamespaceTarget, NameOfTargetException);
            var exceptionOther = Classes.GetException(namespaceOther, nameOtherException);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, exceptionTarget, exceptionOther }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Must().Throws(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful);
            #endregion
        }

        [Fact]
        public void ThrowRules_MustThrowTargetException_RuleIsValid()
        {
            #region Arrange
            var listaThrows = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetException,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceWithThrows(NamespaceSource, listaThrows);
            var exceptionTarget = Classes.GetException(NamespaceTarget, NameOfTargetException);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, exceptionTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Must().Throws(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }
    }
}
