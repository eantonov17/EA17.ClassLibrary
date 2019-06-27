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

}
