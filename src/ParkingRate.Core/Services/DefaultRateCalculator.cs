using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class DefaultRateCalculator : IRateCalculator
	{
		private readonly DefaultRate _config;

		public DefaultRateCalculator(DefaultRate config)
		{
			_config = config;
		}

		public decimal GetAmount(ParkingParameters parameters)
		{
			return (parameters.Days + (parameters.Hours == 24 ? 0 : 1)) * _config.Price;
		}

		public string GetRateName()
		{
			return _config.Name;
		}
	}
}