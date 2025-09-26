using FluentArch.Arch;
using FluentArch.Conditions;
using FluentArch.Layers;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.Rules.DclRules
{
    public class ImplementsRulesTest
    {
        private const string NameOfTargetInterface = "ITargetInterface";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";

        public ImplementsRulesTest()
        {
            Architecture.Reset();
        }

        [Fact]
        public void ImplementsRules_CannotImplementsTargetInterface_RuleIsNotValid()
        {
            #region Arrange
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetInterface,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.First().ClassThatVioletesRule.Equals("ClassSource"));
            #endregion
        }

        [Fact]
        public void ImplementsRules_CannotImplementsTargetInterface_RuleIsValid()
        {
            #region Arrange
            var listaInterfaces = new List<ClassAndNamespace>();

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ImplementsRules_ImplementsOnlyTargetInterface_RuleIsNotValid()
        {
            #region Arrange
            var namespaceOther = "Project.NamespaceOther";
            var nameOtherInterface = "OtherInterface";
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetInterface,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameOtherInterface,
                    NamespacePath = namespaceOther,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);
            var interfaceOther = Classes.GetInterface(namespaceOther, nameOtherInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget, interfaceOther }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.CanOnly().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ImplementsRules_ImplementsOnlyTargetInterface_RuleIsValid()
        {
            #region Arrange
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetInterface,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.CanOnly().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ImplementsRules_MustImplementsTargetInterface_RuleIsNotValid()
        {
            #region Arrange
            var namespaceOther = "Project.NamespaceOther";
            var nameOtherInterface = "OtherInterface";
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = nameOtherInterface,
                    NamespacePath = namespaceOther,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);
            var interfaceOther = Classes.GetInterface(namespaceOther, nameOtherInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget, interfaceOther }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Must().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful);
            #endregion
        }

        [Fact]
        public void ImplementsRules_MustImplementsTargetInterface_RuleIsValid()
        {
            #region Arrange
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetInterface,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Must().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ImplementsRules_OnlySourceClassCanImplementTargetInterface_RuleIsValid()
        {
            #region Arrange
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetInterface,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget }));

            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.OnlyCan().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void ImplementsRules_OnlySourceClassCanImplementTargetInterface_RuleIsNotValid()
        {
            #region Arrange
            var namespaceOther = "Project.NamespaceOther";
            var nameOtherInterface = "OtherInterface";
            var listaInterfaces = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetInterface,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameOtherInterface,
                    NamespacePath = namespaceOther,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceImplementsInterfaces(NamespaceSource, listaInterfaces);
            var interfaceTarget = Classes.GetInterface(NamespaceTarget, NameOfTargetInterface);
            var classOther = Classes.ClassImplementsInterfaces(namespaceOther, listaInterfaces);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, interfaceTarget, classOther }));

            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.OnlyCan().Implements(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.Any());
            #endregion
        }
    }
}


