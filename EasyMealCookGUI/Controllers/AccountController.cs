using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyMealCookGUI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainServices;

namespace EasyMealCookGUI.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private IUserContext _users;
		public AccountController(IUserContext users, UserManager<User> userMgr, SignInManager<User> signInMgr)
		{
			_users = users;
			_userManager = userMgr;
			_signInManager = signInMgr;
		}

		[AllowAnonymous]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return Redirect("/");
			}
			else
			{
				return View(new LoginModel());
			}

		}

		[AllowAnonymous]
		public ViewResult Register()
		{
			return View(new RegisterModel());
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			if (ModelState.IsValid)
			{
				User user = await _userManager.FindByNameAsync(loginModel.Name);

				if (user != null && _users.Cook.Any(a => a.Id == user.Id))
				{
					await _signInManager.SignOutAsync();
					if ((await _signInManager.PasswordSignInAsync(user,
						loginModel.Password, false, false)).Succeeded)
					{
						return Redirect(loginModel?.ReturnUrl ?? "/");
					}
				}
				else
				{
					ModelState.AddModelError("", "Invalid name or password");
					return View(loginModel);
				}
			}
			ModelState.AddModelError("", "Invalid name or password");
			return View(loginModel);
		}

		[AllowAnonymous]
		public async Task<IActionResult> RegisterUser(RegisterModel model)
		{
			User user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				user = new User
				{
					UserName = model.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					PhoneNumber = model.PhoneNumber,
					Cook = new Cook {  }
				};
			}
			else
			{
				TempData["message"] = $"This {user.Email} already exists";

				return RedirectToAction("Register");
			}

			var d = await _userManager.CreateAsync(user, model.Password);

			if (d == IdentityResult.Success)
			{
				return Redirect(model?.ReturnUrl ?? "/");
			}
			else
			{
				return BadRequest(d);
			}
		}
		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			HttpContext.Session.Clear();
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}