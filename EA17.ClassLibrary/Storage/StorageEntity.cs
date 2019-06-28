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

namespace EA17.ClassLibrary.Storage
{
    public class StorageEntity<S> : IStorageEntity<S> where S : StorableObject
    {
        public StorageEntity(IStorageEntity se) => Entity = se;

        public IStorageEntity Entity { get; }
        public S Value => (S)Entity.Value;
        public bool IsRetrieved => Entity.IsRetrieved;
        public bool Delete() => Entity.Delete();
        public IStorageEntity<S> InsertOrMerge() => new StorageEntity<S>(Entity.InsertOrMerge());
        public IStorageEntity<S> InsertOrReplace() => new StorageEntity<S>(Entity.InsertOrReplace());
        public IStorageEntity<S> Retrieve() => new StorageEntity<S>(Entity.Retrieve());
    }
}
