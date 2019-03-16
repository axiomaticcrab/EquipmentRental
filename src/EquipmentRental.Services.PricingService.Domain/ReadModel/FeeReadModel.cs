using System;
using EquipmentRental.Services.PricingService.Domain.WriteModel;
using EquipmentRental.Util.Repository;

namespace EquipmentRental.Services.PricingService.Domain.ReadModel
{
    public class FeeReadModel : Entity
    {
        public Guid AggregateId { get; set; }
        public string Tag { get; set; }
        public int Cost { get; set; }
    }

    public class LoyaltyReadModel : Entity
    {
        public Guid AggregateId { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public int Points { get; set; }
    }
}
