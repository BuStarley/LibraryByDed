using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebApi.Controllers;

public class ProfileController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
}
