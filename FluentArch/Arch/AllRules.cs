using FluentArch.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Arch
{
    internal class AllRules
    {
        private static List<ArchRule> Rules = new List<ArchRule>();
        public static void AddRule(ArchRule rule)
        {
            Rules.Add(rule);
        }
    }
}
