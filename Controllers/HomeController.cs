using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;
using MyCommerce.Uteis;
using System.Diagnostics;

namespace MyCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Readme()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int? id)
        {
            //Realizar Logout
            if(id != null)
            {
                if(id == 0)
                {
                    HttpContext.Session.SetString("IdUserLogado", string.Empty);
                    HttpContext.Session.SetString("NomeUserLogado", string.Empty);
                }
            } //Fim

            return View(); 
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                bool loginOK = login.ValidarLogin();
                if (loginOK)
                {
                     HttpContext.Session.SetString("IdUserLogado", login.Id);
                     HttpContext.Session.SetString("NomeUserLogado", login.Nome);
                    
                   return RedirectToAction("Menu", "Home");
                }
                else
                {
                    TempData["LoginError"] = "Email ou Senha inválido(s)!";
                }
            }           
            return View();
        }

        public IActionResult Index()
        {           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}