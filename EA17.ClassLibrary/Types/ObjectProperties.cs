using EA17.ClassLibrary.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EA17.ClassLibrary.Types
{
    public class ObjectProperties<O, P> : IEnumerable<ObjectProperty<O, P>>
        where O : class
        where P : class
    {
        private static readonly Dictionary<Type, ObjectProperties<O, P>> dictionary = new Dictionary<Type, ObjectProperties<O, P>>();
        public static ObjectProperties<O, P> Get(Type type)
        {
            if (!dictionary.TryGetValue(type, out ObjectProperties<O, P> ps))
                dictionary[type] = ps = new ObjectProperties<O, P>(type);
            return ps;
        }

        private readonly Dictionary<string, ObjectProperty<O, P>> properties;
        private readonly ConstructorInfo c0;
        //        public TagDictionary Tags { get; }

        protected ObjectProperties(Type type)
        {
            if (!(type.IsSubclassOf(typeof(O))))
                throw new ArgumentException($"Type {type.TypeFN()} must be {nameof(O)}", nameof(type));
            properties = new Dictionary<string, ObjectProperty<O, P>>();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            foreach (var pi in propertyInfos)
                if (pi.PropertyType.IsSubclassOf(typeof(P)))
                    properties[pi.Name] = new ObjectProperty<O, P>(pi);

            c0 = type.GetConstructor(Type.EmptyTypes);

            //Tags = new TagDictionary();
            //Tags.OnSetTagEvent += (Tag tag, string name) => properties[name].True(tag.String);
        }

        public O New() => (O)c0.Invoke(null);
        //public O NewInited() { var so = New(); so.InitNulls(); return so; }

        public void InitNulls(O so, params string[] propertyNames) => this.Where((p) => propertyNames.IsNullOrEmpty() || propertyNames.Contains(p.Name)).ForEach((p) => p.InitIfNull(so));

        public ObjectProperty<O, P> this[string name] => properties.TryGetValue(name, out var v) ? v : null;
        public bool Contains(string name) => properties.ContainsKey(name);

        //public ObjectProperty this[Tag tag] => properties.TryGetValue(Tags.GetTag(tag), out var v) ? v : null;

        IEnumerator<ObjectProperty<O, P>> IEnumerable<ObjectProperty<O, P>>.GetEnumerator() => properties.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => properties.Values.GetEnumerator();

        //public ObjectProperties SetTag(string tag, string name)
        //{
        //    if (tags.ContainsKey(tag)) throw new InvalidOperationException($"Tag '{tag}' is already defined");
        //    tags.Add(tag, name);
        //    properties[name].True(tag);
        //    return this;
        //}
        //public string GetTag(string tag) => tags.TryGetValue(tag, out var value) ? (string)value : null;
        //public ObjectProperties SetTags(string tag, params string[] names)
        //{
        //    tags.Add(tag, names);
        //    foreach (var name in names)
        //        properties[name].True(tag);
        //    return this;
        //}
        //public string[] GetTags(string key) => tags.TryGetValue(key, out var value) ? (string[])value : null;

        public override string ToString() => "Count=" + properties.Count;
    }
}
