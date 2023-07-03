using MyCommerce.Uteis;
using System.Data;
using Newtonsoft.Json;

namespace MyCommerce.Models
{
    public class VendaModel
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Cliente_Id { get; set; }
        public string Vendedor_Id { get; set; }
        public double Total { get; set; }
        public string ListaProdutos { get; set; }

        public List<VendaModel> ListaVendas(String DataDe, string DataAte)
        {
            return RetornarListaVendas(DataDe, DataAte);
        }

        public List<VendaModel> ListaVendas()
        {
            return RetornarListaVendas("1900/01/01", "2200/01/01");
        }

        private List<VendaModel> RetornarListaVendas(string DataDe, String DataAte)
        {

            List<VendaModel> list = new List<VendaModel>();
            VendaModel item;
            DAL dal = new DAL();

            string query = "SELECT v1.id, v1.data, v1.total, v2.nome AS vendedor, c.nome AS cliente " +
            "FROM venda v1 INNER JOIN vendedor v2 ON v1.vendedor_id = v2.id INNER JOIN cliente c ON " +
            $"v1.cliente_id = c.id WHERE v1.data >= '{DataDe}' AND v1.data <= '{DataAte}' ORDER BY data, total";
            DataTable dt = dal.ReturnDataTable(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                    Total = double.Parse(dt.Rows[i]["total"].ToString()),
                    Cliente_Id = dt.Rows[i]["cliente"].ToString(),
                    Vendedor_Id = dt.Rows[i]["vendedor"].ToString()

                };
                list.Add(item);
            }
            return list;

        }
        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarClientes();
        }

        public List<VendedorModel> RetornarListaVendedores()
        {
            return new VendedorModel().ListarVendedores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarProdutos();
        }

        public void Inserir()
        {
            DAL dal = new DAL();
            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string query = "INSERT INTO venda(data, total, vendedor_id, cliente_id) " +
            $"VALUES('{dataVenda}', '{Total.ToString().Replace(",", ".")}', '{Vendedor_Id}', '{Cliente_Id}')";

            //Recuperar o ID da Venda
            string query_2 = $"SELECT id FROM venda " +
                $"WHERE data = '{dataVenda}' AND vendedor_id = '{Vendedor_Id}' AND cliente_id = '{Cliente_Id}' " +
                $"ORDER BY id DESC LIMIT 1";

            DataTable dt = dal.ReturnDataTable(query_2);
            string idVenda = dt.Rows[0]["id"].ToString();

            //Deserializar o JSON c os Produtos da Venda e Grava-los na Tabela ITENS_VENDA
            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);
            for(int i = 0; i < lista_produtos.Count; i++)
            {
                string query_3 = "INSERT INTO itens_venda(venda_id, produto_id, qtde_produto, preco_produto) " +
                 $"VALUES({idVenda}, {lista_produtos[i].CodProduto}, {lista_produtos[i].QtdProduto}, {lista_produtos[i].PrecoUnit.ToString().Replace(",", ".")})";

                dal.CRUD(query_3);

                //Baixa os Produtos Inseridos na Venda do Estoque
                string query_4 = $"UPDATE produto SET quantidade_estoque = (quantidade_estoque - {int.Parse(lista_produtos[i].QtdProduto)}) WHERE id = {lista_produtos[i].CodProduto}";
                dal.CRUD(query_4);
            }

           

        }
    }
}
