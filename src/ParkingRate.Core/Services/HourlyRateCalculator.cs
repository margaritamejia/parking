using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class HourlyRateCalculator : IRateCalculator
	{
		private readonly HourlyRate _rate;

		public HourlyRateCalculator(HourlyRate rates)
		{
			_rate = rates;
		}


		public decimal GetAmount(ParkingParameters parameters)
		{
			return parameters.Hours * _rate.Price;
		}

		public string GetRateName()
		{
			return _rate.Name;
		}
	}
}