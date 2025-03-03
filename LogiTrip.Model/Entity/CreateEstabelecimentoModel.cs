using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace LogiTrip.Model.Entity
{
    public class CreateEstabelecimentoModel
    {
        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string CNPJ { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        public string Numero { get; set; }

        public bool Status { get; set; }

        public decimal TaxaEntregaFixa { get; set; }

        public string Descricao { get; set; }

        public string? UrlImagem { get; set; }
    }
}