using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
    internal class CartService : ICartService
    {
        public readonly IDatabase _database; 
        public CartService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCart(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<ShoppingCart?> GetCartAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
        {
            var isSaved = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(20));

            if (!isSaved) return null;

            return await GetCartAsync(cart.Id);
        
        }
    }
}
