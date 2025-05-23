using FluentArch.Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Rules.Interfaces
{
    public interface IConcatFilters 
    {
        IFilters And();
        ILayer DefineAsLayer();
        IMustRules Must();

    }
}
