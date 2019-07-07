using System.Threading.Tasks;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class RateService : IRateService
	{
		private readonly IRateCalculatorFactory _rateCalculatorFactory;

		public RateService(IRateCalculatorFactory rateCalculatorFactory)
		{
			_rateCalculatorFactory = rateCalculatorFactory;
		}

		public async Task<Ticket> GetTicket(ParkingParameters parkingParameters)
		{
			var calculator = _rateCalculatorFactory.GetRateCalculator(parkingParameters);

			var ticket = new Ticket
			{
				Amount = calculator.GetAmount(parkingParameters),
				RateName = calculator.GetRateName()
			};

			return ticket;
		}
	}
}