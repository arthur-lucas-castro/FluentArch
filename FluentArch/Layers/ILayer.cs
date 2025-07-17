using FluentArch.DTO;
using FluentArch.Conditions.Interfaces;
using FluentArch.Conditions.Interfaces.Restrictions;

namespace FluentArch.Layers
{
    public interface ILayer
    {
        string GetName();
        ILayer As(string name);
        IFilters And();
        IRestrictions Must();
        IRestrictions CanOnly();
        IRestrictions Cannot();
        IRestrictions OnlyCan();
        IConcatRules UseCustomRule(ICustomRule customRule);
        public List<TypeEntityDto> GetTypes();
    }
}
