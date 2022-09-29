using Microservice.Shared.Dtos;
using Services.Basket.Dtos;
using System.Text.Json;

namespace Services.Basket.Services
{
    public class BasketManager : IBasketService
    {
        private readonly RedisManager _redisManager;

        public BasketManager(RedisManager redisManager)
        {
            _redisManager = redisManager;
        }

        public async Task<bool> Delete(string userId)
        {
            var status = await _redisManager.GetDb().KeyDeleteAsync(userId);

            return status;
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisManager.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<bool> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisManager.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status;

        }
    }
}
