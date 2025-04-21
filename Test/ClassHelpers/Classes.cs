using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ClassHelpers
{
    public class Classes
    {
        public static string GetClassWithOneMethod(string namespacePath, string className = "TargetClass")
        {
            string code = @$"
            using System;

                namespace {namespacePath}
                {{
                    public class {className}
                    {{
                        public {className}(){{}}
                        public string MethodToTest() => $""Hello from {namespacePath}!"";
                    }}
                }}";

            return code;
        }

        public static string GetClassEmpty(string namespacePath, string className = "TargetClass")
        {
            string code = @$"
            using System;

                namespace {namespacePath}
                {{
                    public class {className}
                    {{
                    }}
                }}";

            return code;
        }
        public static string GetIClassEmpty(string namespacePath, string className = "TargetClass")
        {
            string code = @$"
            using System;

                namespace {namespacePath}
                {{
                    public interface I{className}
                    {{
                    }}
                }}";

            return code;
        }

        public static string GetClassException(string namespacePath, string className = "TargetClass")
        {
            string code = @$"
            using System;

                namespace {namespacePath}
                {{
                    public class {className} : Exception
                    {{
                    }}
                }}";

            return code;
        }
    }
}
