using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Utils
{
    public static class CompareUtils
    {
        public static bool NamespaceCompare(this string namespaceBase, string namespaceComper)
        {
            var contemAsterisco = namespaceComper.Contains(".*");
            if (!contemAsterisco)
            {
                return namespaceBase.Equals(namespaceComper);
            }

            var namespacePreAsterisco = namespaceComper.Split(new string[] { ".*" }, StringSplitOptions.None).FirstOrDefault();

            return namespaceBase.StartsWith(namespacePreAsterisco);
        }
        public static bool CompareClassAndNamespace(this EntityDto entityBase, IEnumerable<EntityDto> entitysComper)
        {
            return entitysComper.Any(comper => comper.Namespace.Equals(entityBase.Namespace) && comper.Name.Equals(entityBase.Name));
        }

    }
}
