using MyCommerce.Uteis;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyCommerce.Models
{
    public class ClienteModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CPF ou CNPJ do Cliente")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "Informe o Email do Cliente")]
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<ClienteModel> ListarClientes()
        {
            List<ClienteModel> list = new List<ClienteModel>();
            ClienteModel item;
            DAL dal = new DAL();

            string query = "SELECT id, nome, cpf_cnpj, email, senha FROM cliente ORDER BY nome ASC";
            DataTable dt = dal.ReturnDataTable(query);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ClienteModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    CPF_CNPJ = dt.Rows[i]["cpf_cnpj"].ToString(),
                    Email = dt.Rows[i]["email"].ToString(),
                    Senha = dt.Rows[i]["senha"].ToString()
                };
                list.Add(item);
            }
            return list;
        }


        public ClienteModel RetornarCliente(int? id)
        {           
            ClienteModel item;
            DAL dal = new DAL();

            string query = $"SELECT id, nome, cpf_cnpj, email, senha FROM cliente WHERE id = '{id}' ORDER BY nome ASC";
            DataTable dt = dal.ReturnDataTable(query);
                      
                item = new ClienteModel
                {
                    Id = dt.Rows[0]["id"].ToString(),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    CPF_CNPJ = dt.Rows[0]["cpf_cnpj"].ToString(),
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
                query = $"UPDATE cliente SET nome = '{Nome}', cpf_cnpj = '{CPF_CNPJ}', email = '{Email}' WHERE id = '{Id}'";
            }
            else
            {
                query = $"INSERT INTO cliente(nome, cpf_cnpj, email, senha) VALUES('{Nome}', '{CPF_CNPJ}', '{Email}', '123456')";
            }
                       
            dal.CRUD(query);
        }

        //DELETE
        public void Deletar(int id)
        {
            DAL dal = new DAL();
            string query = $"DELETE FROM cliente WHERE id = '{id}'";
            dal.CRUD(query);
        }
    }
}
