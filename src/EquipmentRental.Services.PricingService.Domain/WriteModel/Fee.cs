using System;
using CQRSlite.Domain;
using EquipmentRental.Services.PricingService.Domain.Event;

namespace EquipmentRental.Services.PricingService.Domain.WriteModel
{
    public class Fee : AggregateRoot
    {
        private int _feeId;
        private string _tag;
        private int _cost;

        public Fee(Guid id, int feeId, string tag, int cost)
        {
            Id = id;
            _feeId = feeId;
            _tag = tag;
            _cost = cost;

            ApplyChange(new FeeCreatedEvent(id, feeId, tag, cost));
        }

        public void ChangeCost(int cost)
        {
            var oldCost = _cost;
            _cost = cost;

            ApplyChange(new FeeCostChangedEvent(Id,_feeId, oldCost, cost));
        }

    }
}
