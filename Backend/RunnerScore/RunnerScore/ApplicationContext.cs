using MongoDB.Driver;

namespace RunnerScore;

public class ApplicationContext<T>
{
    private IMongoDatabase Database { get; set; }
    private MongoClient MongoClient { get; set; }

    public ApplicationContext()
    {
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        ConfigureMongo();

        return Database.GetCollection<T>(name);
    }

    private void ConfigureMongo()
    {
        string connectionString = "mongodb://localhost:27017/UserRunner";

        var connection = new MongoUrlBuilder(connectionString);

        MongoClient = new MongoClient(connectionString);

        Database = MongoClient.GetDatabase(connection.DatabaseName);
    }
}
