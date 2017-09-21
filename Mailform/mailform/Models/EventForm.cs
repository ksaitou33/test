using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mailform.Models
{
    public class EventForm
    {
        [BsonId]
        public ObjectId BsonId { get; set; }

        [BsonIgnore]
        public string Id
        {
            get { return BsonId.ToString(); }
        }

        public string Title { get; set; }

        public string FCNumber { get; set; }

        public string TelNumber { get; set; }

        public List<string> Others{ get; set; }
    }
}
