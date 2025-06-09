using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ProfileController(ILogger logger) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
