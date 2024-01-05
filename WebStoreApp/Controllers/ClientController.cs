using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebShopApp.Infrastructure.Data.Domain;

using WebStoreApp.Models.Client;

namespace WebStoreApp.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class ClientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ClientController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allUser = this._userManager.Users
                .Select(u => new ClientIndexVM
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Email = u.Email,
                }).ToList();

            var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator"))
                .Select(a=> a.Id).ToList();

            foreach (var user in allUser)
            {
                user.IsAdmin = adminIds.Contains(user.Id);
            }

            var users = allUser.Where(x=> x.IsAdmin == false)
                .OrderBy(x=>x.UserName).ToList();

            return this.View(users);
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(string id)
        {
            var user = this._userManager.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            ClientDeleteVM userToDelete = new ClientDeleteVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                UserName = user.UserName,
            };
            return View(userToDelete);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ClientDeleteVM bidingModel)
        {
            string id = bidingModel.Id;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Success");
            }
            return NotFound();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
