using System;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Models;
using ParkingRate.Core.Services;
using Xunit;

namespace ParkingRate.Tests.Core.Services
{
	public class DefaultRateCalculatorTests
	{
		public DefaultRateCalculatorTests()
		{
			_rateCalculator = new DefaultRateCalculator(new DefaultRate
			{
				Price = 20.00m,
				Name = "Default"
			});
		}

		private readonly DefaultRateCalculator _rateCalculator;

		[Fact]
		public void CalculateForAFullDay()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 02, 00, 00, 0)
			});

			Assert.Equal(20.00m, result);
		}

		[Fact]
		public void CalculateForHalfFullDay()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 01, 12, 00, 0)
			});

			Assert.Equal(20.00m, result);
		}

		[Fact]
		public void GetDefaultRateNameForAny()
		{
			var result = _rateCalculator.GetRateName();

			Assert.Equal("Default", result);
		}
	}
}