using Microsoft.AspNetCore.Mvc;
using MyCommerce.Models;

namespace MyCommerce.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasPeriodo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VendasPeriodo(RelatorioModel relatorio)
        {
            if(relatorio.DataDe.Year == 1)
            {
                ViewBag.ListaVendasPeriodo = new VendaModel().ListaVendas();
            }
            else
            {
                string DataDe = relatorio.DataDe.ToString("yyyy/MM/dd");
                string DataAte = relatorio.DataAte.ToString("yyyy/MM/dd");

                ViewBag.ListaVendasPeriodo = new VendaModel().ListaVendas(DataDe, DataAte);
            }
           
            return View();
        }

        public IActionResult Grafico()
        {
            List<GraficoProdutos> list = new GraficoProdutos().RetornarGrafico();
            string valores = "";
            string labels = "";
            string cores = "";
            var random = new Random();

            //Percorrer a Lista de Itens para Compor o Grafico
            for(int i = 0; i < list.Count; i++)
            {
                valores += $"{list[i].QtdeVendido.ToString()},";
                labels += $"'{list[i].NomeProduto.ToString()}',";

                //Escolher aleatoreamente as Cores do Grafico
                cores = String.Format("#{0:X6}", random.Next(0x1000000));
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }

        public IActionResult Comissao()
        {
            return View();
        }
    }
}
