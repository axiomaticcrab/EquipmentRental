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
    public class FeeController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IFeeRepository _feeRepository;

        public FeeController(ICommandSender commandSender, IFeeRepository feeRepository)
        {
            _commandSender = commandSender;
            _feeRepository = feeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFeeModel createFeeModel)
        {
            await _commandSender.Send(new CreateFeeCommand(Guid.NewGuid(), createFeeModel.FeeId, createFeeModel.Tag,
                createFeeModel.Cost));
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCost([FromBody] UpdateFeeModel updateFeeModel)
        {
            var feeAggregateId = _feeRepository.GetById(updateFeeModel.FeeId).AggregateId;

            await _commandSender.Send(new UpdateFeeCostCommand(feeAggregateId, updateFeeModel.FeeId, updateFeeModel.Cost));
            return Ok();
        }
    }
}
