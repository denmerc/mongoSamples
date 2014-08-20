using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoSamples;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace MongoSampleTests
{
    public class MongoSampleTestBase
    {
        private const string connectionString = "mongodb://localhost";
        private const string databaseName = "promo";
        private const string collectionName = "tags";

        private MongoClient client { get; set; }
        protected MongoServer server { get; set; }
        protected MongoDatabase database { get; set; }
        protected MongoCollection<Tag> collection { get; set; }
        protected MongoCollection<BsonDocument> documentCollection { get; set; }

        [TestInitialize]
        public virtual void Setup()
        {
            SetupConnection();
            PopulateDatabase();
        }

        [TestCleanup]
        public virtual void TearDown()
        {
            // destroy the collection to leave the db clean
            TearDownDatabase();
        }



        protected void SetupConnection()
        {
            // create some fixtures and a connection
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);
            var coll = database.GetCollection<Filter>("filters");
            // two choices of collection type:
            // One based on generic BsonDocumnets
            documentCollection = database.GetCollection(collectionName);
            // And another tied to our domain model
            collection = database.GetCollection<Tag>(collectionName);
        }


        protected virtual void PopulateDatabase()
        {
            // create some entries 
            //var developer = new Developer(1, "Test", "Person", "Developer");
            //collection.Insert(developer);
            //var developer2 = new Developer(2, "Another", "Developer", "Developer");
            //collection.Insert(developer2);

            //BsonDocument document = new BsonDocument();
            //document.Add(new BsonElement("name", "Testing"))
            //    .Add(new BsonElement("number", new BsonInt32(42)));

            //documentCollection.Insert(document);
            //var documentId = document["_id"];

            //for (int i = 0; i < 100; i++)
            //{
            //    collection.Insert(new Domain.Tag());
            //}

        }

        protected void TearDownDatabase()
        {
            //collection.Drop();
        }


    }
}
