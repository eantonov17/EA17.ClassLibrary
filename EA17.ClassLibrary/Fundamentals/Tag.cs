using System;

namespace EA17.ClassLibrary.Fundamentals
{
    /// <summary>
    /// Almost String but not String
    /// </summary>
    public class Tag : IEquatable<Tag>
    {
        protected readonly string tag;
        public Tag(string tag) { this.tag = tag ?? throw new ArgumentNullException(nameof(tag)); }
        public string String => tag;

        public override int GetHashCode() { return tag.GetHashCode(); }
        public override string ToString() { return tag; }
        public override bool Equals(object obj) { return Equals(obj as Tag); }
        public bool Equals(Tag other) { return (object)other != null && tag == other.tag; }
        public static bool operator ==(Tag tag1, Tag tag2) { return tag1.tag == tag2.tag; }
        public static bool operator !=(Tag tag1, Tag tag2) { return tag1.tag != tag2.tag; }
    }
}
