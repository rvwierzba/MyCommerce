using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;

namespace MyCommerce.Controllers
{
    public class VendaController : Controller
    {
        private IHttpContextAccessor httpContext;

        //Recebe o Contexto do HTTP via Injeção de Denpendencia
        public VendaController(IHttpContextAccessor HttpContextAccessor)
        {
            httpContext = HttpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewBag.ListaVendas = new VendaModel().ListaVendas();
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            //Captura o ID do Vendedor Logado no Sistema
            venda.Vendedor_Id = httpContext.HttpContext.Session.GetString("IdUserLogado");
            venda.Inserir();
            CarregarDados();
            return View();
        }

        private void CarregarDados()
        {
            ViewBag.ListaClientes = new VendaModel().RetornarListaClientes();
            ViewBag.ListaVendedores = new VendaModel().RetornarListaVendedores();
            ViewBag.ListaProdutos = new VendaModel().RetornarListaProdutos();
        }
    }
}
