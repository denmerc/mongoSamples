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
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
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
            [BsonElement("name")]
            public string Name { get; set; }
            [BsonElement("status")]
            public string Status { get; set; }
            [BsonElement("description")]
            public string Description { get; set; }
            public DateTime lastUpdated { get; set; }
            public string lastUserUpdated { get; set; }
            public string Comments { get; set; }
            //public string Group1 { get; set; }
            //public string Group2 { get; set; }
            //public string Folder { get; set; }
            [BsonElement("tags")]
            public IList<string> Tags { get; set; }
            public bool Shared { get; set; }
            [BsonElement("filters")]
            public List<SelectedFilter> Filters { get; set; }
            public List<ValueDriver> ValueDrivers { get; set; }
            public List<PriceRoutine> RelatedPriceRoutinesIdentities { get; set; }
            public List<Action> Actions { get; set; } // usually navigates to step in module eg - for analytic->filters, price lists, rounding
        }

        public class PriceRoutine
        {
            public PriceRoutineType @Type { get; set; } //everyday, promo, kits
            public List<Analytic> RelatedAnalyticsIdentities { get; set; }
            public List<Action> Actions { get; set; }
            public List<RoundingScheme> RoundingSchemes { get; set; }
            public List<PriceList> PriceLists { get; set; }
        }


        public class PriceList
        {

        }

        public enum PricingMode
        {
            Single,
            Cascade,
            GlobalKey,
            GlobalKeyPlus
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


        public class ValueDriver
        {
            public int Group { get; set; }
            public ValueDriverType @Type { get; set; }
        }

        public enum ValueDriverType
        {

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
        }

        public class SelectedFilter
        {
            [BsonElement("type")]
            public string Type { get; set; }
            [BsonElement("items")]
            public List<Filter> Filters { get; set; }
        }

        public class EntityBase
        {
            
        }
    
}
