using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using taxfrauders.Models;
using System.Xml.Linq;

namespace taxfrauders.Controllers
{
	public class LoginController : Controller
	{
		private readonly TaxfraudersDbContext _context;

		public LoginController(TaxfraudersDbContext context)
		{
			_context = context;
		}

		// GET: Login
		public async Task<IActionResult> Index()
		{
			if(User.Identity.IsAuthenticated)
			{
				return Redirect("/");
			}
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index");
		}




		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(string Username, string Password)
		{
			var user = _context.User.First(u => u.Username == Username);
			if (user == null)
			{
				return RedirectToAction("Index");
			}
			if (user.Password == Password)
			{
				var claims = new List<Claim>{
				new Claim(ClaimTypes.Name, Username)
			};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					AllowRefresh = true,
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
					IsPersistent = true
				};
				_ = HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);
			}
			return Redirect("Index");
		}

	}
}
