using FluentArch.Arch;
using FluentArch.Rules;
using FluentArch.Layers;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.Rules.DclRules
{
    public class AccessRulesTest
    {
        private const string NameOfTargetClass = "TargetClass";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";

        [Fact]
        public void AccessRules_CannnotAccessTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Access(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful && result.Violations.First().ClassThatVioletesRule.Equals("ClassSource") && result.Violations.First().Violations.First().Namespace.Equals(NamespaceTarget));
            #endregion
        }
        [Fact]
        public void AccessRules_CannnotAccessTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaAcessos = new List<ClassAndNamespace>
            {
            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            var createRules = new AccessRules();


            #endregion

            #region Act
            var result = layerSource.Cannot().Access(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void AccessRules_AccessOnlyTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceThatClasseSourceAccess = "Project.NamespaceThatClasseSourceAccess";
            var nameBaseClass = "BaseClass";
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameBaseClass,
                    NamespacePath = namespaceThatClasseSourceAccess,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classThatClasseSourceAccess = Classes.GetClassWithOneMethod(namespaceThatClasseSourceAccess, nameBaseClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget, classThatClasseSourceAccess }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.CanOnly().Access(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful && result.Violations.First().ClassThatVioletesRule.Equals("ClassSource") && result.Violations.First().Violations.First().Namespace.Equals(namespaceThatClasseSourceAccess));
            #endregion
        }

        [Fact]
        public void AccessRules_AccessOnlyTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.CanOnly().Access(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }
        [Fact]
        public void AccessRules_MustAccessTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceThatClasseSourceAccess = "Project.NamespaceThatClasseSourceAccess";
            var nameBaseClass = "BaseClass";
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = nameBaseClass,
                    NamespacePath = namespaceThatClasseSourceAccess,
                },

            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classThatClasseSourceAccess = Classes.GetClassWithOneMethod(namespaceThatClasseSourceAccess, nameBaseClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Must().Access(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful);
            #endregion
        }

        [Fact]
        public void AccessRules_MustAccessTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }

            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Must().Access(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }
        [Fact]
        public void AccessRules_OnlySourceClassCanAccessTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }

            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.OnlyCan().Access(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void AccessRules_OnlySourceClassCanAccessTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceBaseClass = "Project.NamespaceBaseClass";
            var nameBaseClass = "BaseClass";
            var listaAcessos = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }

            };

            var classSource = ClasseSourceHelper.ClassSourceAccessMethodOfClasses(NamespaceSource, listaAcessos);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var baseClass = Classes.ClassAccessMethodOfClasses(namespaceBaseClass, nameBaseClass, listaAcessos);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget, baseClass }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.OnlyCan().Access(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.Any());
            #endregion
        }
    }
}
