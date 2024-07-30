using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Common
{
    public abstract class AuditableEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; } = DateTime.UtcNow;

        public string? LastModifiedBy { get; set; }
    }
}
