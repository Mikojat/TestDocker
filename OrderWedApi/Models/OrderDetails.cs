using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderWedApi.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class OrderDetails
    {
        [BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set; }

        [BsonElement("quantity"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Quantity { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
