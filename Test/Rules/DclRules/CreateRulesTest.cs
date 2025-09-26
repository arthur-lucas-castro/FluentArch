using FluentArch.Arch;
using FluentArch.Conditions;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.Rules.DclRules
{

    public class CreateRulesTest : IDisposable
    {
        private const string NameOfTargetClass = "TargetClass";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";


        public CreateRulesTest()
        {
            Architecture.Reset();
        }
        public void Dispose()
        {
            Architecture.Reset();
        }
        [Fact]
        public void CreateRules_CannnotCreateTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var listaCriacoes = new List<ClassAndNamespace> 
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, classTarget });

            var createRules = new CreateRules();

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.Cannot().Create(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful && result.Violations.First().ClassThatVioletesRule.Equals("ClassSource") && result.Violations.First().Violations.First().Namespace.Equals(NamespaceTarget));
            #endregion
        }

        [Fact]
        public void CreateRules_CannnotCreateTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaCriacoes = new List<ClassAndNamespace>
            {
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            var createRules = new CreateRules();


            #endregion

            #region Act
            var result = layerSource.Cannot().Create(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void CreateRules_CreateOnlyTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceThatClasseSourceCreate = "Project.NamespaceThatClasseSourceCreate";
            var nameBaseClass = "BaseClass";
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameBaseClass,
                    NamespacePath = namespaceThatClasseSourceCreate,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes );
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classThatClasseSourceCreate = Classes.GetClassWithOneMethod(namespaceThatClasseSourceCreate, nameBaseClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget, classThatClasseSourceCreate }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.CanOnly().Create(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful && result.Violations.First().ClassThatVioletesRule.Equals("ClassSource") && result.Violations.First().Violations.First().Namespace.Equals(namespaceThatClasseSourceCreate));
            #endregion
        }

        [Fact]
        public void CreateRules_CreateOnlyTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.CanOnly().Create(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }
        [Fact]
        public void CreateRules_MustCreateTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceThatClasseSourceCreate = "Project.NamespaceThatClasseSourceCreate";
            var nameBaseClass = "BaseClass";
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = nameBaseClass,
                    NamespacePath = namespaceThatClasseSourceCreate,
                },

            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classThatClasseSourceCreate = Classes.GetClassWithOneMethod(namespaceThatClasseSourceCreate, nameBaseClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Must().Create(NamespaceTarget).Check();
            #endregion

            #region Assert

            Assert.True(!result.IsSuccessful);
            #endregion
        }

        [Fact]
        public void CreateRules_MustCreateTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }

            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerTarget = arch.All().ResideInNamespace(NamespaceTarget);
            var layerSource = arch.All().ResideInNamespace(NamespaceSource);

            #endregion

            #region Act
            var result = layerSource.Must().Create(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void CreateRules_OnlySourceClassCanCreateTargetClass_RuleIsValid()
        {
            #region Arrange
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget }));

            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.OnlyCan().Create(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(result.IsSuccessful && !result.Violations.Any());
            #endregion
        }

        [Fact]
        public void CreateRules_OnlySourceClassCanCreateTargetClass_RuleIsNotValid()
        {
            #region Arrange
            var namespaceOther = "Project.NamespaceOther";
            var nameOtherClass = "OtherClass";
            var listaCriacoes = new List<ClassAndNamespace>
            {
                new ClassAndNamespace
                {
                    Name = NameOfTargetClass,
                    NamespacePath = NamespaceTarget,
                },
                new ClassAndNamespace
                {
                    Name = nameOtherClass,
                    NamespacePath = namespaceOther,
                }
            };

            var classSource = ClasseSourceHelper.ClassSourceCreateClasses(NamespaceSource, listaCriacoes);
            var classTarget = Classes.GetClassWithOneMethod(NamespaceTarget);
            var classOther = Classes.ClassCreateClasses(namespaceOther, listaCriacoes, nameOtherClass);

            var arch = Architecture.Build(SolutionHelper.MontarSolution(new List<string> { classSource, classTarget, classOther }));

            var layerSource = arch.All().ResideInNamespace(NamespaceSource);
            #endregion

            #region Act
            var result = layerSource.OnlyCan().Create(NamespaceTarget).Check();
            #endregion

            #region Assert
            Assert.True(!result.IsSuccessful && result.Violations.Any());
            #endregion
        }
    }
}
