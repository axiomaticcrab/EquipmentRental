using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentRental.Services.PricingService.Controllers;
using EquipmentRental.Services.PricingService.Domain.ReadModel;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using EquipmentRental.Services.PricingService.Domain.WriteModel;
using EquipmentRental.Services.PricingService.Model;
using EquipmentRental.Services.PricingService.Services;
using Moq;
using Xunit;

namespace EquipmentRental.Service.Pricing.UnitTests
{
    public class CalculatorControllerTest
    {
        private readonly Mock<IFeeRepository> _feeRepository;
        private readonly Mock<IPricingRepository> _pricingRepository;
        private readonly Mock<ILoyaltyRepository> _loyaltyRepository;
        private readonly Mock<IEquipmentService> _equipmentService;

        public CalculatorControllerTest()
        {
            _feeRepository = new Mock<IFeeRepository>();
            _pricingRepository = new Mock<IPricingRepository>();
            _loyaltyRepository = new Mock<ILoyaltyRepository>();
            _equipmentService = new Mock<IEquipmentService>();
        }


        [Fact]
        public async Task Should_Calculate_TotalAmount()
        {
            _equipmentService.Setup(x => x.GetEquipments())
                .Returns(Task.FromResult<List<EquipmentModel>>(new List<EquipmentModel>
                {
                    new EquipmentModel
                    {
                        EquipmentType = EquipmentType.Heavy,
                        Name = "heavy"
                    }
                }));

            _loyaltyRepository.Setup(x => x.GetMultiple(It.IsAny<List<int>>()))
                .Returns(new List<LoyaltyReadModel>());

            _pricingRepository.Setup(x => x.GetMultiple(It.IsAny<List<int>>()))
                .Returns(new List<PricingReadModel>
                {
                    new PricingReadModel
                    {
                        EquipmentType = EquipmentType.Heavy,
                        FeeTag = "one-time",
                        StartingDay = 0,
                        EndingDay = 0,
                    }
                });

            _feeRepository.Setup(x => x.GetMultiple(It.IsAny<List<int>>()))
                .Returns(new List<FeeReadModel>
                {
                    new FeeReadModel
                    {
                        Cost = 1,
                        Tag = "one-time",
                    }
                });

            var controller = new CalculatorController(_feeRepository.Object, _pricingRepository.Object,
                _loyaltyRepository.Object, _equipmentService.Object);

            var result = await controller.Post(new CalculationRequest
            {
                LineItems = new List<LineItemModel>
                {
                    new LineItemModel
                    {
                        RentalDays = 4,
                        EquipmentName = "heavy"
                    }
                }
            });

            Assert.Equal(1,result.TotalPrice);
        }

        [Fact]
        public async Task Should_Calculate_LoyaltyPoints()
        {

        }

        /*
         * ...
         *
         * Could not add more cases because my time runs out
         *
         * ...
         *
         */
    }
}
