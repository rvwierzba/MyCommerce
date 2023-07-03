using MyCommerce.Uteis;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyCommerce.Models
{
    public class VendedorModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Vendedor")]
        public string Nome { get; set; }
              
        [Required(ErrorMessage = "Informe o Email do Vendedor")]
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<VendedorModel> ListarVendedores()
        {
            List<VendedorModel> list = new List<VendedorModel>();
            VendedorModel item;
            DAL dal = new DAL();

            string query = "SELECT id, nome, email, senha FROM vendedor ORDER BY nome ASC";
            DataTable dt = dal.ReturnDataTable(query);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendedorModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Email = dt.Rows[i]["email"].ToString(),
                    Senha = dt.Rows[i]["senha"].ToString()
                };
                list.Add(item);
            }
            return list;
        }


        public VendedorModel RetornarVendedor(int? id)
        {           
            VendedorModel item;
            DAL dal = new DAL();

            string query = $"SELECT id, nome, email, senha FROM vendedor WHERE id = '{id}' ORDER BY nome ASC";
            DataTable dt = dal.ReturnDataTable(query);
                      
                item = new VendedorModel
                {
                    Id = dt.Rows[0]["id"].ToString(),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Email = dt.Rows[0]["email"].ToString(),
                    Senha = dt.Rows[0]["senha"].ToString()
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
                query = $"UPDATE vendedor SET nome = '{Nome}', email = '{Email}' WHERE id = '{Id}'";
            }
            else
            {
                query = $"INSERT INTO vendedor(nome, email, senha) VALUES('{Nome}', '{Email}', '123456')";
            }
                       
            dal.CRUD(query);
        }

        //DELETE
        public void Deletar(int id)
        {
            DAL dal = new DAL();
            string query = $"DELETE FROM vendedor WHERE id = '{id}'";
            dal.CRUD(query);
        }
    }
}
