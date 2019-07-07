using System;

namespace ParkingRate.Core.Models
{
	public class ParkingParameters
	{
		public DateTime Entry { get; set; }

		public DateTime Exit { get; set; }

		private TimeSpan Duration => Exit.Subtract(Entry);

		public int Hours => (int) Math.Ceiling(Duration.TotalHours);

		public int Days => (int) Duration.TotalDays;
	}
}