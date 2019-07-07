using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using NSubstitute;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Models;
using ParkingRate.Core.Services;

namespace ParkingRate.Tests.Core.Services
{
	public class RateCalculatorFactoryTests
	{
		[Fact]
		public void EmptyConfigThrowsException()
		{
			var mockConfig = Substitute.For<IOptions<RatesConfiguration>>();

			var mockLogger = Substitute.For<ILogger<RateCalculatorFactory>>();


			Assert.Throws<ArgumentNullException>(() => new RateCalculatorFactory(mockConfig, mockLogger));
		}

		[Fact]
		public void GetsDefaultCalculator()
		{
			var mockConfig = Substitute.For<IOptions<RatesConfiguration>>();

			var mockLogger = Substitute.For<ILogger<RateCalculatorFactory>>();


			mockConfig.Value.ReturnsForAnyArgs(new RatesConfiguration()
			{
				DefaultRate = new DefaultRate()
				{
					Price = 20.00m
				}
			});

			var rateCalculator = new RateCalculatorFactory(mockConfig, mockLogger);

			var result = rateCalculator.GetRateCalculator(new ParkingParameters());

			Assert.IsType<ParkingRate.Core.Services.DefaultRateCalculator>(result);
		}
	}
}

