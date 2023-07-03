using MyCommerce.Uteis;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace MyCommerce.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage="Digite seu endereço de Email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Email Inválido, por favor Verifique!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite sua Senha!")]
        public string Senha { get; set; }

        public bool ValidarLogin()
        {
            string query = $"SELECT id, nome FROM vendedor WHERE email = '{Email}' AND senha = '{Senha}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText= query;
            DAL dal = new DAL();
            DataTable dt = dal.ReturnDataTableCMD(cmd);
            if(dt.Rows.Count == 1)
            {
                Id = dt.Rows[0]["id"].ToString();
                Nome = dt.Rows[0]["nome"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
