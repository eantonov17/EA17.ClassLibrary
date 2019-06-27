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

using EA17.ClassLibrary.Types;
using System;

namespace EA17.ClassLibrary.Storage
{
    /// <summary>
    /// Base class for objects that can be stored - saved to an external storage and later retrieved.
    /// Such objects have 3 types of values 
    ///     1) Initial (default) value is assigned when the object is created.
    ///     2) Stored is value retrieved for the storage. Stored value is preserved even if the object then gets a new value.
    /// This is needed to evaluate if we need to send an update.
    ///     3) Value is assigned during life-time of the object it needs to be saved to the storage (if never saved or different for Stored)
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay(Type,IsDirty,Value,Stored)}")]
    public abstract class Storable
    {
        public Type Type { get; }
        protected object _initial;
        public object Initial { get => _initial; set => _initial = value.MustBe(Type); }
        protected object _stored;
        public object Stored { get => _stored; set => _stored = value.MustBe(Type); }
        protected object _value;
        public virtual object Value { get => _value ?? Stored ?? _initial; set => _value = value.MustBe(Type); }

        public bool IsDirty => Equ.Not(Value, Stored);

        protected Storable(Type type) => Type = type ?? throw new ArgumentNullException(nameof(type));

        public override string ToString() => Value?.ToString();
        public static implicit operator string(Storable s) => s.ToString();

        static string DF(object obj) => obj is System.Collections.ICollection c ? "Count=" + c.Count : obj?.ToString();
        static string DebuggerDisplay(Type type, bool isDirty, object v, object s)
            => DF(v) + (isDirty ? " {" + DF(s) + "}" : String.Empty);

        public static Type GetValueType(Type t)
        {
            for (; t.BaseType != typeof(Storable); t = t.BaseType)
                if (t.BaseType == typeof(Object))
                    //throw new ArgumentException($"Type {t.TypeFN()} must be a subclass of Storable");
                    return null;
            return t.GenericTypeArguments[0];
        }
    }

    #region Storable<T>
    public abstract class StorableBase<T> : Storable
    {
        protected StorableBase() : base(typeof(T)) { }
        protected StorableBase(T t) : this() { _value = t; }
        public T ValueT => (T)Value;
        public static implicit operator T(StorableBase<T> s) => s.ValueT;
    }

    public class Storable<T> : StorableBase<T>
    {
        public Storable(T t) : base(t) { }
        public Storable() { }
        public static implicit operator Storable<T>(T t) => new Storable<T>(t);
    }

    //public class StorableSet<T> : StorableBase<Sorted<T>>
    //{
    //    public StorableSet(Sorted<T> s) : base(s) { }
    //    public StorableSet() { }
    //    public StorableSet<T> Add(T t) { Value = (Sorted<T>)Value + t; return this; }
    //    public bool Remove(T t) => Value is Sorted<T> s ? s.Remove(t) : false;
    //    public bool Contains(T t) => Value is Sorted<T> s ? s.Contains(t) : false;
    //    public static implicit operator StorableSet<T>(T t) => new StorableSet<T>().Add(t);
    //}
    #endregion Storable<T>

    #region StoredOnce<T>
    public abstract class StoredOnceBase<T> : StorableBase<T>
    {
        public override object Value { get => Stored ?? _value ?? _initial; }
        protected StoredOnceBase(T t) : base(t) { }
        protected StoredOnceBase() { }
    }

    /// <summary>
    /// StoredOnce value
    /// If the Storable has been stored, the retrieved Stored is used
    /// Otherwise a supplied initial value is used and stored to the storage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoredOnce<T> : StoredOnceBase<T>
    {
        public StoredOnce(T t) : base(t) { }
        public StoredOnce() { }
        public static implicit operator StoredOnce<T>(T t) => new StoredOnce<T>(t);
    }

    //public class Identity<T> : StoredOnceBase<T>
    //{
    //    public Identity(T t) : base(t) { }
    //    public Identity() { }
    //    //public static Identity New() => new Identity(Guid.NewGuid());
    //    public static implicit operator Identity<T>(T t) => new Identity<T>(t);
    //}
    #endregion StoredOnce<T>

    #region Counters
    //public class StorableInt : StorableBase<int>
    //{
    //    public StorableInt(int value) : base(value) { }
    //    public StorableInt() { }
    //    public static implicit operator StorableInt(int value) => new StorableInt(value);
    //    public void Add(int value) => Value = ValueT + value;
    //    //public static StorableInt operator +(StorableInt c, int i) => new StorableInt((int)c.Value + i) { Stored = c.Stored };
    //    //public static StorableInt operator -(StorableInt c, int i) => c + (-i);
    //    //public static StorableInt operator ++(StorableInt c) => c + 1;
    //    //public static StorableInt operator --(StorableInt c) => c - 1;
    //}

    //public class AutoCounter : StorableBase<int>
    //{
    //    public override object Value { get => Stored == null ? _value : 1 + (int)Stored; }
    //    public AutoCounter(int initial) : base(initial) { }
    //    public AutoCounter() { }
    //    public static implicit operator AutoCounter(int initial) => new AutoCounter(initial);
    //}
    #endregion Counters

    //public static class StorableExtensions
    //{
    //    public static void Add(this Storable<int> s, int i) => s.Value = s.ValueT + i;
    //}
}