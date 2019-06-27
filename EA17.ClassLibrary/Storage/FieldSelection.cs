using EA17.ClassLibrary.Collections;
using System;
using System.Collections.Generic;

namespace EA17.ClassLibrary.Storage
{
    public class FieldSelection
    {
        public List<string> List { get; } = new List<string>();
        public FieldSelection Add(string name) { List.Add(name); return this; }
        public FieldSelection Add(params string[] names) { names.ForEach((name) => List.Add(name)); return this; }
        public FieldSelection Clear() { List.Clear(); return this; }
        public override string ToString() => String.Join(", ", List);
    }
}
