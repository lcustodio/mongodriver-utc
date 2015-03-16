Mongo Driver - UTC test suite

The goal is to test the behavior of C# Date types when store on MongoDb.
Tests was done with:

DateTime.Now
DateTime.UtcNow

DateTimeOffset.UtcNow
DateTimeOffset.Now

	DateTimeOffset is not a BsonValue valid type, the conversion is based on the [official driver](https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/ObjectModel/BsonTypeMapper.cs#L519).

The output is:

`
/* 0 */
{
    "_id" : ObjectId("55073c3399df5c158873bef9"),
    "UtcNow" : {
        "date" : ISODate("2015-03-16T20:25:23.879Z"),
        "iso format" : "2015-03-16T21:25:23.8791764+01:00",
        "details" : "UtcNow 2015-03-16T21:25:23.8791764+01:00, Kind = Local"
    }
}

/* 1 */
{
    "_id" : ObjectId("55073c3399df5c158873befa"),
    "Now" : {
        "date" : ISODate("2015-03-16T20:25:23.972Z"),
        "iso format" : "2015-03-16T20:25:23.9729345Z",
        "details" : "Now 2015-03-16T20:25:23.9729345Z, Kind = Utc"
    }
}

/* 2 */
{
    "_id" : ObjectId("55073c3399df5c158873befb"),
    "OffSet UtcNow" : {
        "date" : ISODate("2015-03-16T20:25:23.988Z"),
        "iso format" : "2015-03-16T20:25:23.9887142+00:00",
        "details" : "OffSet UtcNow 2015-03-16T20:25:23.9887142+00:00, OffSet = 00:00:00"
    }
}

/* 3 */
{
    "_id" : ObjectId("55073c3499df5c158873befc"),
    "OffSet Now" : {
        "date" : ISODate("2015-03-16T20:25:24.004Z"),
        "iso format" : "2015-03-16T21:25:24.0042211+01:00",
        "details" : "OffSet Now 2015-03-16T21:25:24.0042211+01:00, OffSet = 01:00:00"
    }
}
`