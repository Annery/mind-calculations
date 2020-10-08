using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Utility
{
    public static IEnumerable<Type> GetAllTypes<T>() where T : class => Assembly.GetAssembly(typeof(T))
        .GetTypes()
        .Where(t => typeof(T).IsAssignableFrom(t) && t.IsAbstract == false);

    public static IEnumerable<T> GetAllInstances<T>() where T : class => GetAllTypes<T>()
        .Select(t => Activator.CreateInstance(t) as T);
}