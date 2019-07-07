using ParkingRate.Core.Models;

namespace ParkingRate.Core.Configuration
{
	public class FlatRate : DefaultRate
	{
		public Range EntryTimeRange { get; set; }

		public Range ExitTimeRange { get; set; }

		public int[] EntryDaysRange { get; set; }

		public int[] ExitDaysRange { get; set; }

		public int MaxDays { get; set; }
	}
}