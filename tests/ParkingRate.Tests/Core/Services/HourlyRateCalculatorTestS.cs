using System;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Models;
using ParkingRate.Core.Services;
using Xunit;

namespace ParkingRate.Tests.Core.Services
{
	public class HourlyRateCalculatorTests
	{
		public HourlyRateCalculatorTests()
		{
			_rateCalculator = new HourlyRateCalculator(new HourlyRate
			{
				Price = 5.00m,
				Name = "Standard"
			});
		}

		private readonly HourlyRateCalculator _rateCalculator;

		[Fact]
		public void CalculateForA3Hours()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 01, 03, 00, 0)
			});

			Assert.Equal(15.00m, result);
		}

		[Fact]
		public void CalculateForAFullDay()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 02, 00, 00, 0)
			});

			Assert.Equal(120.00m, result);
		}

		[Fact]
		public void CalculateForAFullHour()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 01, 01, 00, 0)
			});

			Assert.Equal(5.00m, result);
		}

		[Fact]
		public void CalculateForAHalfHour()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 01, 00, 30, 0)
			});

			Assert.Equal(5.00m, result);
		}

		[Fact]
		public void CalculateForHalfDay()
		{
			var result = _rateCalculator.GetAmount(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 00, 0, 0),
				Exit = new DateTime(2019, 07, 01, 12, 00, 0)
			});

			Assert.Equal(60.00m, result);
		}

		[Fact]
		public void GetDefaultRateName()
		{
			var result = _rateCalculator.GetRateName();

			Assert.Equal("Standard", result);
		}
	}
}