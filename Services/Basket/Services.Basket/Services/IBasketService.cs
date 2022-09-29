using Microservice.Shared.Dtos;
using Services.Basket.Dtos;

namespace Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<bool> SaveOrUpdate(BasketDto basketDto);

        Task<bool> Delete(string userId);
    }
}
