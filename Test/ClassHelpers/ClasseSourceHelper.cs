using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ClassHelpers
{
    public static class ClasseSourceHelper
    {
        public static string ClassSourceCreateClasses(string namespaceSource, List<ClassAndNamespace> classesToCreate)
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

                namespace {namespaceSource}
                {{
                    public class ClassSource
                    {{
                        public static void MethodForTest()
                        {{
                            {body}
                        }}
                    }}
                }}";

            return code;
        }

        public static string ClassSourceAccessMethodOfClasses(string namespaceSource, List<ClassAndNamespace> classesToAccess)
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
                    public class ClassSource
                    {{
                        public static void MethodForTest()
                        {{
                            {body}
                        }}
                    }}
                }}";

            return code;
        }

        public static string ClassSourceWithParametersInMethod(string namespaceSource, List<ClassAndNamespace> parametersToAdd)
        {
            var parameters = "";

            var usigns = "";

            var listParameters = new List<string>();
            foreach (var classAndNamespace in parametersToAdd)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";

                listParameters.Add(@$"

                    {classAndNamespace.Name} classCreate{classAndNamespace.Name};
                ");

            }
          
            parameters += String.Join(", ", listParameters);

            string code = @$"
                using System;
                {usigns}

                namespace {namespaceSource}
                {{
                    public class ClassSource
                    {{
                        public static void MethodForTest({parameters})
                        {{
                        }}
                    }}
                }}";

            return code;
        }

        public static string ClassSourceWithLocalTypes(string namespaceSource, List<ClassAndNamespace> localTypesToAdd)
        {
            return ClassSourceCreateClasses(namespaceSource, localTypesToAdd);  
        }

        public static string ClassSourceWithThrows(string namespaceSource, List<ClassAndNamespace> typesToThrows)
        {

            var body = "";

            var usigns = "";

            foreach (var classAndNamespace in typesToThrows)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";
                body += @$"
                    
                    throw new {classAndNamespace.Name}();
                ";

            }

            string code = @$"
                using System;
                {usigns}

                namespace {namespaceSource}
                {{
                    public class ClassSource
                    {{
                        public static void MethodForTest()
                        {{
                            {body}
                        }}
                    }}
                }}";

            return code;
        }

        public static string ClassSourceExtends(string namespaceSource, ClassAndNamespace? typeToExtend)
        {
            var usigns = typeToExtend is not null ? @$" using {typeToExtend.NamespacePath};" : "";

            var extends = typeToExtend is not null ? @$" : {typeToExtend.Name}" : "";

            string code = @$"
                using System;
                {usigns}

                namespace {namespaceSource}
                {{
                    public class ClassSource {extends}
                    {{
                    }}
                }}";

            return code;
        }

        public static string ClassSourceImplements(string namespaceSource, List<ClassAndNamespace> typesToImplements)
        {
            var interfaces = "";

            var usigns = "";

            var listInterfaces = new List<string>();
            foreach (var classAndNamespace in typesToImplements)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";

                listInterfaces.Add(@$"I{classAndNamespace.Name}");

            }

            interfaces += String.Join(", ", listInterfaces);

            string code = @$"
                using System;
                {usigns}

                namespace {namespaceSource}
                {{
                    public class ClassSource : {interfaces}
                    {{
                    }}
                }}";

            return code;
        }

        public static string ClassSourceWithFields(string namespaceSource, List<ClassAndNamespace> typesOfFields)
        {
            var body = "";

            var usigns = "";

            foreach (var classAndNamespace in typesOfFields)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";
                body += @$"

                    private {classAndNamespace.Name} _classCreate{classAndNamespace.Name};
                ";
            }
            string code = @$"
                using System;
                {usigns}

                namespace {namespaceSource}
                {{
                    public class ClassSource 
                    {{
                        {body}

                    }}
                }}";

            return code;
        }

        public static string ClassSourceWithProperties(string namespaceSource, List<ClassAndNamespace> typesOfProperties)
        {
            var body = "";

            var usigns = "";

            foreach (var classAndNamespace in typesOfProperties)
            {
                usigns += @$"
                    using {classAndNamespace.NamespacePath};";
                body += @$"

                    private {classAndNamespace.Name} _classCreate{classAndNamespace.Name} {{get; set}}
                ";
            }

            string code = @$"
                using System;
                {usigns};

                namespace {namespaceSource}
                {{
                    public class ClassSource 
                    {{
                        {body}

                    }}
                }}";

            return code;
        }
    }
}
