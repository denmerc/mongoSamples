using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MongoDB.Driver.Linq;
using Domain;
using System.Diagnostics;
using System.Collections.Generic;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace MongoSampleTests
{
    [TestClass]
    public class MongoLinqTests
    {
        public readonly PromoContext Context = new PromoContext();

        [TestMethod, Ignore]
        public void TestALinqQuery()
        {
            //var query =
            //    from e in collection.AsQueryable()
            //    where e.LastName == "Person"
            //    select e;

            //foreach (var developer in query){
            //    Assert.IsNotNull(developer);
            //}
        }

        [TestMethod, Ignore]
        public void TestInsertTags()
        {

            Random r = new Random();

            //Mock lookups
           //var   SampleFilters = new List<Domain.Filter>();
           //var   SampleFolders = new List<Domain.Tag>();

           // for (int i = 0; i < 100; i++)
           // {
           //     Domain.Analytic
           //     SampleFilters.Add(new Domain.Filter());
           //     SampleFolders.Add(new Domain.Folder());
           // }
           // int j = 0;
           // SampleFilters.ForEach(x => x.Id = ++j);
           // SampleFilters.ForEach(x => x.Code +=  ++j);

           // SampleFilters.ForEach(x => x.IsSelected = r.NextDouble() > 0.5);


            //for (int i = 0; i < 100; i++)
            //{
            //    Context.Tags.Insert(new Tag());
            //}

            //var query =
            //    from e in Context.Tags.AsQueryable()
            //    select e;


            //int idCount = 0;

            //foreach (var t in query)
            //{
            //    t.Code = "Code" + idCount;
            //    t.Description = "Description" + idCount;
            //    Context.Tags.Save(t);
            //    Assert.IsNotNull(t);

            //    idCount++;
                
            //}

        }

        //[TestMethod]
        //public void TestInsertFilters()
        //{

        //    Random r = new Random();
        //    var collection = context.Filters;

        //    for (int i = 0; i < 100; i++)
        //    {
        //        collection.Insert(new Filter());
        //    }

        //    var query =
        //        from e in collection.AsQueryable()
        //        where  e.Code == null
        //        select e;


        //    int idCount = 0;

        //    foreach (var t in query)
        //    {
        //        t.Code = "Code" + idCount;
        //        t.Description = "Description" + idCount;
        //        collection.Save(t);
        //        Assert.IsNotNull(t);

        //        idCount++;

        //    }

        //}


        [TestMethod]
        public void Filters_GetFiltersByType()
        {

            var collection = Context.Filters;

            var query =
                    from a in collection.FindAllAs<Filter>()
                    where a.Type == FilterType.ProductType
                    select a;

           
            List<Filter> fList = query.ToList();
                        
        }

        [TestMethod]
        public void Filters_GetAllFilters()
        {
            var collection = Context.Filters;

            MongoDB.Driver.MongoCursor<Filter> cursor = collection.FindAllAs<Filter>();
            try
            {
                //defaults to FilterCode because it first defined in Enum
                List<Filter> filters = cursor.ToList<Filter>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void Filters_GetDistinctFilterTypeList()
        {
            var collection = Context.Filters;

            var cursor =
                    from a in collection.FindAllAs<Filter>()
                    where a.Type == FilterType.VendorCode
                    select a.Type;

            try
            {
                // note that this requires the extension methods in System.Linq
                List<FilterType> filters = cursor.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [TestMethod]
        public void Analytics_GetAllAnalytics()
        {
            var collection = Context.Analytics;

            MongoDB.Driver.MongoCursor<Analytic> cursor = collection.FindAllAs<Analytic>();
            try
            {
                // note that this requires the extension methods in System.Linq
                List<Analytic> analytics = cursor.ToList<Analytic>();
                foreach (var a in analytics)
                {
                    Console.WriteLine(a);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod, Ignore]
        public void Analytics_GetTagsAsBson_AddedAsString()
        {
            var collection = Context.Analytics;

            try
            {
                var cursor = collection.Distinct(

                    "Tags"

                    );

                    //new MongoDB.Driver.QueryDocument{}

                var d = new List<string>();
                foreach (var item in cursor)
                {
                    
                    d.Add(item.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [TestMethod]
        public void Analytics_GetListAllTagsUsedByAnalytics()
        {
            var collection = Context.Analytics;

            try
            {
                var tags = from a in collection.AsQueryable<Analytic>()
                           select a.Tags.Distinct();


                var list = tags.ToList();
                var count = tags.Count();


                Assert.IsTrue(count > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void Analytics_GetAnalyticsByTag()
        {
            var collection = Context.Analytics;

            try
            {

                //var query = collection.AsQueryable()
                //    .Where(a => a.Tags.In(tags));


                //var tags = from a in collection.AsQueryable<Analytic>()
                //           select a.Tags.Distinct();

                //single tag tag-consequatur
                //var tagToSearch = tags.ToList().First();
                var tags = new List<string> { "tag-consequatur" };

                var query = collection.AsQueryable().Where(a => a.Tags.ContainsAny(tags));



                var analyticsFoundWithTag = query.ToList();

                foreach (var item in analyticsFoundWithTag)
                {
                    Assert.IsTrue(analyticsFoundWithTag.Count > 0);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [TestMethod, Ignore]
        public void Options_GetActionsForAnalyticsModule()
        {
            try
            {
                var collection = Context.Actions;
                var actions = from a in collection.AsQueryable<Actions>()
                              where a.Key == "Commands"
                                select a.Items;

                var list = actions.ToList();

            }
            catch (Exception)
            {
                
                throw;
            }
        }

         [TestMethod, Ignore]
        public void Options_GetCommandsForAnalyticsModule()
        {
            try
            {
                var collection = Context.Actions;
                var actions = from a in collection.AsQueryable<Actions>()
                              where a.Key == "Commands"
                              select a.Items;

                var list = actions.ToList();


            }
            catch (Exception)
            {

                throw;
            }
        }

         [TestMethod, Ignore]
        public void Options_GetCommandsForAnalytics_ActionsBar()
        {
            try
            {
                var collection = Context.ActionsAsDocuments;
                //var actions = from a in collection.AsQueryable<Actions>()
                //              where a.Key == "Commands"
                //              select a.Items;

                //foreach (var a in actions)
                //{


                //}
                


                List<BsonValue> ancestors = new List<BsonValue>();

                ancestors.Add("Actions2");

                var query = Query.And(
                        Query.EQ("Key", "Commands"),
                        Query.In("Items.Ancestors", ancestors)
                    
                    );


                MongoDB.Bson.BsonDocument documentRead = collection.FindOne(query);



            }
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod]
         public void Commands_GetCommandsForAnalytics_ActionsBar()
         {
             try
             {
                 var collection = Context.Commands;
                 //var actions = from a in collection.AsQueryable<Actions>()
                 //              where a.Key == "Commands"
                 //              select a.Items;

                 //foreach (var a in actions)
                 //{


                 //}



                 //List<BsonValue> ancestors = new List<BsonValue>();
                 //ancestors.Add("Planning");
                 //ancestors.Add("Analytics");
                 //ancestors.Add("Actions2");

                 var query = 
                     Query.And(
                         Query.In("Ancestors", new BsonArray(new List<string>{"Planning","Actions2"}))
                     );


                 List<Domain.Action> actions = collection.Find(query).ToList();

                 Assert.IsTrue(actions.Count > 0);
                     

             }
             catch (Exception)
             {

                 throw;
             }
         }
    }
}
