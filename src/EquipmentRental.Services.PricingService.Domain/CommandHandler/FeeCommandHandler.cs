using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using EquipmentRental.Services.PricingService.Domain.Command;
using EquipmentRental.Services.PricingService.Domain.WriteModel;

namespace EquipmentRental.Services.PricingService.Domain.CommandHandler
{
    public class FeeCommandHandler : ICommandHandler<CreateFeeCommand>, ICommandHandler<UpdateFeeCostCommand>
    {
        private readonly ISession _session;

        public FeeCommandHandler(ISession session)
        {
            _session = session;
        }

        public Task Handle(CreateFeeCommand message)
        {
            var fee = new Fee(message.Id, message.FeeId, message.Tag, message.Cost);
            _session.Add(fee);
            return _session.Commit();
        }

        public async Task Handle(UpdateFeeCostCommand message)
        {
            var fee = await _session.Get<Fee>(message.Id);
            fee.ChangeCost(message.Cost);
            await _session.Commit();
        }
    }
}
