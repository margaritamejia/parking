using Microsoft.AspNetCore.Mvc;

namespace ParkingRate.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class BaseApiController : Controller
	{
	}
}