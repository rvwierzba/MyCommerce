using MyCommerce.Uteis;
using System.Data;

namespace MyCommerce.Models
{
    public class RelatorioModel
    {
        public DateTime DataDe { get; set; }
        public DateTime DataAte { get; set; }
    }

    public class GraficoProdutos
    {
        public int QtdeVendido { get; set; }
        public int CodigoProduto { get; set; }
        public String NomeProduto { get; set; }

        public List<GraficoProdutos> RetornarGrafico()
        {
            DAL dal = new DAL();
            string query = "SELECT SUM(qtde_produto) AS qtde p.nome AS produto " +
            "FROM itens_venda i INNER JOIN produto p ON i.produto_id = p.id GROUP BY p.nome";

            DataTable dt = dal.ReturnDataTable(query);
            List<GraficoProdutos> list = new List<GraficoProdutos>();
            GraficoProdutos item;

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GraficoProdutos();
                item.QtdeVendido = int.Parse(dt.Rows[i]["qtde"].ToString());
                item.NomeProduto = dt.Rows[i]["produto"].ToString();
                   
                list.Add(item);
            }
            return list;
        }
    }
}
