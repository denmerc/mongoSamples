using Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MongoDB.Bson;

namespace MongoSampleTests
{
    public class PromoContext
    {
        private readonly string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
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

        public MongoCollection<Actions> Actions
        {
            get
            {
                return database.GetCollection<Actions>("options");
            }
        }

        public MongoCollection<BsonDocument> ActionsAsDocuments
        {
            get
            {
                return database.GetCollection("options");
            }
        }
        public MongoCollection<Domain.Action> Commands
        {
            get
            {
                return database.GetCollection<Domain.Action>("commands");
            }
        }
    }
}
