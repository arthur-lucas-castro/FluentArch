using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Utils
{
    public static class ErrorDescriptionFormarter
    {
        public static string FormatarErrorDescription(string descriptionBase, string[] agrs)
        {
            return string.Format(descriptionBase, agrs);
        }
    }
}
