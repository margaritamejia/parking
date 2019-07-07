using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class HourlyRateCondition : IRateCondition
	{
		private readonly HourlyRate _rates;

		public HourlyRateCondition(HourlyRate rates)
		{
			_rates = rates;
		}

		public bool IsValid(ParkingParameters parkingParameters)
		{
			return _rates.MaxHours >= parkingParameters.Hours;
		}
	}
}