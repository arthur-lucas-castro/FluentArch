using FluentArch.Layers;

namespace FluentArch.Rules.Interfaces
{
    public interface IConcatFilters 
    {
        IFilters And();
        ILayer DefineAsLayer();
        IMustRules Must();

    }
}
