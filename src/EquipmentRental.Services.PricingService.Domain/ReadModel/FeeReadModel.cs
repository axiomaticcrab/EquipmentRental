using System;

namespace EquipmentRental.Services.PricingService.Domain.ReadModel
{
    public class FeeReadModel
    {
        public Guid AggregateId { get; set; }
        public int FeeId { get; set; }
        public string Tag { get; set; }
        public int Cost { get; set; }
    }
}
