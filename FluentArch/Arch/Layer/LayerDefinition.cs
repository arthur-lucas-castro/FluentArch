using FluentArch.Filters;
using FluentArch.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch.Layer
{
    public static class LayerDefinition
    {
        public static Layer DefineAsLayer(this Rules.Rules regra, string name)
        {
            return new Layer(regra._classes, name);
        }
    }
}
