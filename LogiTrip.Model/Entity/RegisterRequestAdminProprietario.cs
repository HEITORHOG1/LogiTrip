using LogiTrip.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace LogiTrip.Model.Entity
{
    public class RegisterRequestAdminProprietario
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string NomeUsuario { get; set; }

        [Required]
        public string CPF_CNPJ { get; set; }

        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}