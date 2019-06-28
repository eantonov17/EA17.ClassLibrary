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

namespace EA17.ClassLibrary.Storage
{
    /// <summary>
    /// Storages available from one storage namespace
    /// Actual class will receive a connection string as constructor's parameter
    /// </summary>
    public abstract class StorageBase : IStorage
    {
        private static StorageDictionary storages = new StorageDictionary();

        protected static IStorage Get(Type storageType, string storageName, Func<string, IStorage> create) => storages.Get(storageType, storageName, create);

        protected abstract IStorageAdapter CreateAdapter(Type t);

        public string Name { get; }
        protected StorageBase(string name) => Name = name;

        private static Dictionary<Type, object> adapters = new Dictionary<Type, object>();
        public IStorageAdapter<S> GetAdapter<S>() where S : StorableObject
        {
            var t = typeof(S);
            if (adapters.TryGetValue(t, out var i))
                return (IStorageAdapter<S>)i;
            var s = new StorageAdapter<S>(CreateAdapter(t));
            adapters[t] = s;
            return s;
        }

        public IStorageEntity<S> GetEntity<S>(S so) where S : StorableObject => GetAdapter<S>().GetEntity(so);
        public IStorageEntity<S> Retrieve<S>(S so) where S : StorableObject => GetEntity(so).Retrieve();
        public IStorageEntity<S> InsertOrMerge<S>(S so) where S : StorableObject => GetEntity(so).InsertOrMerge();
        public IStorageEntity<S> InsertOrReplace<S>(S so) where S : StorableObject => GetEntity(so).InsertOrReplace();
        public bool Delete<S>(S so) where S : StorableObject => GetEntity(so).Delete();

        public IEnumerable<IStorageEntity<S>> Select<S>(S so, int? maxCount = null) where S : StorableObject => GetAdapter<S>().Select(so, maxCount);
    }
}
