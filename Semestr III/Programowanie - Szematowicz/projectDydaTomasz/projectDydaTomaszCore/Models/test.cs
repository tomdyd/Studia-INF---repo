using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projectDydaTomaszCore.Models
{
    public class test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int MyNum { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\nMyNum: {MyNum}\nName:{Name}";
        }
    }
}
