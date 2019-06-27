using System;
using System.Collections.Generic;
using System.Text;

namespace EA17.ClassLibrary.Storage
{
    public interface IStorage
    {
        string Name { get; }
        //IStorageAdapter GetAdapter(System.Type t);
        IStorageAdapter<S> GetAdapter<S>() where S : StorableObject;
    }

    public interface IStorageAdapter
    {
        IStorageEntity GetEntity(StorableObject so);
        IEnumerable<IStorageEntity> Select(StorableObject so, int? maxCount);
    }

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
