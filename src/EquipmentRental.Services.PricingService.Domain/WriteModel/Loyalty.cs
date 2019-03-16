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

        public Loyalty(Guid id, int loyaltyId, EquipmentType equipmentType, int points)
        {
            Id = id;
            _loyaltyId = loyaltyId;
            _equipmentType = equipmentType;
            _points = points;

            ApplyChange(new LoyaltyCreatedEvent(id, loyaltyId, equipmentType, points));
        }
    }

    public class Pricing : AggregateRoot
    {
        private int _pricingId;
        private EquipmentType _equipmentType;
        private int _startingDay;
        private int _endingDay;
        private string _feeTag;

        public Pricing(Guid id, int pricingId, EquipmentType equipmentType, int startingDay, int endingDay, string feeTag)
        {
            Id = id;
            _pricingId = pricingId;
            _equipmentType = equipmentType;
            _startingDay = startingDay;
            _endingDay = endingDay;
            _feeTag = feeTag;

            ApplyChange(new PricingCreatedEvent(id, pricingId, equipmentType, startingDay, endingDay, feeTag));
        }
    }
}
