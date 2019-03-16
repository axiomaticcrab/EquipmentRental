using EquipmentRental.Services.BasketService.Controllers;
using EquipmentRental.Util.Repository;
using StackExchange.Redis;

namespace EquipmentRental.Services.BasketService.Repository
{

    public interface IBasketRepository : IRepository<Basket>
    {
        Basket Save(Basket basket);
        Basket ById(int id);
        Basket Update(Basket basket);
    }

    public class BasketRepository : RedisRepository<Basket>, IBasketRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public BasketRepository(IConnectionMultiplexer redis) : base(redis, "basket")
        {
            _redis = redis;
        }

        public Basket Save(Basket basket)
        {
            Save(basket.Id, basket);
            return basket;
        }

        public Basket ById(int id)
        {
            return Get(id);
        }

        public Basket Update(Basket basket)
        {
            Save(basket.Id, basket);
            return basket;
        }
    }
}
