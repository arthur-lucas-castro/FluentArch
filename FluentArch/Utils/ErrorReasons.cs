using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Utils
{
    public static class ErrorReasons
    {
        public const string ERROR_MUST_DESCRIPTION = "The layer is expected to {0} from a class in module {1}, but the type {2} is not doing so..";
        public const string ERROR_ONLY_CAN_DESCRIPTION = "Only the specified layer is allowed to {0} from a class in module {1}, but the type {2} is also doing so.";
        public const string ERROR_CAN_ONLY_DESCRIPTION = "The layer is only allowed to {0} from a class in module {1}, but the type {2} is violating this rule.";
        public const string ERROR_CANNOT_DESCRIPTION = "The layer is not allowed to {0} a type from {1}, but type {2} is inheriting.";
        public const string ERROR_CUSTOM_RULE = "Class {0} violates the custom rule.";
    }
}
