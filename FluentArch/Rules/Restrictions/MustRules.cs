using FluentArch.Arch;
using FluentArch.Layers;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using FluentArch.Rules.Interfaces.Restrictions;

namespace FluentArch.Rules.Restrictions
{
    public class MustRules : IRestrictions
    {
        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;

        private IRuleBuilder _builder;

        public MustRules(IRuleBuilder builder) 
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

            var violations = _accessRules.MustAccess(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Access(ILayer layer)
        {
            var violations = _accessRules.MustAccess(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _declareRules.MustDeclare(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Declare(ILayer layer)
        {
            var violations = _declareRules.MustDeclare(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _createRules.MustCreate(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Create(ILayer layerTarget)
        {
            var violations = _createRules.MustCreate(_builder.GetTypes(), layerTarget);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(string namespacePath)
        {
 
            var violations = _extendsRules.MustExtends(_builder.GetTypes(), namespacePath);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Extends(ILayer layer)
        {
            var violations = _extendsRules.MustExtends(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _implementsRules.MustImplements(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Implements(ILayer layer)
        {
            var violations = _implementsRules.MustImplements(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder); 
        }
        public IConcatRules Throws(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _throwRules.MustThrow(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }
        public IConcatRules Throws(ILayer layer)
        {
            var violations = _throwRules.MustThrow(_builder.GetTypes(), layer);

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Handle(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _accessRules.MustAccess(_builder.GetTypes(), layer);

            violations.AddRange(_declareRules.MustDeclare(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Handle(ILayer layer)
        {
            var violations = _accessRules.MustAccess(_builder.GetTypes(), layer);

            violations.AddRange(_declareRules.MustDeclare(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _extendsRules.MustExtends(_builder.GetTypes(), namespacePath);

            violations.AddRange(_implementsRules.MustImplements(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Derive(ILayer layer)
        {
            var violations = _extendsRules.MustExtends(_builder.GetTypes(), layer);

            violations.AddRange(_implementsRules.MustImplements(_builder.GetTypes(), layer));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(string namespacePath)
        {
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath).As(namespacePath);

            var violations = _accessRules.MustAccess(_builder.GetTypes(), layerTarget);
            violations.AddRange(_declareRules.MustDeclare(_builder.GetTypes(), layerTarget));
            violations.AddRange(_createRules.MustCreate(_builder.GetTypes(), layerTarget));
            violations.AddRange(_extendsRules.MustExtends(_builder.GetTypes(), namespacePath));
            violations.AddRange(_implementsRules.MustImplements(_builder.GetTypes(), layerTarget));
            violations.AddRange(_throwRules.MustThrow(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

        public IConcatRules Depend(ILayer layerTarget)
        {
            var violations = _accessRules.MustAccess(_builder.GetTypes(), layerTarget);
            violations.AddRange(_declareRules.MustDeclare(_builder.GetTypes(), layerTarget));
            violations.AddRange(_createRules.MustCreate(_builder.GetTypes(), layerTarget));
            violations.AddRange(_extendsRules.MustExtends(_builder.GetTypes(), layerTarget));
            violations.AddRange(_implementsRules.MustImplements(_builder.GetTypes(), layerTarget));
            violations.AddRange(_throwRules.MustThrow(_builder.GetTypes(), layerTarget));

            _builder.AddResults(new ConditionResult(!violations.Any(), violations));

            return new Rules(_builder);
        }

    }
}
