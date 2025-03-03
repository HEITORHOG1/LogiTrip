using LogiTrip.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace LogiTrip.Model.Entity
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public string Imagem { get; set; }
        public bool Disponivel { get; set; }
        public int CategoriaId { get; set; }
        public int EstabelecimentoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int QuantidadeReservada { get; set; }
        public string? CodigoDeBarras { get; set; }
        public StatusProduto? Status { get; set; }
        public string NomeCategoria { get; set; }

        // Propriedade de navegação para a Categoria
        public Categoria Categoria { get; set; }

        // Coleções de Opções e Adicionais associados ao produto
        public virtual ICollection<OpcaoProduto> Opcoes { get; set; }
        public virtual ICollection<AdicionalProduto> Adicionais { get; set; }

        public Produto()
        {
            DataCadastro = DateTime.UtcNow;
            Disponivel = true;
            QuantidadeEmEstoque = 0;
            Status = StatusProduto.Ativo;

            // Inicializa as listas para evitar null reference
            Opcoes = new List<OpcaoProduto>();
            Adicionais = new List<AdicionalProduto>();
        }
    }
}
