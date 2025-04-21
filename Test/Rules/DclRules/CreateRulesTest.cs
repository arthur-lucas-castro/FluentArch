using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Test.ClassHelpers;
using Test.Helpers;

namespace Test.Rules.DclRules
{
    public class CreateRulesTest
    {
        private const string NameOfTargetClass = "TargetClass";
        private const string NamespaceSource = "Project.NamespaceSource";
        private const string NamespaceTarget = "Project.NamespaceTarget";
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

            var layerTarget = new Layer(dadosClasse.Where(x=> x.Namespace.Equals(NamespaceTarget)), "");
            var layerSource = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceSource)), "");

            var createRules = new CreateRules();


            #endregion

            #region Act
            var rule = createRules.CannotCreate(layerTarget, layerSource._classes);
            #endregion

            #region Assert

            Assert.True(!rule.IsSuccessful && rule._violacoes.First().ClassName.Equals("ClassSource") && rule._violacoes.First().Violations.First().Namespace.Equals(NamespaceTarget));
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

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, classTarget });

            var layerTarget = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceTarget)), "");
            var layerSource = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceSource)), "");

            var createRules = new CreateRules();


            #endregion

            #region Act
            var rule = createRules.CannotCreate(layerTarget, layerSource._classes);
            #endregion

            #region Assert

            Assert.True(rule.IsSuccessful && !rule._violacoes.Any());
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

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, classTarget, classThatClasseSourceCreate });

            var layerTarget = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceTarget)), "");
            var layerSource = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceSource)), "");

            var createRules = new CreateRules();


            #endregion

            #region Act
            var rule = createRules.CreateOnly(layerTarget, layerSource._classes);
            #endregion

            #region Assert

            Assert.True(!rule.IsSuccessful && rule._violacoes.First().ClassName.Equals("ClassSource") && rule._violacoes.First().Violations.First().Namespace.Equals(namespaceThatClasseSourceCreate));
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

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, classTarget });

            var layerTarget = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceTarget)), "");
            var layerSource = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceSource)), "");

            var createRules = new CreateRules();


            #endregion

            #region Act
            var rule = createRules.CreateOnly(layerTarget, layerSource._classes);
            #endregion

            #region Assert
            Assert.True(rule.IsSuccessful && !rule._violacoes.Any());
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

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, classTarget, classThatClasseSourceCreate });

            var layerTarget = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceTarget)), "");
            var layerSource = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceSource)), "");

            var createRules = new CreateRules();


            #endregion

            #region Act
            var rule = createRules.CreateOnly(layerTarget, layerSource._classes);
            #endregion

            #region Assert

            Assert.True(!rule.IsSuccessful);
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

            var dadosClasse = SolutionHelper.ObterDadosDaClasse(new List<string> { classSource, classTarget });

            var layerTarget = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceTarget)), "");
            var layerSource = new Layer(dadosClasse.Where(x => x.Namespace.Equals(NamespaceSource)), "");

            var createRules = new CreateRules();


            #endregion

            #region Act
            var rule = createRules.CreateOnly(layerTarget, layerSource._classes);
            #endregion

            #region Assert
            Assert.True(rule.IsSuccessful && !rule._violacoes.Any());
            #endregion
        }
    }
}
