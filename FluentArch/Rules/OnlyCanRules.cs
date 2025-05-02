using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using FluentArch.Utils;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace FluentArch.Rules
{
    public class OnlyCanRules :IOnlyCanRules
    {

        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;

        private ICompleteRule _builder;

        public OnlyCanRules(ICompleteRule builder)
        {
            _builder = builder;
            _createRules = new CreateRules();
            _accessRules = new AccessRules();
            _declareRules = new DeclareRules();
            _extendsRules = new ExtendsRules();
            _implementsRules = new ImplementsRules();
            _throwRules = new ThrowRules();
        }
        public IConcatRules Access(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.CannotAccess(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Access(ILayer layer)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.CannotAccess(allClassesExceptLayerSource, layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(string namespacePath)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var violations = _declareRules.CannotDeclare(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _declareRules.CannotDeclare(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(string namespacePath)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var violations = _createRules.CannotCreate(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _createRules.CannotCreate(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.CannotExtends(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.CannotExtends(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _implementsRules.CannotImplements(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _implementsRules.CannotImplements(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _throwRules.CannotThrow(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _throwRules.CannotThrow(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        //DUVIDA, MUST hANDLE DEVE ACESSAR E CRIAR OU ACESSAR OU CRIAR?
        public IConcatRules Handle(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.CannotAccess(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_declareRules.CannotDeclare(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Handle(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Except(_builder.GetTypes()).ToList();

            var violations = _accessRules.CannotAccess(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_declareRules.CannotDeclare(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));


            return new Rules(_builder);
        }

        public IConcatRules Derive(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.CannotExtends(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_implementsRules.CannotImplements(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.CannotExtends(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_implementsRules.CannotImplements(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(string namespacePath)
        {
            throw new NotImplementedException();
        }

        public IConcatRules Depend(ILayer layer)
        {
            throw new NotImplementedException();
        }
    }
}
