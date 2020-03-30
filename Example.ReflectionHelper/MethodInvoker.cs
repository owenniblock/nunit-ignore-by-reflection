using System;
using System.Linq;
using System.Reflection;

namespace Example.ReflectionHelper
{
    public class MethodInvoker
    {
        public void Invoke(string methodName, string exceptionText)
        {
            var nunitType = FindAssertType("NUnit.Framework.Assert", exceptionText);
            var method = nunitType.GetMethod(methodName, new[] { typeof(string) });

            if (method == null)
            {
                throw new Exception(exceptionText);
            }

            method.Invoke(null, new object[] { exceptionText });
        }

        private Type FindAssertType(string frameworkTypeName, string exceptionText)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("nunit.framework")))
            {
                var type = assembly.GetExportedTypes().FirstOrDefault(t => t.FullName == frameworkTypeName);
                if (type != null)
                {
                    return type;
                }
            }

            throw new Exception(exceptionText);
        }
    }
}
