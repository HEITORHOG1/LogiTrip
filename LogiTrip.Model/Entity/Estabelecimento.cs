using Microsoft.AspNetCore.Http;

namespace LogiTrip.Model.Entity
{
    public class Estabelecimento
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string CEP { get; set; } // Novo Campo
        public string Endereco { get; set; }
        public string Numero { get; set; } // Novo Campo
        public bool Status { get; set; }
        public decimal taxaEntregaFixa { get; set; }
        public string? UrlImagem { get; set; }
        public string? Descricao { get; set; }
        public string? UsuarioId { get; set; } // Novo Campo

    }
}