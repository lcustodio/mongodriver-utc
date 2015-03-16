using System;
using System.Collections.Generic;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoUtcDates
{
    class Sample
    {
        private static MongoCollection<BsonDocument> _collection;

        public static void Main()
        {
            const string connectionString = "mongodb://mongo";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("CShartDriver");
            _collection = database.GetCollection("dates");

            _collection.RemoveAll();
        
            DisplayNow("UtcNow", DateTime.Now);
            DisplayNow("Now", DateTime.UtcNow);

            DisplayNow("OffSet UtcNow", DateTimeOffset.UtcNow);
            DisplayNow("OffSet Now", DateTimeOffset.Now);
        }

        public static void DisplayNow(string title, DateTime inputDt)
        {
            var dtString = inputDt.ToString("o");

            var details = String.Format("{0} {1}, Kind = {2}", title, dtString, inputDt.Kind);
            Trace.WriteLine(details);

            SaveDatesToMongo(title, inputDt, dtString, details);
        }


        private static void DisplayNow(string title, DateTimeOffset inputDt)
        {
            var dtString = inputDt.ToString("O");
            var details = String.Format("{0} {1}, OffSet = {2}", title, dtString, inputDt.Offset);
            Trace.WriteLine(details);

            //https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/ObjectModel/BsonTypeMapper.cs#L519
            BsonValue valuea = new BsonDateTime((inputDt).UtcDateTime);
            SaveDatesToMongo(title, valuea, dtString, details);
        }

        public static void SaveDatesToMongo(string title, BsonValue date, string isoFormat, string details)
        {
            var date0 = new BsonElement("date", date);
            var date1 = new BsonElement("iso format", isoFormat);
            var date2 = new BsonElement("details", details);

            var dates = new BsonDocument();
            dates.AddRange(new List<BsonElement> { date0, date1, date2 });

            var doc = new BsonDocument(title, dates);
            _collection.Insert(doc);
        }
    }
}