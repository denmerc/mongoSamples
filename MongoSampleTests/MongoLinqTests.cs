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


            for (int i = 0; i < 100; i++)
            {
                Context.Tags.Insert(new Tag());
            }

            var query =
                from e in Context.Tags.AsQueryable()
                select e;


            int idCount = 0;

            foreach (var t in query)
            {
                t.Code = "Code" + idCount;
                t.Description = "Description" + idCount;
                Context.Tags.Save(t);
                Assert.IsNotNull(t);

                idCount++;
                
            }

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
                where e.Code == "fcode-24"
                select e;

            foreach (var filter in query)
            {
                Assert.IsNotNull(filter);
                Assert.AreEqual("fcode-24", filter.Code);
                Assert.AreEqual("filter-et", filter.Description);
                Debug.WriteLine(filter.Code);
                Debug.WriteLine(filter.Description);
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

    }
}
