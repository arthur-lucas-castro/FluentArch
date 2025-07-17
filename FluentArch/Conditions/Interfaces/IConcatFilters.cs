using FluentArch.Layers;

namespace FluentArch.Conditions.Interfaces
{
    public interface IConcatFilters 
    {
        IFilters And();
        ILayer DefineAsLayer();
        IMustRules Must();

    }
}
