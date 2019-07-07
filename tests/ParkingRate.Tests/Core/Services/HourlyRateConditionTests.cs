using System;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Models;
using ParkingRate.Core.Services;
using Xunit;

namespace ParkingRate.Tests.Core.Services
{
	public class HourlyRateConditionTests
	{
		public HourlyRateConditionTests()
		{
			_rateCalculator = new HourlyRateCondition(new HourlyRate
			{
				MaxHours = 3
			});
		}

		private readonly HourlyRateCondition _rateCalculator;

		[Fact]
		public void HourlyRateIsInvalid()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 06, 0, 0, 0),
				Exit = new DateTime(2019, 07, 06, 3, 0, 1)
			});

			Assert.False(result);
		}

		[Fact]
		public void HourlyRateIsValidUnderMax()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 06, 0, 0, 0),
				Exit = new DateTime(2019, 07, 06, 2, 59, 59)
			});

			Assert.True(result);
		}

		[Fact]
		public void HourlyRateIsValidWithMaxTime()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 06, 0, 0, 0),
				Exit = new DateTime(2019, 07, 06, 3, 00, 00)
			});

			Assert.True(result);
		}

		[Fact]
		public void HourlyRateIsValidWithSeconds()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 06, 0, 0, 0),
				Exit = new DateTime(2019, 07, 06, 0, 0, 1)
			});

			Assert.True(result);
		}
	}
}