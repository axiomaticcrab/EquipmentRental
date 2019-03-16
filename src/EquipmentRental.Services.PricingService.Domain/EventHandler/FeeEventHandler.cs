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
        private readonly IFeeRepository _feeRepository;

        public FeeEventHandler(IFeeRepository repository, IFeeRepository feeRepository)
        {
            _repository = repository;
            _feeRepository = feeRepository;
        }

        public Task Handle(FeeCreatedEvent message)
        {
            _repository.Save(new FeeReadModel
            {
                Cost = message.Cost,
                FeeId = message.FeeId,
                Tag = message.Tag,
                AggregateId = message.Id
            });
            return Task.CompletedTask;
        }

        public Task Handle(FeeCostChangedEvent message)
        {
            var fee = _feeRepository.GetById(message.FeeId);
            fee.Cost = message.NewCost;
            _repository.Save(fee);
            return Task.CompletedTask;
        }
    }
}
