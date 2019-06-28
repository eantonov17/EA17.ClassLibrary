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

namespace EA17.ClassLibrary.Storage
{
    // If at some point we need to load a storage library at run time this might be needed
    // Now we just statically link with appropriate lib.
    //public interface IStorageFactory
    //{
    //    IStorage GetStorage(string storageName = null);
    //}


    /// <summary>
    /// This is an interface to a db driver: one for Azure Table Storage, another for MS SQL etc.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Name of the storage. A formality but can be used as a prefix of a connection string name.
        /// </summary>
        string Name { get; }
        
        //IStorageAdapter GetAdapter(System.Type t);
        
        /// <summary>
        /// We ask for an adapter that allows to store/retrieve a subtype of StorableObject
        /// </summary>
        /// <typeparam name="S">StorableObject, aka table in SQL db</typeparam>
        /// <returns></returns>
        IStorageAdapter<S> GetAdapter<S>() where S : StorableObject;
    }

    /// <summary>
    /// Interface to access a subtype of StorableObject
    /// In SQL equivalent is a db table
    /// </summary>
    public interface IStorageAdapter
    {
        /// <summary>
        /// Get an interface to access a storage entity (~ a row in SQL)
        /// If not all Storable properties set, this entity will allow a partial update, 
        /// or mutltiple retrieves
        /// </summary>
        /// <param name="so"></param>
        /// <returns></returns>
        IStorageEntity GetEntity(StorableObject so);

        /// <summary>
        /// Select several storage entities based on properties set in StorableObject so
        /// </summary>
        /// <param name="so"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        IEnumerable<IStorageEntity> Select(StorableObject so, int? maxCount);
    }

    /// <summary>
    /// Storage Entity - the thing we save to/load from the storage
    /// In SQL aka db row
    /// </summary>
    public interface IStorageEntity
    {
        StorableObject Value { get; }
        bool IsRetrieved { get; }
        IStorageEntity Retrieve();
        IStorageEntity InsertOrMerge();
        IStorageEntity InsertOrReplace();
        bool Delete();
    }

    public interface IStorageAdapter<S> where S : StorableObject
    {
        IStorageAdapter Adapter { get; }
        IStorageEntity<S> GetEntity(S so);
        IEnumerable<IStorageEntity<S>> Select(S so, int? maxCount);
    }

    public interface IStorageEntity<S> where S : StorableObject
    {
        IStorageEntity Entity { get; }
        S Value { get; }
        bool IsRetrieved { get; }
        IStorageEntity<S> Retrieve();
        IStorageEntity<S> InsertOrMerge();
        IStorageEntity<S> InsertOrReplace();
        bool Delete();
    }
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


}
