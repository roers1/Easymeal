using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CustomerModels;
using EasyMealCore.DomainModel.Invoice;
using EasyMealCore.DomainServices;
using EasyMealCustomerGUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyMealCustomerGUI.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IUserContext _users;
		private readonly IOrderRepository _context;
		public AccountController(IUserContext users, UserManager<User> userMgr, SignInManager<User> signInMgr, IOrderRepository ctx)
		{
			_users = users;
			_userManager = userMgr;
			_signInManager = signInMgr;
			_context = ctx;
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

				if (user != null && _users.Customer.Any(a => a.Id == user.Id))
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
					Address = model.Address,
					DateOfBirth = model.DateOfBirth,
					PhoneNumber = model.PhoneNumber,
					Glutes = model.glutes,
					Diabetes = model.diabetes,
					Salt = model.salt,
					Customer = new Customer { }
				};
			}
			else
			{
				TempData["message"] = $"The user {user.Email} already exists";

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

		public IActionResult Details()
		{
			var user = GetUser();

			RegisterModel account = new RegisterModel
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				diabetes = user.Diabetes,
				glutes = user.Glutes,
				salt = user.Salt
			};
			return View(account);
		}

		private User GetUser()
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			if (userId == null)
			{
				return null;
			}
			return _userManager.FindByIdAsync(userId).Result;
		}
		public IActionResult Invoice()
		{
			var user = GetUser();

			var invoices = _context.GetOrder(user);
			return View(invoices);
		}

		public IActionResult InvoiceDetails(int invoiceId)
		{
			var user = GetUser();
			var invoices = _context.GetOrder(user).Include(i => i.InvoiceLines);
			var invoice = invoices.First(a => a.InvoiceId == invoiceId);

			return View(invoice);
		}

		[HttpPost]
		public async Task<ActionResult> Details(RegisterModel model)
		{

			var userId = _userManager.GetUserId(HttpContext.User);
			if (userId == null)
			{
				TempData["msg"] = "Interne fout, log opnieuw in";
				return RedirectToAction("Login");
			}
			if (model.Password != null)
			{
				await ChangePassword(model.Password);
			};

			User user = _userManager.FindByIdAsync(userId).Result;

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Email = model.Email;
			user.PhoneNumber = model.PhoneNumber;
			user.Diabetes = model.diabetes;
			user.Glutes = model.glutes;
			user.Salt = model.salt;

			await _userManager.UpdateAsync(user);

			TempData["msg"] = "Uw profiel is aangepast";
			return RedirectToAction("Details");
		}

		public async Task ChangePassword(String newPassword)
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			User user = _userManager.FindByIdAsync(userId).Result;

			await _userManager.RemovePasswordAsync(user);
			await _userManager.AddPasswordAsync(user, newPassword);

		}

		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			HttpContext.Session.Clear();
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}