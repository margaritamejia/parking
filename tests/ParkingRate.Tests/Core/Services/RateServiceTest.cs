using System;
using NSubstitute;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;
using ParkingRate.Core.Services;
using Xunit;

namespace ParkingRate.Tests.Core.Services
{
	public class RateServiceTest
	{
		[Fact]
		public void EmptyConfigThrowsException()
		{
			var mockRateCalculatorFactory = Substitute.For<IRateCalculatorFactory>();
			var mockCalculatorFactory = Substitute.For<IRateCalculator>();


			var parameters = new ParkingParameters
			{
				Entry = DateTime.Today,
				Exit = DateTime.Now
			};

			mockRateCalculatorFactory.GetRateCalculator(parameters).Returns(mockCalculatorFactory);
			mockCalculatorFactory.GetAmount(parameters).Returns(10.00m);

			mockCalculatorFactory.GetRateName().Returns("standard");

			var rateService = new RateService(mockRateCalculatorFactory);

			var result = rateService.GetTicket(parameters);

			Assert.Equal(10.00m, result.Result.Amount);

			Assert.Equal("standard", result.Result.RateName);
		}
	}
}