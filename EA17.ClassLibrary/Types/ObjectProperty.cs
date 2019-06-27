﻿using System;
using System.Reflection;

namespace EA17.ClassLibrary.Types
{
    public class ObjectProperty<ObjectType, PropertyType>
        where ObjectType : class
        where PropertyType : class
    {
        protected readonly PropertyInfo pi;
        protected readonly MethodInfo getter;
        protected readonly MethodInfo setter;
        protected readonly ConstructorInfo c0;

        public string Name { get; }
        public Type Type { get; }

        public ObjectProperty(PropertyInfo pi)
        {
            this.pi = pi;
            getter = pi.GetMethod;
            setter = pi.SetMethod;
            c0 = pi.PropertyType.GetConstructor(Type.EmptyTypes);
            Name = pi.Name;
            Type = pi.PropertyType;
        }

        public PropertyType Get(ObjectType obj) => (PropertyType)getter.Invoke(obj, null);
        public void Set(ObjectType obj, PropertyType property) => setter.Invoke(obj, new[] { property });
        public PropertyType New() => (PropertyType)c0.Invoke(null);

        //public void SetValue(ObjectType obj, object value)
        //{
        //    var property = Get(obj) is PropertyType property ? property : null;
        //    if (Equ.Default(property))
        //        property = New();
        //    setter.Invoke(obj, new[] { property });
        //}

        public PropertyType this[ObjectType obj] { get => Get(obj); set => Set(obj, value); }
        public void InitIfNull(ObjectType obj) { if (this[obj] == null) this[obj] = New(); }

        //protected readonly Dictionary<string, object> data = new Dictionary<string, object>();
        //public PropertyTypeProperty SetData(string key, object value) { data.Add(key, value); return this; }
        //public object GetData(string key) => data.TryGetValue(key, out var value) ? value : null;
        //public PropertyTypeProperty True(string key) => SetData(key, true);
        //public bool Bool(string key) => GetData(key) is bool b ? b : false;
        //public bool Or(params string[] keys) => keys.Any((key) => Bool(key));

        public override string ToString() => $"ObjectProperty<{Type.TypeFN()}> {Name}";
    }
}
