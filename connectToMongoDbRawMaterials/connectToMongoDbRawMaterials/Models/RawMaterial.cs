using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectToMongoDbRawMaterials.Models
{
    public class RawMaterial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string _index { get; set; }
        [BsonElement("Name")]
        public string _fullName { get; set; }
        public string _supplier { get; set; }
        public DateOnly _acceptanceDate { get; set; }
        public DateOnly _expiriationDate { get; set; }
        public int _amount { get; set; }
        public string _storagePlace { get; set; }
        public string _destiny { get; set; }
    }
}
