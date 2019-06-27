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
