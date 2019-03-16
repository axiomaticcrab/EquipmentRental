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

        public class PricingCommandHandler : ICommandHandler<CreatePricingCommand>
        {
            private readonly ISession _session;

            public PricingCommandHandler(ISession session)
            {
                _session = session;
            }

            public Task Handle(CreatePricingCommand message)
            {
                var pricing = new Pricing(message.Id, message.PricingId, message.EquipmentType, message.StartingDay,
                    message.EndingDay, message.FeeTag);
                _session.Add(pricing);
                return _session.Commit();
            }
        }
    }
}