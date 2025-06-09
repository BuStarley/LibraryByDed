using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PublishController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
