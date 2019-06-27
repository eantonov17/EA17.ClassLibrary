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
using System.Collections.Generic;

namespace EA17.ClassLibrary.Generics
{
    public class KiVi<K, V> : Dictionary<K, V>
    {
        protected readonly Func<K, V> k2v;

        public KiVi() { }
        public KiVi(Func<K, V> fill) => k2v = fill ?? throw new ArgumentNullException(nameof(fill));

        public V Get(K k)
        {
            if (!TryGetValue(k, out V v))
                this[k] = v = k2v(k);
            return v;
        }

        public V Get(K k, Func<V> create)
        {
            if (!TryGetValue(k, out V v))
                this[k] = v = create();
            return v;
        }
    }
}
