using Microsoft.AspNetCore.Mvc;

namespace FullStackDemo.Front.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}