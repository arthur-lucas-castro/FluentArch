using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using FluentArch.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace FluentArch.Rules
{
    public class Rules
    {
        public readonly IEnumerable<ClassEntityDto> _classes;
        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;
        private List<PreResult> _preResults;

        public Rules(IEnumerable<ClassEntityDto> classes)
        {
            _classes = classes;
            _createRules = new CreateRules();
            _accessRules = new AccessRules();
            _declareRules = new DeclareRules();
            _extendsRules = new ExtendsRules();
            _implementsRules = new ImplementsRules();
            _throwRules = new ThrowRules();
            _preResults = new List<PreResult>();
        }
        public Rules(IEnumerable<ClassEntityDto> classes, List<PreResult> preResults)
        {
            _classes = classes;
            _createRules = new CreateRules();
            _accessRules = new AccessRules();
            _declareRules = new DeclareRules();
            _extendsRules = new ExtendsRules();
            _implementsRules = new ImplementsRules();
            _throwRules = new ThrowRules();
            _preResults = preResults;
        }

        #region Create
        public IntermediaryRule CannotCreate(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();
            var result = _createRules.CannotCreate(layer, _classes);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }

        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public IntermediaryRule CannotCreate(Layer layer)
        {
            var result = _createRules.CannotCreate(layer, _classes);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }

        public IntermediaryRule CanCreateOnly(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            var result = _createRules.CreateOnly(layerTarget, _classes);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }
        public IntermediaryRule CanCreateOnly(Layer layer)
        {
            var result = _createRules.CreateOnly(layer, _classes);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }

        public IntermediaryRule OnlyCanCreate(string namespacePath)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Except(_classes).ToList();

            var layerTarget = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            var result = _createRules.CannotCreate(layerTarget, allClassesExceptLayerSource);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }

        public IntermediaryRule OnlyCanCreate(Layer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Except(_classes).ToList();

            var result = _createRules.CannotCreate(layerTarget, allClassesExceptLayerSource);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public IntermediaryRule MustCreate(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            var result = _createRules.MustCreate(_classes, layerTarget);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }
        public IntermediaryRule MustCreate(Layer layerTarget)
        {
            var result = _createRules.MustCreate(_classes, layerTarget);
            _preResults.Add(result);
            return new IntermediaryRule(_classes, _preResults);
        }
        #endregion

        #region Access
        public bool CannotAccess(Layer layer)
        {
            return !_accessRules.Access(layer, _classes);
        }
        public bool CannotAccess(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return !_accessRules.Access(layer, _classes);
        }
        public bool CanAccessOnly(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _accessRules.AccessOnly(layer, _classes);
        }
        public bool CanAccessOnly(Layer layer)
        {
            return _accessRules.AccessOnly(layer, _classes);
        }

        public bool OnlyCanAccess(string namespacePath)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return !_accessRules.Access(layer, todasClassesExcetoModuloAtual);
        }

        public bool OnlyCanAccess(Layer layer)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_accessRules.Access(layer, todasClassesExcetoModuloAtual);
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public bool MustAccess(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _accessRules.MustAccess(layer, _classes);
        }
        public bool MustAccess(Layer layer)
        {
            return _accessRules.MustAccess(layer, _classes);
        }
        #endregion

        #region Declare
        public bool CannotDeclare(Layer layer)
        {

            return !_declareRules.Declare(layer, _classes);
        }
        public bool CannotDeclare(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return !_declareRules.Declare(layer, _classes);
        }
        public bool CanDeclareOnly(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _declareRules.DeclareOnly(layer, _classes);
        }
        public bool CanDeclareOnly(Layer layer)
        {
            return _declareRules.DeclareOnly(layer, _classes);
        }

        public bool OnlyCanDeclare(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToArray();

            return !_declareRules.Declare(layer, todasClassesExcetoModuloAtual);
        }

        public bool OnlyCanDeclare(Layer layer)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToArray();

            return !_declareRules.Declare(layer, todasClassesExcetoModuloAtual);
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public bool MustDeclare(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _declareRules.MustDeclare(layer, _classes);
        }
        public bool MustDeclare(Layer layer)
        {
            return _declareRules.MustDeclare(layer, _classes);
        }
        #endregion

        #region Extends
        public bool CannotExtends(Layer layer)
        {
            return !_extendsRules.Extends(layer, _classes);
        }
        public bool CannotExtends(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();
 
            return !_extendsRules.Extends(layer, _classes);
        }
        public bool CanExtendsOnly(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _extendsRules.ExtendsOnly(layer, _classes);
        }
        public bool CanExtendsOnly(Layer layer)
        {
            return _extendsRules.ExtendsOnly(layer, _classes);
        }

        public bool OnlyCanExtends(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_extendsRules.Extends(layer, todasClassesExcetoModuloAtual);
        }

        public bool OnlyCanExtends(Layer layer)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_extendsRules.Extends(layer, todasClassesExcetoModuloAtual);
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public bool MustExtends(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _extendsRules.MustExtends(layer, _classes);
        }
        public bool MustExtends(Layer layer)
        {
            return _extendsRules.MustExtends(layer, _classes); ;
        }
        #endregion

        #region Implements
        public bool CannotImplements(Layer layer)
        {
            return !_implementsRules.Implements(layer, _classes);
        }
        public bool CannotImplements(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return !_implementsRules.Implements(layer, _classes);
        }
        public bool CanImplementsOnly(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _implementsRules.ImplementsOnly(layer, _classes);
        }
        public bool CanImplementsOnly(Layer layer)
        {
            return _implementsRules.ImplementsOnly(layer, _classes);
        }

        public bool OnlyCanImplements(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_implementsRules.Implements(layer, todasClassesExcetoModuloAtual);
        }

        public bool OnlyCanImplements(Layer layer)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_implementsRules.Implements(layer, todasClassesExcetoModuloAtual);
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public bool MustImplements(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _implementsRules.MustImplements(layer, _classes);
        }
        public bool MustImplements(Layer layer)
        {
            return _implementsRules.MustImplements(layer, _classes);
        }
        #endregion

        #region Throw
        public bool CannotThrows(Layer layer)
        {
            return !_throwRules.Throw(layer, _classes);
        }
        public bool CannotThrows(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return !_throwRules.Throw(layer, _classes);
        }
        public bool CanThrowsOnly(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _throwRules.ThrowsOnly(layer, _classes);
        }
        public bool CanThrowsOnly(Layer layer)
        {
            return _throwRules.ThrowsOnly(layer, _classes);
        }

        public bool OnlyCanThrows(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_throwRules.Throw(layer, todasClassesExcetoModuloAtual);
        }

        public bool OnlyCanThrows(Layer layer)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToList();

            return !_throwRules.Throw(layer, todasClassesExcetoModuloAtual);
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public bool MustThrows(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().That().ResideInNamespace(namespacePath).DefineAsLayer();

            return _throwRules.MustThrow(layer, _classes);
        }
        public bool MustThrows(Layer layer)
        {
            return _throwRules.MustThrow(layer, _classes);
        }
        #endregion

        #region Custom Rule
        public bool UseCustomRule(ICustomRule customRule)
        {
            return customRule.DefineCustomRule();
        }
        #endregion
    }
}
