using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWedApi.Models;

namespace OrderWedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderController()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongourl = MongoUrl.Create(connectionString);
            var mongoclient = new MongoClient(mongourl);
            var database = mongoclient.GetDatabase(mongourl.DatabaseName);

            _orderCollection = database.GetCollection<Order>("order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetById(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);

            return await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            await _orderCollection.InsertOneAsync(order);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Updat(Order order)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterDefinition, order);

            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> Delete(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);

            await _orderCollection.DeleteOneAsync(filterDefinition);

            return Ok();
        }
    }
}
