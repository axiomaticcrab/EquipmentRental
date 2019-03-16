using CQRSlite.Commands;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Services.Pricing.PricingService.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IPricingRepository _repository;

        public PricingController(ICommandSender commandSender, IPricingRepository repository)
        {
            _commandSender = commandSender;
            _repository = repository;
        }
    }
}
