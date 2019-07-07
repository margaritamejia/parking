using System;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Models;
using ParkingRate.Core.Services;
using Xunit;

namespace ParkingRate.Tests.Core.Services
{
	public class FlatRateConditionTests
	{
		public FlatRateConditionTests()
		{
			_rateCalculator = new FlatRateCondition(new FlatRate
			{
				EntryTimeRange = new Range
				{
					Start = new TimeSpan(6, 0, 0),
					End = new TimeSpan(9, 0, 0)
				},
				ExitTimeRange = new Range
				{
					Start = new TimeSpan(15, 30, 0),
					End = new TimeSpan(23, 30, 0)
				},
				EntryDaysRange = new[] {1, 2, 3, 4, 5},
				ExitDaysRange = new[] {1, 2, 3, 4, 5},
				MaxDays = 0
			});
		}

		private readonly FlatRateCondition _rateCalculator;

		[Fact]
		public void AppliesWithMaxTime()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 6, 0, 0),
				Exit = new DateTime(2019, 07, 01, 23, 30, 0)
			});

			Assert.True(result);
		}

		[Fact]
		public void EntryInvalidExitInvalid()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 5, 0, 0),
				Exit = new DateTime(2019, 07, 01, 6, 30, 0)
			});

			Assert.False(result);
		}

		[Fact]
		public void EntryInvalidExitValid()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 5, 59, 59),
				Exit = new DateTime(2019, 07, 01, 23, 30, 0)
			});

			Assert.False(result);
		}

		[Fact]
		public void EntryValidExitInvalid()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 7, 0, 0),
				Exit = new DateTime(2019, 07, 01, 23, 30, 1)
			});

			Assert.False(result);
		}

		[Fact]
		public void ValidTimesInvalidDay()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 06, 6, 0, 0),
				Exit = new DateTime(2019, 07, 06, 23, 30, 0)
			});

			Assert.False(result);
		}

		[Fact]
		public void ValidTimesMoreThanOneDay()
		{
			var result = _rateCalculator.IsValid(new ParkingParameters
			{
				Entry = new DateTime(2019, 07, 01, 6, 0, 0),
				Exit = new DateTime(2019, 07, 02, 23, 30, 0)
			});

			Assert.False(result);
		}
	}
}