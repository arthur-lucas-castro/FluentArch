using FluentArch.Arch;
using FluentArch.Layers;
using FluentArch.Result;
using FluentArch.Conditions.Interfaces;
using FluentArch.Conditions.Interfaces.Restrictions;
using FluentArch.Utils;

namespace FluentArch.Conditions.Restrictions
{
    public class OnlyCanRules : IRestrictions
    {
        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;

        private IRuleBuilder _builder;

        public OnlyCanRules(IRuleBuilder builder)
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
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.OnlyCanAccess(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Access(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.OnlyCanAccess(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(string namespacePath)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _declareRules.OnlyCanDeclare(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _declareRules.OnlyCanDeclare(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(string namespacePath)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _createRules.OnlyCanCreate(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _createRules.OnlyCanCreate(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.OnlyCanExtends(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.OnlyCanExtends(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _implementsRules.OnlyCanImplements(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _implementsRules.OnlyCanImplements(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _throwRules.OnlyCanThrow(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _throwRules.OnlyCanThrow(allClassesExceptLayerSource, layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Handle(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.OnlyCanAccess(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_declareRules.OnlyCanDeclare(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Handle(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Except(_builder.GetTypes()).ToList();

            var violations = _accessRules.OnlyCanAccess(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_declareRules.OnlyCanDeclare(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));


            return new Rules(_builder);
        }

        public IConcatRules Derive(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.OnlyCanExtends(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_implementsRules.OnlyCanImplements(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _extendsRules.OnlyCanExtends(allClassesExceptLayerSource, layerTarget);

            violations.AddRange(_implementsRules.OnlyCanImplements(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class => !@class.CompareClassAndNamespace(_builder.GetTypes())).ToList();

            var violations = _accessRules.OnlyCanAccess(allClassesExceptLayerSource, layerTarget);
            violations.AddRange(_declareRules.OnlyCanDeclare(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_createRules.OnlyCanCreate(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_extendsRules.OnlyCanExtends(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_implementsRules.OnlyCanImplements(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_throwRules.OnlyCanThrow(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(ILayer layerTarget)
        {
            var allClassesExceptLayerSource = Architecture.GetClasses().Where(@class =>
                !(@class.CompareClassAndNamespace(_builder.GetTypes()) || @class.CompareClassAndNamespace(layerTarget.GetTypes()))).ToList();

            var violations = _accessRules.OnlyCanAccess(allClassesExceptLayerSource, layerTarget);
            violations.AddRange(_declareRules.OnlyCanDeclare(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_createRules.OnlyCanCreate(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_extendsRules.OnlyCanExtends(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_implementsRules.OnlyCanImplements(allClassesExceptLayerSource, layerTarget));
            violations.AddRange(_throwRules.OnlyCanThrow(allClassesExceptLayerSource, layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
    }
}
