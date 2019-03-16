using System;
using CQRSlite.Domain;
using EquipmentRental.Services.PricingService.Domain.Event;

namespace EquipmentRental.Services.PricingService.Domain.WriteModel
{
    public class Loyalty : AggregateRoot
    {
        private int _loyaltyId;
        private EquipmentType _equipmentType;
        private int _points;

        public Loyalty(Guid id,int loyaltyId, EquipmentType equipmentType,int points)
        {
            Id = id;
            _loyaltyId = loyaltyId;
            _equipmentType = equipmentType;
            _points = points;

            ApplyChange(new LoyaltyCreatedEvent(id,loyaltyId,equipmentType,points));
        }
    }
}
