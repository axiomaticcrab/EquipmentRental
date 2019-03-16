using System;
using EquipmentRental.Services.PricingService.Domain.WriteModel;

namespace EquipmentRental.Services.PricingService.Domain.Command
{
    public class CreateLoyaltyCommand : BaseCommand
    {
        public readonly int LoyaltyId;
        public readonly EquipmentType EquipmentType;
        public readonly int Points;

        public CreateLoyaltyCommand(Guid id, int loyaltyId, EquipmentType equipmentType,int points)
        {
            Id = id;
            LoyaltyId = loyaltyId;
            EquipmentType = equipmentType;
            Points = points;
        }
    }
}