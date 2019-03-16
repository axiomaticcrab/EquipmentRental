using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using EquipmentRental.Services.PricingService.Domain.Command;
using EquipmentRental.Services.PricingService.Domain.WriteModel;

namespace EquipmentRental.Services.PricingService.Domain.CommandHandler
{
    public class LoyaltyCommandHandler : ICommandHandler<CreateLoyaltyCommand>
    {
        private readonly ISession _session;

        public LoyaltyCommandHandler(ISession session)
        {
            _session = session;
        }

        public Task Handle(CreateLoyaltyCommand message)
        {
            var loyalty = new Loyalty(message.Id, message.LoyaltyId, message.EquipmentType, message.Points);
            _session.Add(loyalty);
            return _session.Commit();
        }
    }
}