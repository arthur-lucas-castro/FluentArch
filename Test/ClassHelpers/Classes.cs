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
        public static string ClassCreateClasses(string namespacePath, List<ClassAndNamespace> classesToCreate, string className = "TargetClass")
        {

            var body = "";

            var usigns = "";

            foreach (var classAndNamespace in classesToCreate)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";
                body += @$"

                    var classCreate{classAndNamespace.Name} = new {classAndNamespace.Name}();
                ";

            }

            string code = @$"
                using System;
                {usigns};

                namespace {namespacePath}
                {{
                    public class {className}
                    {{
                        public static void MethodForTest()
                        {{
                            {body}
                        }}
                    }}
                }}";

            return code;
        }

        public static string ClassSourceWithLocalTypes(string namespacePath, List<ClassAndNamespace> localTypesToAdd, string className = "TargetClass")
        {
            return ClassCreateClasses(namespacePath, localTypesToAdd, className);
        }
        public static string ClassAccessMethodOfClasses(string namespaceSource, string classNameSource, List<ClassAndNamespace> classesToAccess)
        {

            var body = "";

            var usigns = "";

            foreach (var classAndNamespace in classesToAccess)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";
                body += @$"

                    var classCreate{classAndNamespace.Name} = new {classAndNamespace.Name}();
                    classCreate{classAndNamespace.Name}.MethodToTest();
                ";

            }

            string code = @$"
                using System;
                {usigns}

                namespace {namespaceSource}
                {{
                    public class {classNameSource}
                    {{
                        public static void MethodForTest()
                        {{
                            {body}
                        }}
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
        public static string GetInterface(string namespacePath, string interfaceName)
        {
            return $@"
                namespace {namespacePath}
                {{
                    public interface {interfaceName}
                    {{
                    }}
                }}";
        }
        public static string GetException(string namespacePath, string exceptionName)
        {
            return $@"
                namespace {namespacePath}
                {{
                    public class {exceptionName} : System.Exception
                    {{
                        public {exceptionName}() : base() {{ }}
                    }}
                }}";
        }
        public static string ClassImplementsInterfaces(string namespacePath, List<ClassAndNamespace> interfacesToImplement)
        {
            var usings = "";
            var interfaces = "";

            if (interfacesToImplement.Any())
            {
                usings = string.Join(Environment.NewLine, interfacesToImplement.Select(i => $"using {i.NamespacePath};"));
                interfaces = " : " + string.Join(", ", interfacesToImplement.Select(i => i.Name));
            }

            string code = @$"
                using System;
                {usings}

                    namespace {namespacePath}
                    {{
                         public class Class{interfaces}
                        {{
                        }}
                    }}";

            return code;
        }

    }
}
