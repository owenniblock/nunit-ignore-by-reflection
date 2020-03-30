using System;
using System.Linq;
using System.Reflection;

namespace Example.ReflectionHelper
{
    public class MethodInvoker
    {
        private static readonly Action<string, object[]> AssertIgnore =
            FindAssertType("NUnit.Framework.Assert", "nunit.framework").CreateStaticMethodDelegateIfExists<Action<string, object[]>>("Ignore");

        private static readonly Action<string, object[]> AssertInconclusive =
            FindAssertType("NUnit.Framework.Assert", "nunit.framework").CreateStaticMethodDelegateIfExists<Action<string, object[]>>("Inconclusive");

        public void Ignore(string message, object[] args)
        {
            if (AssertIgnore == null)
            {
                // Implement fallback behavior
                throw new Exception(message);
            }

            AssertIgnore?.Invoke(message, args);
        }

        public void Inconclusive(string message, object[] args)
        {
            if (AssertInconclusive == null)
            {
                // Implement fallback behavior
                throw new Exception(message);
            }

            AssertInconclusive?.Invoke(message, args);
        }

        private static Type FindAssertType(string frameworkTypeName, string assemblyName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(assemblyName)))
            {
                var type = assembly.GetExportedTypes().FirstOrDefault(t => t.FullName == frameworkTypeName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
