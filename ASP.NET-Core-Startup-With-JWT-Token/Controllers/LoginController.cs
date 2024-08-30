using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("/")]
public class LoginController : Controller
{
    private readonly string secretKey = "YourVeryLongAndSecureSecretKeyThatIsAtLeast32Characters"; // Same key with Program.cs

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

    #region Login

    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [HttpPost("/login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "testuser" && request.Password == "password")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        return Unauthorized();
    }

    #endregion

    [HttpGet("/register")]
    public IActionResult GetRegisterToken()
    {
        return View("Register");
    }

    [HttpPost("/register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { success = false, message = "Username or password cannot be empty." });
        }
        bool registrationSuccessful = true;

        if (registrationSuccessful)
        {
            return Ok(new { success = true, message = "Registration successful!" });
        }
        else
        {
            return BadRequest(new { success = false, message = "Registration failed: Invalid data" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
