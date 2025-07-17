using FluentArch.Arch;
using FluentArch.Layers;
using FluentArch.Result;
using FluentArch.Conditions.Interfaces;
using FluentArch.Conditions.Interfaces.Restrictions;

namespace FluentArch.Conditions.Restrictions
{
    public class CannotRules : IRestrictions
    {
        private IRuleBuilder _builder;
        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;

        public CannotRules(IRuleBuilder builder)
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
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _accessRules.CannotAccess(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Access(ILayer layer)
        {
            var violations = _accessRules.CannotAccess(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _declareRules.CannotDeclare(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(ILayer layer)
        {
            var violations = _declareRules.CannotDeclare(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _createRules.CannotCreate(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(ILayer layerTarget)
        {
            var violations = _createRules.CannotCreate(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _extendsRules.CannotExtends(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(ILayer layer)
        {
            var violations = _extendsRules.CannotExtends(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _implementsRules.CannotImplements(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(ILayer layer)
        {
            var violations = _implementsRules.CannotImplements(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _throwRules.CannotThrow(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(ILayer layer)
        {
            var violations = _throwRules.CannotThrow(_builder.GetTypes(), layer);


            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Handle(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _accessRules.CannotAccess(_builder.GetTypes(), layer);

            violations.AddRange(_declareRules.CannotDeclare(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Handle(ILayer layer)
        {
            var violations = _accessRules.CannotAccess(_builder.GetTypes(), layer);

            violations.AddRange(_declareRules.CannotDeclare(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _extendsRules.CannotExtends(_builder.GetTypes(), layer);

            violations.AddRange(_implementsRules.CannotImplements(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(ILayer layer)
        {
            var violations = _extendsRules.CannotExtends(_builder.GetTypes(), layer);

            violations.AddRange(_implementsRules.CannotImplements(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _accessRules.CannotAccess(_builder.GetTypes(), layerTarget);
            violations.AddRange(_declareRules.CannotDeclare(_builder.GetTypes(), layerTarget));
            violations.AddRange(_createRules.CannotCreate(_builder.GetTypes(), layerTarget));
            violations.AddRange(_extendsRules.CannotExtends(_builder.GetTypes(), layerTarget));
            violations.AddRange(_implementsRules.CannotImplements(_builder.GetTypes(), layerTarget));
            violations.AddRange(_throwRules.CannotThrow(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(ILayer layerTarget)
        {
            var listNamespaces = layerTarget.GetTypes().SelectMany(type => type.Namespace);

            var violations = new AccessRules().CannotAccess(_builder.GetTypes(), layerTarget);
            violations.AddRange(new DeclareRules().CannotDeclare(_builder.GetTypes(), layerTarget));
            violations.AddRange(new CreateRules().CannotCreate(_builder.GetTypes(), layerTarget));
            violations.AddRange(new ExtendsRules().CannotExtends(_builder.GetTypes(), layerTarget));
            violations.AddRange(new ImplementsRules().CannotImplements(_builder.GetTypes(), layerTarget));
            violations.AddRange(new ThrowRules().CannotThrow(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

    }
}
