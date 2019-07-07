using System.Threading.Tasks;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Interfaces
{
	/// <summary>
	/// Service responsible for the ticket details
	/// </summary>
	public interface IRateService
	{
		/// <summary>
		/// Gets the ticket details given parking parameters 
		/// </summary>
		/// <param name="parkingParameters">entry and exit for parking</param>
		/// <returns>Amount and name to display</returns>
		Task<Ticket> GetTicket(ParkingParameters parkingParameters);
	}
}