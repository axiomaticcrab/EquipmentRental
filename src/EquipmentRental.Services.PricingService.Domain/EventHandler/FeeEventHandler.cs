using System.Threading.Tasks;
using CQRSlite.Events;
using EquipmentRental.Services.PricingService.Domain.Event;
using EquipmentRental.Services.PricingService.Domain.ReadModel;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;

namespace EquipmentRental.Services.PricingService.Domain.EventHandler
{
    public class FeeEventHandler : IEventHandler<FeeCreatedEvent>, IEventHandler<FeeCostChangedEvent>
    {
        private readonly IFeeRepository _repository;

        public FeeEventHandler(IFeeRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(FeeCreatedEvent message)
        {
            _repository.Save(new FeeReadModel
            {
                Cost = message.Cost,
                EntityId = message.FeeId,
                Tag = message.Tag,
                AggregateId = message.Id
            });
            return Task.CompletedTask;
        }

        public Task Handle(FeeCostChangedEvent message)
        {
            var fee = _repository.GetById(message.FeeId);
            fee.Cost = message.NewCost;
            _repository.Save(fee);
            return Task.CompletedTask;
        }
    }

    public class LoyaltyEventHandler : IEventHandler<LoyaltyCreatedEvent>
    {
        private readonly ILoyaltyRepository _repository;

        public LoyaltyEventHandler(ILoyaltyRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(LoyaltyCreatedEvent message)
        {
            _repository.Save(new LoyaltyReadModel
            {
                AggregateId = message.Id,
                EquipmentType = message.EquipmentType,
                Points = message.Points,
                EntityId = message.LoyaltyId
            });
            return Task.CompletedTask;
        }
    }

    public class PricingEventHandler : IEventHandler<PricingCreatedEvent>
    {
        private readonly IPricingRepository _repository;

        public PricingEventHandler(IPricingRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(PricingCreatedEvent message)
        {
            _repository.Save(new PricingReadModel
            {
                EquipmentType = message.EquipmentType,
                EntityId = message.PricingId,
                FeeTag = message.FeeTag,
                AggregateId = message.Id,
                StartingDay = message.StartingDay,
                EndingDay = message.EndingDay
            });
            return Task.CompletedTask;
        }
    }
}
