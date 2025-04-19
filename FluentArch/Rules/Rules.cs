using FluentArch.Arch;
using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules
{
    public class Rules
    {
        public readonly IEnumerable<ClassEntityDto> _classes;
        private readonly CreateRules _createRules;
        private readonly bool _isNegative;
        public Rules(IEnumerable<ClassEntityDto> classes, bool isNegative)
        {
            _classes = classes;
            _createRules = new CreateRules();
            _isNegative = isNegative;
        }

        #region Create
        public bool CannotCreate(string namespacePath)
        {
            var result = !_createRules.Create(namespacePath, _classes);
            return _isNegative ? !result : result;
        }

        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public bool CannotCreate(Layer layer)
        {
            var result = !_createRules.Create(layer, _classes);
            return _isNegative ? !result : result;
        }

        public bool CanCreateOnly(string namespacePath)
        {
            var result = _createRules.OnlyCreate(namespacePath, _classes);
            return _isNegative ? !result : result;
        }
        public bool CanCreateOnly(Layer layer)
        {
            var result = _createRules.OnlyCreate(layer, _classes);
            return _isNegative ? !result : result;
        }

        public bool OnlyCanCreate(string namespacePath)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToArray();
            var result = !_createRules.Create(namespacePath, todasClassesExcetoModuloAtual);
            return result;
        }

        public bool OnlyCanCreate(Layer layer)
        {
            var todasClassesExcetoModuloAtual = Architecture.GetClasses().Except(_classes).ToArray();
            var result = !_createRules.Create(layer, todasClassesExcetoModuloAtual);
            return result;
        }

        //DUVIDA: Todas classes do modulo A devem acessar o modulo B?
        public bool MustCreate(string namespacePath)
        {
            var result = _createRules.MustCreate(namespacePath, _classes);
            return _isNegative ? !result : result;
        }
        public bool MustCreate(Layer layer)
        {
            var result = _createRules.MustCreate(layer, _classes);
            return _isNegative ? !result : result;
        }
        #endregion

        #region Access
        public bool CannotAcess(Layer layer)
        {
            var result = _createRules.MustCreate(layer, _classes);
            return _isNegative ? !result : result;
        }


        #endregion
    }
}
