using System;
using System.Threading.Tasks;
using CQRSlite.Commands;
using EquipmentRental.Services.Pricing.PricingService.Command.Model;
using EquipmentRental.Services.PricingService.Domain.Command;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Services.Pricing.PricingService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly ILoyaltyRepository _repository;

        public LoyaltyController(ICommandSender commandSender, ILoyaltyRepository repository)
        {
            _commandSender = commandSender;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFeeModel createFeeModel)
        {
            await _commandSender.Send(new CreateFeeCommand(Guid.NewGuid(), createFeeModel.FeeId, createFeeModel.Tag,
                createFeeModel.Cost));
            return Ok();
        }
    }
}
