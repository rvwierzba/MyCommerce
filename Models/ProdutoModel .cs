using MyCommerce.Uteis;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyCommerce.Models
{
    public class ProdutoModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Produto")]
        public string Nome { get; set; }

      
        [Required(ErrorMessage = "Informe a Descrição do Produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o Preço unitário do Produto")]
        public decimal? Preco_Unitario { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade em Estoque do Produto")]
        public decimal? Quantidade_Estoque { get; set; }

        [Required(ErrorMessage = "Informe a Unidade de Medida do Produto")]
        public string Unidade_Medida { get; set; }

        [Required(ErrorMessage = "Informe o Link da Foto do Produto")]
        public string Link_Foto { get; set; }


        public List<ProdutoModel> ListarProdutos()
        {
            List<ProdutoModel> list = new List<ProdutoModel>();
            ProdutoModel item;
            DAL dal = new DAL();

            string query = "SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM produto ORDER BY nome ASC";
            DataTable dt = dal.ReturnDataTable(query);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Descricao = dt.Rows[i]["descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[i]["preco_unitario"].ToString()),
                    Quantidade_Estoque = decimal.Parse(dt.Rows[i]["quantidade_estoque"].ToString()),
                    Unidade_Medida = dt.Rows[i]["unidade_medida"].ToString(),
                    Link_Foto = dt.Rows[i]["link_foto"].ToString()

                };
                list.Add(item);
            }
            return list;
        }


        public ProdutoModel RetornarProduto(int? id)
        {           
            ProdutoModel item;
            DAL dal = new DAL();

            string query = $"SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM produto WHERE id = '{id}' ORDER BY nome ASC";
            DataTable dt = dal.ReturnDataTable(query);
                      
                item = new ProdutoModel
                {
                    Id = dt.Rows[0]["id"].ToString(),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Descricao = dt.Rows[0]["descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[0]["preco_unitario"].ToString()),
                    Quantidade_Estoque = decimal.Parse(dt.Rows[0]["quantidade_estoque"].ToString()),
                    Unidade_Medida = dt.Rows[0]["unidade_medida"].ToString(),
                    Link_Foto = dt.Rows[0]["link_foto"].ToString()

                };
                         
            return item;
        }


        //INSERT ou UPDATE
        public void Gravar()
        {
            DAL dal = new DAL();
            string query;

            if (Id != null)
            {
                query = $"UPDATE produto SET " +
                    $"nome = '{Nome}', descricao = '{Descricao}' " +
                    $"preco_unitario = {Preco_Unitario.ToString().Replace(",", ".")}, quantidade_estoque = '{Quantidade_Estoque}', " +
                    $"unidade_medidade = '{Unidade_Medida}', link_foto = '{Link_Foto}' WHERE id = '{Id}'";
            }
            else
            {
                query = $"INSERT INTO produto" +
                $"(nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto) " +
                $"VALUES('{Nome}', '{Descricao}', {Preco_Unitario.ToString().Replace(",", ".")}, '{Quantidade_Estoque}', '{Unidade_Medida}', '{Link_Foto}')";
            }
                       
            dal.CRUD(query);
        }

        //DELETE
        public void Deletar(int id)
        {
            DAL dal = new DAL();
            string query = $"DELETE FROM produto WHERE id = '{id}'";
            dal.CRUD(query);
        }
    }
}
