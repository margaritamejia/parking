using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class FlatRateCalculator : IRateCalculator
	{
		private readonly FlatRate _rateConditions;

		public FlatRateCalculator(FlatRate rateConditions)
		{
			_rateConditions = rateConditions;
		}

		public decimal GetAmount(ParkingParameters parameters)
		{
			return _rateConditions.Price;
		}

		public string GetRateName()
		{
			return _rateConditions.Name;
		}
	}
}