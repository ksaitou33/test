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
        private IMongoCollection<EventTemplate> collection;

        public EventList()
        {
            database = DBConnection.Connect();
            collection = database.GetCollection<EventTemplate>(CollectionName);
        }

        public List<EventTemplate> List
        {
            get
            {
                return collection.AsQueryable<EventTemplate>().ToList<EventTemplate>();
            }
            set
            {
                database.DropCollection(CollectionName);
                collection = database.GetCollection<EventTemplate>(CollectionName);
                collection.InsertMany(value);
            }
        }

        public EventTemplate this[string id]
        {
            get
            {
                EventTemplate eventForm = null;
                if (!String.IsNullOrEmpty(id))
                {
                    var bsonId = new BsonObjectId(new ObjectId(id));
                    eventForm = collection.AsQueryable<EventTemplate>().Where(e => bsonId.Equals(e.Id)).SingleOrDefault<EventTemplate>();
                }
                return eventForm;
            }
            set
            {
                if(String.IsNullOrEmpty(id))
                {
                    var bsonId = new BsonObjectId(new ObjectId(id));
                    collection.ReplaceOne<EventTemplate>(e => bsonId.Equals(e.Id), value);
                }
            }
        }

        public long Delete(string id)
        {
            long deleteCount = 0;
			if (String.IsNullOrEmpty(id))
			{
				var bsonId = new BsonObjectId(new ObjectId(id));
                var result = collection.DeleteOne(e => bsonId.Equals(e.Id));
                deleteCount = result.DeletedCount;
			}
            return deleteCount;
        }
    }
}
