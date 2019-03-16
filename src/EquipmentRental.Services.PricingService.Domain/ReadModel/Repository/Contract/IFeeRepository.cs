using EquipmentRental.Services.PricingService.Domain.WriteModel;
using EquipmentRental.Util.Repository;

namespace EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract
{
    public interface IFeeRepository : IRepository<FeeReadModel>
    {
    }
}
