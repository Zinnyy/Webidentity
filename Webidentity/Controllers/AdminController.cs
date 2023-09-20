using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webidentity.Areas.Identity.Data;

namespace Webidentity.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var getUser = _userManager.Users.ToList();
            var nonAdminUsers = getUser.Where(u => !_userManager.IsInRoleAsync(u, "Admin").Result).ToList();
            return View(getUser);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var getUser = await _userManager.FindByIdAsync(id);
            if(getUser == null)
            {
                return NotFound();
            }
            return View(getUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser update)
        {
            var getUser = await _userManager.GetUserAsync(User);

            if(getUser != null)
            {
                getUser.FistName = update.FistName;
                getUser.Lastname = update.Lastname;
                getUser.Location = update.Location;
                getUser.Bio = update.Bio;
                getUser.Phone = update.Phone;
                getUser.Email = update.Email;

              var result =  await _userManager.UpdateAsync(getUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(update);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var getUser = await _userManager.FindByIdAsync(id);
            if(getUser == null)
            {
                return NotFound();
            }
            return View(getUser);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser appUser)
        {
           
            var userTask = await _userManager.FindByIdAsync(appUser.Id);
            if (userTask != null)
            {
                userTask.IsActivated = true;
                await _userManager.UpdateAsync(userTask);
            }
            return RedirectToAction("Index");

            //var lockDateTask = _userManager.SetLockoutEndDateAsync(user, endDate);
            //lockDateTask.Wait();

            //return lockUserTask.Result.Succeeded;
            //var user = userTask.Result;
            //var lockUserTask =  _userManager.SetLockoutEnabledAsync(user, true);
            //lockUserTask.Wait();

           
        }

        [HttpGet]

        public async Task<IActionResult> Activate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(ApplicationUser userId)
        {
            var user = await _userManager.FindByIdAsync(userId.Id);
            if (user != null)
            {
                user.IsActivated = false;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }







        //public async Task<IActionResult> Delete(ApplicationUser deactivate)
        //{
        //    var getUsers = await _userManager.GetUserAsync(User);
        //    if (getUsers == null)
        //    {
        //        return NotFound();
        //    }
        //    getUsers.IsDeactivated = true;

        //    var result = await _userManager.UpdateAsync(getUsers);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Index", "Admin");
        //    }
        //    return View(deactivate);
        //}
    }
}
