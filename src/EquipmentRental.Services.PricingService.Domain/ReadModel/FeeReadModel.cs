using System;
using EquipmentRental.Util.Repository;

namespace EquipmentRental.Services.PricingService.Domain.ReadModel
{
    public class FeeReadModel : Entity
    {
        public Guid AggregateId { get; set; }
        public string Tag { get; set; }
        public int Cost { get; set; }
    }
}
