using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Mailform.Models
{
    public class EventTemplate
    {
		[BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
		public BsonObjectId BsonId { get; set; }

		[BsonIgnore]
		public string Id
		{
			get { return BsonId.ToString(); }
		}

		public string Title { get; set; }

		public List<string> Items { get; set; }
	}
}
