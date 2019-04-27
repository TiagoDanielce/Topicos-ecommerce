using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Topicos.Models
{
    public class BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
