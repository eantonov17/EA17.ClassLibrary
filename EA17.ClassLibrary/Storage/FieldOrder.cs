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

using System.Collections.Generic;
using System.Linq;

namespace EA17.ClassLibrary.Storage
{
    public class FieldOrder
    {
        private Dictionary<string, bool> dict = new Dictionary<string, bool>();
        public FieldOrder Add(string name, bool descending) { dict[name] = descending; return this; }
        public FieldOrder Asc(string name) => Add(name, false);
        public FieldOrder Desc(string name) => Add(name, true);
        public FieldOrder Clear() { dict.Clear(); return this; }

        public List<string> List => new List<string>(Enumerate());
        public IEnumerable<string> Enumerate() => dict.Select((f) => f.Key + (f.Value ? " desc" : " asc"));
        public override string ToString() => string.Join(", ", Enumerate());
    }
}
