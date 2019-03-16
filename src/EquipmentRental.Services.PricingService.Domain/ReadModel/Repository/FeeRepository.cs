using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using EquipmentRental.Services.PricingService.Domain.WriteModel;
using EquipmentRental.Util.Repository;
using StackExchange.Redis;

namespace EquipmentRental.Services.PricingService.Domain.ReadModel
{
    public class FeeRepository : RedisRepository<FeeReadModel>, IFeeRepository
    {
        public FeeRepository(IConnectionMultiplexer redis) : base(redis, "fee")
        {
        }
    }

    public class LoyaltyRepository : RedisRepository<LoyaltyReadModel>, ILoyaltyRepository
    {
        public LoyaltyRepository(IConnectionMultiplexer redis) : base(redis, "loyalty")
        {
        }
    }
}