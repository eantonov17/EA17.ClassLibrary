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

using EA17.ClassLibrary.Generics;
using System;

namespace EA17.ClassLibrary.Storage
{
    /// <summary>
    /// Here we cache all IStorage implementation we use in the code. This gives us singleton implementation of each IStorage
    /// </summary>
    public class StorageDictionary
    {
        KiVi<(Type storageType, string storageName), IStorage> lookup = new KiVi<(Type storageType, string storageName), IStorage>();
        public StorageDictionary() { }

        public IStorage Get(Type storageType, string storageName, Func<string, IStorage> create)
        {
            if (storageType == null) throw new ArgumentNullException(nameof(storageType));
            if (storageName == null) throw new ArgumentNullException(nameof(storageName));
            if (create == null) throw new ArgumentNullException(nameof(create));
            return lookup.Get((storageType, storageName), () => create(storageName));
        }
    }
}
