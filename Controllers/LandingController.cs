using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace card_game_web_app.Controllers
{
	public class LandingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}