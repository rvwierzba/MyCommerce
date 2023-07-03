using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;

namespace MyCommerce.Controllers
{
    public class VendedorController : Controller
    {
        public IActionResult DeletarVendedor(int id)
        {
            new VendedorModel().Deletar(id);
            return View();
        }

        public IActionResult Deletar(int id)
        {
            ViewData["idDelete"] = id;
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.ListaVendedores = new VendedorModel().ListarVendedores();
            return View();
        }

        
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if(id != null)
            {
                //Carregar o Registro do Vendedor em uma ViewBag
                ViewBag.Vendedor = new VendedorModel().RetornarVendedor(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(VendedorModel vendedor)
        {
            if (!ModelState.IsValid)
            {
                vendedor.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
