using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyMealCookGUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			if (User.Identity.IsAuthenticated)
			{
				return View();
			}
			{
				return Redirect("/account/login");
			}
        }
    }
}