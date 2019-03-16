using System;
using CQRSlite.Commands;
using EquipmentRental.Services.PricingService.Domain.Command;
using EquipmentRental.Services.PricingService.Domain.WriteModel;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Services.Pricing.PricingService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitializerController : ControllerBase
    {
        private readonly ICommandSender _commandSender;

        public InitializerController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        [HttpGet]
        public IActionResult Init()
        {
            var oneTimeFeeAggId = Guid.NewGuid();
            _commandSender.Send(new CreateFeeCommand(oneTimeFeeAggId, 1, "one-time", 11));
            _commandSender.Send(new CreateFeeCommand(Guid.NewGuid(), 2, "premium", 60));
            _commandSender.Send(new CreateFeeCommand(Guid.NewGuid(), 3, "regular", 40));
            _commandSender.Send(new UpdateFeeCostCommand(oneTimeFeeAggId, 1, 100));

            _commandSender.Send(new CreateLoyaltyCommand(Guid.NewGuid(), 1, EquipmentType.Heavy, 2));
            _commandSender.Send(new CreateLoyaltyCommand(Guid.NewGuid(), 2, EquipmentType.Regular, 1));
            _commandSender.Send(new CreateLoyaltyCommand(Guid.NewGuid(), 3, EquipmentType.Specialized, 1));

            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 1, EquipmentType.Heavy, 0, 0, "one-time"));
            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 2, EquipmentType.Heavy, 0, int.MaxValue, "premium"));

            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 3, EquipmentType.Regular, 0, 0, "one-time"));
            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 4, EquipmentType.Regular, 0, 2, "premium"));
            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 5, EquipmentType.Regular, 3, int.MaxValue, "reqular"));

            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 6, EquipmentType.Specialized, 0, 3, "one-time"));
            _commandSender.Send(new CreatePricingCommand(Guid.NewGuid(), 7, EquipmentType.Specialized, 4, int.MaxValue, "regular"));

            return Ok();
        }

    }
}
