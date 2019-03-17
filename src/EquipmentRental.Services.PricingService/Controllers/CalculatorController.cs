using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using EquipmentRental.Services.PricingService.Model;
using EquipmentRental.Services.PricingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Services.PricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IFeeRepository _feeRepository;
        private readonly IPricingRepository _pricingRepository;
        private readonly ILoyaltyRepository _loyaltyRepository;
        private readonly EquipmentService _equipmentService;

        public CalculatorController(IFeeRepository feeFeeRepository, IPricingRepository pricingRepository, ILoyaltyRepository loyaltyRepository, EquipmentService equipmentService)
        {
            _feeRepository = feeFeeRepository;
            _pricingRepository = pricingRepository;
            _loyaltyRepository = loyaltyRepository;
            _equipmentService = equipmentService;
        }

        
        [HttpPost]
        public async Task<CalculationResult> Post([FromBody] CalculationRequest calculationRequest)
        {
            var equipments = await _equipmentService.GetEquipments();
            var prices = _pricingRepository.GetMultiple(new List<int> { 1, 2, 3, 4, 5, 6, 7 });
            var loyalties = _loyaltyRepository.GetMultiple(new List<int> { 1, 2, 3 });
            var fees = _feeRepository.GetMultiple(new List<int> { 1, 2, 3 });

            //todo : needs to be unit tested!
            var result = new CalculationResult();

            int loyaltyPoints = 0;
            foreach (var requestLineItem in calculationRequest.LineItems)
            {
                int lineItemCost = 0;
                var equipmentType = equipments.First(x => x.Name == requestLineItem.EquipmentName).EquipmentType;

                prices.Where(x => x.EquipmentType == equipmentType).ToList().ForEach(x =>
                    {
                        if (x.StartingDay == 0 && x.EndingDay == 0)
                        {
                            lineItemCost += fees.Where(y => y.Tag == x.FeeTag).Sum(z => z.Cost);
                        }

                        if (x.StartingDay <= requestLineItem.RentalDays && requestLineItem.RentalDays <= x.EndingDay)
                        {
                            lineItemCost += fees.Where(y => y.Tag == x.FeeTag).Sum(z => z.Cost);
                        }
                    });

                loyaltyPoints += loyalties.Where(x => x.EquipmentType == equipmentType).Sum(x => x.Points);

                result.GenerateInvoiceLineItemResults.Add(new LineItemCalculationResult
                {
                    Name = requestLineItem.EquipmentName,
                    Cost = lineItemCost
                });
            }

            result.LoyaltyPoints = loyaltyPoints;
            result.TotalPrice = result.GenerateInvoiceLineItemResults.Sum(x => x.Cost);

            return result;
        }
    }
}
