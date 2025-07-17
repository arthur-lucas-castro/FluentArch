
using FluentArch.Conditions.Interfaces.Restrictions;

namespace FluentArch.Conditions.Interfaces
{
    public interface IRules
    {
        IRestrictions Must();
        IRestrictions CanOnly();
        IRestrictions Cannot();
        IRestrictions OnlyCan();
    }
}
