using System;
using System.Collections;
using System.Collections.Generic;

namespace EA17.ClassLibrary.Collections
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> a) { foreach (var i in e) { a(i); } }

        public static bool IsNullOrEmpty(this IEnumerable e) => !e?.GetEnumerator()?.MoveNext() ?? false;

        public static bool GetFirst(this IEnumerable e, out object o)
        {
            if (e?.GetEnumerator() is IEnumerator en && en.MoveNext()) { o = en.Current; return true; }
            else { o = null; return false; }
        }
    }
}
