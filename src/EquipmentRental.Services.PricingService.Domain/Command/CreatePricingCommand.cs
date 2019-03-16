using System;
using EquipmentRental.Services.PricingService.Domain.WriteModel;

namespace EquipmentRental.Services.PricingService.Domain.Command
{
    public class CreatePricingCommand : BaseCommand
    {
        public readonly int PricingId;
        public readonly EquipmentType EquipmentType;
        public readonly int StartingDay;
        public readonly int EndingDay;
        public readonly string FeeTag;

        public CreatePricingCommand(Guid id, int pricingId, EquipmentType equipmentType,int startingDay,int endingDay,string feeTag)
        {
            Id = id;
            PricingId = pricingId;
            EquipmentType = equipmentType;
            StartingDay = startingDay;
            EndingDay = endingDay;
            FeeTag = feeTag;
        }
    }
}