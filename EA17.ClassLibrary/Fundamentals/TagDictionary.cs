using EA17.ClassLibrary.Collections;
using System;
using System.Collections.Generic;

namespace EA17.ClassLibrary.Fundamentals
{
    public class TagDictionary
    {
        private readonly Dictionary<Tag, object> tags = new Dictionary<Tag, object>();

        public delegate void OnSetTag(Tag tag, string name);
        public event OnSetTag OnSetTagEvent;

        public TagDictionary SetTag(Tag tag, string name)
        {
            if (tags.ContainsKey(tag)) throw new InvalidOperationException($"Tag '{tag}' is already defined");
            tags.Add(tag, name);
            OnSetTagEvent?.Invoke(tag, name);
            return this;
        }
        public string GetTag(Tag tag) => tags.TryGetValue(tag, out var value) ? (string)value : null;
        public TagDictionary SetTags(Tag tag, params string[] names)
        {
            if (tags.ContainsKey(tag)) throw new InvalidOperationException($"Tag '{tag}' is already defined");
            tags.Add(tag, names);
            if (OnSetTagEvent != null)
                names.ForEach((name) => OnSetTagEvent(tag, name));
            return this;
        }
        public string[] GetTags(Tag tag) => tags.TryGetValue(tag, out var value) ? (string[])value : null;

        public override string ToString() { return "TagDictionary, count=" + tags.Count; }
    }
}
