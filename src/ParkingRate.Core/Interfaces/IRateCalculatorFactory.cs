using ParkingRate.Core.Models;

namespace ParkingRate.Core.Interfaces
{
	/// <summary>
	/// Responsible for configuring the possible rates
	/// </summary>
	public interface IRateCalculatorFactory
	{
		/// <summary>
		/// Get an instance of the calculator from the factory
		/// </summary>
		/// <returns></returns>
		IRateCalculator GetRateCalculator(ParkingParameters parameters);
	}
}