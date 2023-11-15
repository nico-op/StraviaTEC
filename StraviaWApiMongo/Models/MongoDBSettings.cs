public class MongoDBSettings : IMongoDBSettings
{
    public string MongoDBConnection { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }


}

public interface IMongoDBSettings
{
    string MongoDBConnection { get; set; }
    string DatabaseName { get; set; }
    string CollectionName { get; set; }

}