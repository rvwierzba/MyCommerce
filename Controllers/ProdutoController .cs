using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;

namespace MyCommerce.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult DeletarProduto(int id)
        {
            new ProdutoModel().Deletar(id);
            return View();
        }

        public IActionResult Deletar(int id)
        {
            ViewData["idDelete"] = id;
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.ListaProdutos = new ProdutoModel().ListarProdutos();
            return View();
        }

        
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if(id != null)
            {
                //Carregar o Registro do Produto em uma ViewBag
                ViewBag.Produto = new ProdutoModel().RetornarProduto(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoModel produto)
        {
            if (!ModelState.IsValid)
            {
                produto.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
