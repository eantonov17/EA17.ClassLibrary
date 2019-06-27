//  # EA17.ClassLibrary
//  C# .Net Core general class library 
//
//  Copyright(C) 2018-2019 Eugene Antonov
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of version 3 of the GNU General Public License
//  as published by the Free Software Foundation.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.

using EA17.ClassLibrary.Collections;
using EA17.ClassLibrary.Generics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EA17.ClassLibrary
{
    public static class Equ
    {
        //private static Dictionary<Type, Func<object, object, bool>> dict;

        //static Equ() => dict = new Dictionary<Type, Func<object, object, bool>>
        //{
        //    //[typeof(string)] = (o1, o2) => (string)o1 == (string)o2
        //};

        private static Func<object, object, bool> GetEqualer(Type t)
        {
            // пока нет ничего в словаре
            //if (dict.TryGetValue(t, out var equaler))
            //    return equaler;
            if (t.IsArray)
                return Arrays;
            //https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
            //if (t.GetInterfaces().Contains(typeof(IEnumerable)))
            if (typeof(IEnumerable).IsAssignableFrom(t))
                return Enumerables;
            return null;
        }

        private static bool GetEqualer(Type t, out Func<object, object, bool> equaler) => (equaler = GetEqualer(t)) != null;

        public static bool Al<O>(params O[] os) => _Al(typeof(O), os.Select((o) => (object)o));
        public static bool Al(params object[] os) => _Al(null, os);
        public static bool Not<O>(O o1, O o2) => !Al(o1, o2);
        public static bool Not(object o1, object o2) => !Al(o1, o2);

        private static bool _Al(Type t, IEnumerable<object> os)
        {
            if (!os.GetFirst(out var o1)) return false;
            if (os.All(IsNull)) return true;
            if (os.Any(IsNull)) return false;
            return _Al(t ?? o1.GetType(), o1, os.Skip(1));
        }

        private static bool _Al(Type t, object o1, IEnumerable<object> os)
        {
            System.Diagnostics.Debug.Assert(o1 != null && !os.Any(IsNull), "Must be no nulls by now");

            //if (t == null)
            //{
            //t = o1.GetType();
            //    if (os.Any((o) => o.GetType() != t)) return false;
            //}

            if (GetEqualer(t, out var equaler))
                return os.All((o) => equaler(o1, o));

            return os.All((o) => o1.Equals(o));
        }

        private static bool IsNull(object o) => o == null;

        private static bool Arrays(object o1, object o2)
        {
            var a1 = (Array)o1;
            var a2 = (Array)o2;
            if (a1.Length != a2.Length) return false;
            for (int i = 0; i < a1.Length; ++i)
                if (!Al(a1.GetValue(i), a2.GetValue(i)))
                    return false;
            return true;
        }

        private static bool Enumerables(object o1, object o2)
        {
            var e1 = (IEnumerable)o1;
            var en2 = ((IEnumerable)o2).GetEnumerator();
            foreach (var v1 in e1)
            {
                if (!en2.MoveNext()) return false;
                var v2 = en2.Current;
                if (!Al(v1, v2))
                    return false;
            }
            return true;
        }

        public static bool Default<O>(params O[] os) => _Default(typeof(O), os.Select((o) => (object)o));
        public static bool Default(params object[] os) => _Default(null, os);

        private static bool _Default(Type t, IEnumerable<object> os)
        {
            if (!os.GetFirst(out var o1)) return false;
            var v = GetDefaultValue(t ?? o1?.GetType());
            return v == null ? os.All(IsNull) : os.All((o) => v.Equals(o));
        }

        // https://stackoverflow.com/questions/2490244/default-value-of-a-type-at-runtime
        private static object _GetDefaultValue(Type t) => t.IsValueType ? Activator.CreateInstance(t) : null;
        private static KiVi<Type, object> kivi = new KiVi<Type, object>(_GetDefaultValue);
        public static object GetDefaultValue(Type t) => t == null ? null : kivi.Get(t);
    }
}