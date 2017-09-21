using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;

namespace Mailform.Models
{
    public class EventList
    {
        private const string CollectionName = @"events";

        private IMongoDatabase database;
        private IMongoCollection<EventForm> collection;

        public EventList()
        {
            database = DBConnection.Connect();
            collection = database.GetCollection<EventForm>(CollectionName);
        }

        public List<EventForm> List
        {
            get
            {
                return collection.AsQueryable<EventForm>().ToList<EventForm>();
            }
            set
            {
                database.DropCollection(CollectionName);
                collection = database.GetCollection<EventForm>(CollectionName);
                collection.InsertMany(value);
            }
        }

        public EventForm this[string id]
        {
            get
            {
                return collection.AsQueryable<EventForm>().Where(e => new ObjectId(id).Equals(e.Id)).SingleOrDefault<EventForm>();
            }
            set
            {
                if(String.IsNullOrEmpty(id))
                {
                    collection.ReplaceOne<EventForm>(e => new ObjectId(id).Equals(e.Id), value);
                }
            }
        }
    }
}
