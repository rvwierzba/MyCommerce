using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;

namespace MyCommerce.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult DeletarCliente(int id)
        {
            new ClienteModel().Deletar(id);
            return View();
        }

        public IActionResult Deletar(int id)
        {
            ViewData["idDelete"] = id;
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.ListaClientes = new ClienteModel().ListarClientes();
            return View();
        }

        
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if(id != null)
            {
                //Carregar o Registro do Cliente em uma ViewBag
                ViewBag.Cliente = new ClienteModel().RetornarCliente(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteModel cliente)
        {
            if (!ModelState.IsValid)
            {
                cliente.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
