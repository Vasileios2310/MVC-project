using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebAppMVCDBFirst.DTO;
using WebAppMVCDBFirst.Services;

namespace WebAppMVCDBFirst.Controllers;

public class UserController : Controller
{
    private readonly IApplicationService _applicationService;

    public UserController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        ClaimsPrincipal principal = HttpContext.User;
        if (!principal.Identity!.IsAuthenticated)
        {
            return View();
        }
        return RedirectToAction("Index" , "User");  // Dashboard
    }

    [HttpPost]
    public async Task<ActionResult> Login(UserLoginDTO credentials)
    {
        var user = await _applicationService.UserService.VerifyAndGetUserAsync(credentials);
        if (user == null)
        {
            ViewData["ValidateMessage"] = "Invalid credentials - Username or password is incorrect.";
            return View();
        }
        // make a list with claims (role)
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, credentials.Username!),
            new Claim(ClaimTypes.Role, user.UserRole.ToString()!)
        };
        // Claims describe who the user is and what roles/permissions they have.
        // Cookies are used to store that identity on the client side, so the user stays logged in across requests
        ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties properties = new()
        {
            AllowRefresh = true,
            IsPersistent = false
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
        
        return RedirectToAction("Index" , "User");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login" , "User");
    }
}