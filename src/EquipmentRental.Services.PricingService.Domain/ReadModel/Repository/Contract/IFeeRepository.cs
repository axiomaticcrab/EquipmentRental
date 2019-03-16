using EquipmentRental.Util.Repository;

namespace EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract
{
    public interface IFeeRepository : IRepository<FeeReadModel>
    {
    }

    public interface ILoyaltyRepository : IRepository<LoyaltyReadModel>
    {
    }
}
