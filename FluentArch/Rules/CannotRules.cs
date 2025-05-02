using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.Result;
using FluentArch.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class CannotRules : ICannotRules
    {

        private readonly CreateRules _createRules;
        private readonly AccessRules _accessRules;
        private readonly DeclareRules _declareRules;
        private readonly ExtendsRules _extendsRules;
        private readonly ImplementsRules _implementsRules;
        private readonly ThrowRules _throwRules;

        private ICompleteRule _builder;

        public CannotRules(ICompleteRule builder)
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
            var layer = Architecture.GetInstance().AreClasses().ResideInNamespace(namespacePath);

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
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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
            var layerTarget = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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

        //DUVIDA, MUST hANDLE DEVE ACESSAR E CRIAR OU ACESSAR OU CRIAR?
        public IConcatRules Handle(string namespacePath)
        {
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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
            var layer = Architecture.GetInstance().Classes().ResideInNamespace(namespacePath);

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
            throw new NotImplementedException();
        }

        public IConcatRules Depend(ILayer layer)
        {
            throw new NotImplementedException();
        }
    }
}
