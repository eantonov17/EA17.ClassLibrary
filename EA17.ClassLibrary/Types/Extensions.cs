using System;

namespace EA17.ClassLibrary.Types
{
    public static class Extensions
    {
        public static string TypeFN(this object v) => v?.GetType()?.FullName;

        public static object MustBe(this object v, Type t) => t.IsInstanceOfType(v) ? v : throw new InvalidOperationException($"Object '{v}' must have type {t.TypeFN()}");

    }
}
