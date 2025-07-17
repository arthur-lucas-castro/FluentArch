using FluentArch.Layers;

namespace FluentArch.Conditions.Interfaces.Restrictions
{
    public interface IRestrictions
    {
        IConcatRules Access(string namespacePath);
        IConcatRules Access(ILayer layer);
        IConcatRules Declare(string namespacePath);
        IConcatRules Declare(ILayer layer);
        IConcatRules Create(string namespacePath);
        IConcatRules Create(ILayer layerTarget);
        IConcatRules Extends(string namespacePath);
        IConcatRules Extends(ILayer layer);
        IConcatRules Implements(string namespacePath);
        IConcatRules Implements(ILayer layer);
        IConcatRules Throws(string namespacePath);
        IConcatRules Throws(ILayer layer);
        IConcatRules Handle(string namespacePath);
        IConcatRules Handle(ILayer layer);
        IConcatRules Derive(string namespacePath);
        IConcatRules Derive(ILayer layer);
        IConcatRules Depend(string namespacePath);
        IConcatRules Depend(ILayer layer);
    }
}
