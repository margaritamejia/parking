using ParkingRate.Core.Models;

namespace ParkingRate.Core.Interfaces
{
	/// <summary>
	///  Responsible for evaluating if this is applicable.
	/// </summary>
	public interface IRateCondition
	{
		/// <summary>
		/// Check if this condition is valid for the parking parameters 
		/// </summary>
		/// <param name="parkingParameters">Entry and exit for parking</param>
		/// <returns></returns>
		bool IsValid(ParkingParameters parkingParameters);
	}
}