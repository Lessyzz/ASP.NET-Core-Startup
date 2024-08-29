using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var responseObject = new
        {
            Message = "Json Message",
            Status = "Success",
            MadeBy = "Lessy"
        };
        return new JsonResult(responseObject); // Json Response
    }

    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View("Login"); // Html Page Response
    }

    [HttpGet("/register")]
    public IActionResult Register()
    {
        return View("Register"); // Html Page Response
    }
}
