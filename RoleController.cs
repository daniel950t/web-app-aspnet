using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class RoleController : Controller
{
    // Add this action to the RoleController

    [Authorize]
    public IActionResult Roles()
    {
        return View();
    }

    
}