using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Web.Controllers
{
	public class RateController : BaseApiController
	{
		private readonly IRateService _rateService;

		public RateController(IRateService rateService)
		{
			_rateService = rateService;
		}

		[HttpGet("{entry:datetime}/{exit:datetime}")]
		public async Task<IActionResult> CalculateRate(DateTime entry, DateTime exit)
		{
			if (entry > exit) return BadRequest("Exit date must be before entry date");

			var parkingParameters = new ParkingParameters
			{
				Entry = entry,
				Exit = exit
			};

			var ticket = await _rateService.GetTicket(parkingParameters);

			return Ok(ticket);
		}
	}
}