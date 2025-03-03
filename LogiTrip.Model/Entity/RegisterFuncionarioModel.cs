using LogiTrip.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace LogiTrip.Model.Entity
{
    public class RegisterFuncionarioModel
    {
        public int EstabelecimentoId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido.")]
        public string CPF_CNPJ { get; set; }

        [Required]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }

        public string NomeUsuario { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public Role NivelAcesso { get; set; }
        public Role Role { get; set; }
    }
}