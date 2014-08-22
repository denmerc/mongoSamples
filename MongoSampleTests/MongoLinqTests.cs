using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MongoDB.Driver.Linq;
using Domain;
using System.Diagnostics;
using System.Collections.Generic;

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
        public void TestSerializingFilters()
        {

            var collection = Context.Filters;


            var query =
                from e in collection.AsQueryable()
                where e.Type == FilterType.ProductType
                select e;

            foreach (var filter in query)
            {
                Assert.IsNotNull(filter);
                Assert.AreEqual("Movement", filter.Type);
            }
                        
        }

        [TestMethod]
        public void FilterListBoxDataSource()
        {
            var collection = Context.Filters;

            MongoDB.Driver.MongoCursor<Filter> cursor = collection.FindAllAs<Filter>();
            try
            {
                // note that this requires the extension methods in System.Linq
                List<Filter> filters = cursor.ToList<Filter>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void FilterDistinct()
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
        public void Analytics_ToList()
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

        [TestMethod]
        public void Tags_MasterList_Bson_AddedAsString()
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
        public void Tags_Per_Analytic()
        {
            var collection = Context.Analytics;

            try
            {
                var tags = from a in collection.AsQueryable<Analytic>()
                           select a.Tags.Distinct();
                foreach (var item in tags)
                {
                    Console.WriteLine(item);
                }
                

                var count = tags.Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void SearchAnalyticsByTag()
        {
            var collection = Context.Analytics;

            try
            {
                //var cursor = collection.AsQueryable();

                //var cursor = collection.Distinct<string>(
                //    "Tags"

                //    );
                var tags = from a in collection.AsQueryable<Analytic>()
                           select a.Tags.Distinct();

                //IEnumerable<BsonValue> to list<string>
                //new MongoDB.Driver.QueryDocument{}


               

                var query = collection.AsQueryable()
                    .Where(a => a.Tags.In(tags));
                
                
                
                //var tagToSearch = tags.First();
                //single tag
                //var query2 = collection.AsQueryable().Where(a => a.Tags.Contains("tag-vero"));

                foreach (var a in query)
                {


                    //List<Analytic> analytics = collection.FindAllAs<Analytic>(
                    //    new MongoDB.Driver.QueryDocument{
                    //        {"Tags", { "$in" , ["tag-fuga"]}}
                        
                    //});
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [TestMethod]
        public void GetAllAnalytics()
        {
            var collection = Context.Analytics;

        }
    }
}
