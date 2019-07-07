using System;
using System.Linq;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class FlatRateCondition : IRateCondition
	{
		private readonly FlatRate _rateConditions;

		public FlatRateCondition(FlatRate rateConditions)
		{
			_rateConditions = rateConditions;
		}

		public bool IsValid(ParkingParameters parkingParameters)
		{
			if (parkingParameters.Days > _rateConditions.MaxDays) return false;

			var currentEntryTime = parkingParameters.Entry.TimeOfDay;
			var currentExitTime = parkingParameters.Exit.TimeOfDay;

			var currentEntryDayOfWeek = parkingParameters.Entry.DayOfWeek;
			var currentExitDayOfWeek = parkingParameters.Exit.DayOfWeek;

			return IsBetweenValidRange(currentEntryTime, _rateConditions.EntryTimeRange) &&
			       IsBetweenValidRange(currentExitTime, _rateConditions.ExitTimeRange) &&
			       _rateConditions.EntryDaysRange.Contains((int) currentEntryDayOfWeek) &&
			       _rateConditions.ExitDaysRange.Contains((int) currentExitDayOfWeek);
		}

		private static bool IsBetweenValidRange(TimeSpan currentTime, Range validRange)
		{
			return (currentTime.Equals(TimeSpan.Zero) || validRange.Start <= currentTime)
			       && (validRange.End.Equals(TimeSpan.Zero) || currentTime <= validRange.End);
		}
	}
}