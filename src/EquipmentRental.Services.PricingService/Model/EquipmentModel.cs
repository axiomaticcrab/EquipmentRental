using EquipmentRental.Services.PricingService.Domain.WriteModel;

namespace EquipmentRental.Services.PricingService.Services
{
    public class EquipmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}