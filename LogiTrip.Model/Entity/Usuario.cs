using LogiTrip.Model.Enums;

namespace LogiTrip.Model.Entity
{
    public class Usuario
    {
        public string Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string? CPF_CNPJ { get; set; }
        public string? Telefone { get; set; }
        public bool Ativo { get; set; }
        public string Role { get; set; }
        public Role NivelAcesso { get; set; }
    }
}