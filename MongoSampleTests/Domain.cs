using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
        public class Tag
        {
            //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            //public string Id { get; set; }
            //public string Code { get; set; }
            //public string Description { get; set; }
            public string Value { get; set; }
            
        }

        public class Folder
        {
            public string Name { get; set; }
        }

        public enum Module
        {
            Planning,
            Tracking,
            Reporting,
            Administration
        }

        public enum AnalyticStepType
        {
            Identity,
            Filters,
            PriceLists,
            ValueDrivers
        }

        public enum PricingStepType
        {
            Identity,
            Filters,
            PriceLists,
            Rounding,
            Strategy,
            Results,
            Forecasts,
            Approval
        }


        public class Session
        {
            public User User { get; set; }
            public List<Analytic> Analytics { get; set; }
            public List<PriceRoutine> PriceRoutines { get; set; }
        }

        public class User
        {
            public string Login { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Dictionary<Action, EntityBase> RecentHistory { get; set; } //instead of ViewModelBase
            public List<PriceRoutine> Folders { get; set; }
            public class Role
            {
                List<Action> Permissions { get; set; } //which actions can they execute
            }

        }
        //[BsonKnownTypes(typeof(Filter))]
        public class Analytic
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            
            public string Name { get; set; }
            
            public string Status { get; set; }
            
            public string Description { get; set; }
            public DateTime LastUpdated { get; set; }
            public string LastUserUpdated { get; set; }
            public string Comments { get; set; }
            //public string Group1 { get; set; }
            //public string Group2 { get; set; }
            //public string Folder { get; set; }
            public List<string> Tags { get; set; }
            //public IList<string> Drivers { get; set; }
            public bool Shared { get; set; }
            public List<FilterSet> Filters { get; set; }
            [BsonElement("Drivers")]
            public List<ValueDriver> Drivers { get; set; }
            public List<PriceScheme> PriceSchemes { get; set; }
            //public List<PriceRoutine> RelatedPriceRoutinesIdentities { get; set; }
            //public List<Action> Actions { get; set; } // usually navigates to step in module eg - for analytic->filters, price lists, rounding
        }

        public class PriceRoutine
        {
            public PriceRoutineType @Type { get; set; } //everyday, promo, kits
            public List<Analytic> RelatedAnalyticsIdentities { get; set; }
            public List<Action> Actions { get; set; }
            public List<RoundingScheme> RoundingSchemes { get; set; }
            public List<PriceList> PriceLists { get; set; }
        }

        public class PriceScheme
        {
            public PricingMode PricingMode { get; set; }
            public List<PriceList> PriceLists { get; set; }
        }


        public enum PriceListType
        {
            Cost,
            Sales,

        }

        public class PriceList
        {
            public int SortId { get; set; }
            public string Type { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public Boolean IsKey { get; set; }
        }

        public enum PricingMode
        {
            Single,
            Cascade,
            Global,
            GlobalPlus
        }


        public class RoundingScheme
        {
            public string Lower { get; set; }
            public string Upper { get; set; }
            public int RoundTo { get; set; }
        }


        public enum PriceRoutineType
        {
            EveryDay,
            Promo,
            Kit
        }

        public enum ModuleType
        {
            Planning,
            Tracking,
            Reporting,
            Administration
        }

        public enum ValueDriverType
        {
            Movement,
            Markup,
            DaysOnHand,
            DaysLeadTime,
            InStockRatio,
            SalesTrendRatio
        }

        public class ValueDriver
        {
            public ValueDriverType @Type { get; set; }
            public Mode Mode { get; set;}
            public List<Group> Groups { get; set; }
            
        }

        public enum Mode
	    {
	        Auto,
            Manual
	    }

        public class Group
        {
            public int LineItemId { get; set; }
            public string GroupName { get; set; }
            public int SkuCount { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public double SalesValue { get; set; }
        }


        public enum FilterType
        {
            VendorCode,
            IsKit,
            OnSale,
            Category,
            DiscountType,
            StatusType,
            ProductType,
            StockClass
        }

        

        public class Filter
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            //public Boolean IsSelected { get; set; } //TODO: should be in viewmodel
            [BsonElement("code")]
            public string Code { get; set; }
            [BsonElement("description")]
            public string Description { get; set; }
            public FilterType Type { get; set; }
        }

        public class FilterSet
        {
            public string Type { get; set; }
            public List<Filter> Items { get; set; }
        }

        public class EntityBase
        {
            
        }
    
}
