﻿using CQRSlite.Commands;
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
    }
}
