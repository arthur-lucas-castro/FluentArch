using FluentArch.Arch;
using FluentArch.Layers;
using FluentArch.Result;
using FluentArch.Conditions.Interfaces;
using FluentArch.Conditions.Interfaces.Restrictions;
using System.Linq.Expressions;

namespace FluentArch.Conditions.Restrictions
{
    public class CanOnlyRules : IRestrictions
    {
        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;

        private IRuleBuilder _builder;

        public CanOnlyRules(IRuleBuilder builder)
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

            return Access(layerTarget);
        }
        public IConcatRules Access(ILayer layerTarget)
        {
            var violations = _accessRules.AccessOnly(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Declare(layerTarget);
        }
        public IConcatRules Declare(ILayer layerTarget)
        {
            var violations = _declareRules.DeclareOnly(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Create(layerTarget);
        }
        public IConcatRules Create(ILayer layerTarget)
        {
            var violations = _createRules.CreateOnly(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(string namespacePath)
        {
            var violations = _extendsRules.ExtendsOnly(_builder.GetTypes(), namespacePath);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Extends(ILayer layerTarget)
        {
            var violations = _extendsRules.ExtendsOnly(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Implements(layerTarget);
        }
        public IConcatRules Implements(ILayer layerTarget)
        {
            var violations = _implementsRules.ImplementsOnly(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Throws(layerTarget);
        }
        public IConcatRules Throws(ILayer layerTarget)
        {
            var violations = _throwRules.ThrowsOnly(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Handle(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Handle(layerTarget);
        }

        public IConcatRules Handle(ILayer layerTarget)
        {
            var violations = _accessRules.AccessOnly(_builder.GetTypes(), layerTarget);

            violations.AddRange(_declareRules.DeclareOnly(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Derive(layerTarget);
        }

        public IConcatRules Derive(ILayer layerTarget)
        {
            var violations = _extendsRules.ExtendsOnly(_builder.GetTypes(), layerTarget);

            violations.AddRange(_implementsRules.ImplementsOnly(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);
            return Depend(layerTarget);
        }

        public IConcatRules Depend(ILayer layerTarget)
        {
            var violations = _accessRules.AccessOnly(_builder.GetTypes(), layerTarget);
            violations.AddRange(_declareRules.DeclareOnly(_builder.GetTypes(), layerTarget));
            violations.AddRange(_createRules.CreateOnly(_builder.GetTypes(), layerTarget));
            violations.AddRange(_extendsRules.ExtendsOnly(_builder.GetTypes(), layerTarget));
            violations.AddRange(_implementsRules.ImplementsOnly(_builder.GetTypes(), layerTarget));
            violations.AddRange(_throwRules.ThrowsOnly(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
    }
}
