using System;
using System.Reflection;

internal static class ReflectionExtensions
{
    public static T CreateStaticMethodDelegateIfExists<T>(this Type declaringType, string methodName)
        where T : Delegate
    {
        var method = declaringType.GetMethod(
              methodName,
              BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
              binder: null,
              GetInvocationParameterTypes<T>(),
              modifiers: null);

        return method is null
            ? null
            : (T)Delegate.CreateDelegate(typeof(T), method);
    }

    private static Type[] GetInvocationParameterTypes<T>()
        where T : Delegate
    {
        var method = typeof(T).GetMethod("Invoke", BindingFlags.Public | BindingFlags.Instance)
            ?? throw new NotSupportedException("Delegate types must have an Invoke method.");

        return Array.ConvertAll(method.GetParameters(), p => p.ParameterType);
    }
}