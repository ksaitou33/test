using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Mailform.Models
{
    public static class DBConnection
    {
        private const string ServerURI = @"mongodb://127.0.0.1";
        private const string DatabaseName = @"mailform";

        public static IMongoDatabase Connect()
        {
            if(Database == null)
            {
                Database = new MongoClient(ServerURI).GetDatabase(DatabaseName);
            }

            return Database;
        }

        public static IMongoDatabase Database { get; private set; }
    }
}
