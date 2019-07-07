using System.Collections.Generic;

namespace ParkingRate.Core.Configuration
{
	public class RatesConfiguration
	{
		public List<FlatRate> FlatRates { get; set; }

		public HourlyRate HourlyRate { get; set; }

		public DefaultRate DefaultRate { get; set; }
	}
}