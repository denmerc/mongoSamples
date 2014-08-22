using Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSampleTests
{
    public class PromoContext
    {
        private const string connectionString = "";
        private const string databaseName = "promo";
        private const string TagsCollectionName = "tags";
        //public MongoDatabase Database;
        private MongoClient client { get; set; }
        protected MongoServer server { get; set; }
        protected MongoDatabase database { get; set; }


        public PromoContext() 
        {
            // create some fixtures and a connection
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);


        }
        public MongoCollection<Tag> Tags
        {
            get
            {
                return database.GetCollection<Tag>("tags");
            }
        }

        public MongoCollection<Filter> Filters
        {
            get
            {
                return database.GetCollection<Filter>("filters");
            }
        }

        public MongoCollection<Analytic> Analytics
        {
            get
            {
                return database.GetCollection<Analytic>("analytics");
            }
        }
    }
}
