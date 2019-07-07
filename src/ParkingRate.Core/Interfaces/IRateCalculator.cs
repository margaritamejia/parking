using ParkingRate.Core.Models;

namespace ParkingRate.Core.Interfaces
{
	/// <summary>
	/// Responsible for returning the amount and rate name.
	/// </summary>
	public interface IRateCalculator
	{
		/// <summary>
		/// Gets the correspondent amount
		/// </summary>
		/// <param name="parameters">Entry and exit for parking</param>
		/// <returns>Amount to charge</returns>
		decimal GetAmount(ParkingParameters parameters);

		/// <summary>
		/// Get the correspondent name
		/// </summary>
		/// <returns>Rate name to display</returns>
		string GetRateName();
	}
}