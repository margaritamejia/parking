using ParkingRate.Core.Configuration;
using ParkingRate.Core.Services;
using Xunit;

namespace ParkingRate.Tests.Core.Services
{
	public class FlatRateCalculatorTests
	{
		public FlatRateCalculatorTests()
		{
			_rateCalculator = new FlatRateCalculator(new FlatRate
			{
				Name = "Flat Rate",
				Price = 10.00m
			});
		}

		private readonly FlatRateCalculator _rateCalculator;

		[Fact]
		public void GetFlatRateAmountForAny()
		{
			var result = _rateCalculator.GetAmount(null);

			Assert.Equal(10.00m, result);
		}

		[Fact]
		public void GetFlatRateNameForAny()
		{
			var result = _rateCalculator.GetRateName();

			Assert.Equal("Flat Rate", result);
		}
	}
}