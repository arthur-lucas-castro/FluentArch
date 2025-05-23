
using FluentArch.Rules.Interfaces.Restrictions;

namespace FluentArch.Rules.Interfaces
{
    public interface IRules
    {
        IRestrictions Must();
        IRestrictions CanOnly();
        IRestrictions Cannot();
        IRestrictions OnlyCan();
    }
}
