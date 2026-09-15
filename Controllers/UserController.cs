using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.Services;

namespace MvcProject.Controllers;

public class UserController(IAdminService _adminService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {

        var users = await _adminService.GetAllUsersAsync(cancellationToken);
        ViewBag.Users = users;

        return View();
    }
}
