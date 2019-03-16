using System;
using EquipmentRental.Services.PricingService.Domain.WriteModel;

namespace EquipmentRental.Services.PricingService.Domain.Event
{
    public class FeeCreatedEvent : BaseEvent
    {
        public readonly int FeeId;
        public readonly string Tag;
        public readonly int Cost;

        public FeeCreatedEvent(Guid id,int feeId,string tag,int cost)
        {
            Id = id;
            FeeId = feeId;
            Tag = tag;
            Cost = cost;
        }
    }

    public class FeeCostChangedEvent : BaseEvent
    {
        public readonly int FeeId;
        public readonly int OldCost;
        public readonly int NewCost;

        public FeeCostChangedEvent(Guid id,int feeId,int oldCost,int newCost)
        {
            Id = id;
            FeeId = feeId;
            OldCost = oldCost;
            NewCost = newCost;
        }
    }

    public class LoyaltyCreatedEvent : BaseEvent
    {
        public readonly int LoyaltyId;
        public readonly EquipmentType EquipmentType;
        public readonly int Points;

        public LoyaltyCreatedEvent(Guid id, int loyaltyId, EquipmentType equipmentType,int points)
        {
            Id = id;
            LoyaltyId = loyaltyId;
            EquipmentType = equipmentType;
            Points = points;
        }
    }

    public class PricingCreatedEvent : BaseEvent
    {
        public readonly int PricingId;
        public readonly EquipmentType EquipmentType;
        public readonly int StartingDay;
        public readonly int EndingDay;
        public readonly string FeeTag;

        public PricingCreatedEvent(Guid id, int pricingId, EquipmentType equipmentType, int startingDay, int endingDay, string feeTag)
        {
            PricingId = pricingId;
            EquipmentType = equipmentType;
            StartingDay = startingDay;
            EndingDay = endingDay;
            FeeTag = feeTag;
            Id = id;
        }
    }
}
