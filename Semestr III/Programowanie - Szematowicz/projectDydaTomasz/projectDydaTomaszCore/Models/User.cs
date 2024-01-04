using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace projectDydaTomaszCore.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"UserID: {UserId}\nUsername: {Username}\nPassword: {PasswordHash}\nEmail: {Email}";
        }
    }    
}
