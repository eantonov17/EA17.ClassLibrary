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
