using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Webidentity.Areas.Identity.Data;
using Webidentity.Models;

namespace Webidentity.Controllers
{
    [Authorize]
    
    public class HomeController : Controller
        
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            var user = _userManager.GetUserAsync(User).Result;
            return View(user);          
        }

        [HttpGet]
        public  async Task<IActionResult> EditProfile()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
        [HttpPost]
     
        public async Task<IActionResult> EditProfile(ApplicationUser update)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FistName = update.FistName;
                user.Lastname = update.Lastname;
                user.Email = update.Email;
                user.Location = update.Location;
                user.Phone = update.Phone;
                user.Bio = update.Bio;

                

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(update);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}