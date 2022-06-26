using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RunnerScore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RunnerScore.Models;

public class User : IIdentifiable<string>
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public decimal Score { get; set; }

    public static User Of(string name)
    {
        return new User
        {
            Name = name
        };
    }
}
