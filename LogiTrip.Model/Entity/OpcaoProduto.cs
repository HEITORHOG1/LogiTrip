namespace LogiTrip.Model.Entity
{
    public class OpcaoProduto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Obrigatorio { get; set; }

        public virtual Produto Produto { get; set; } // Certifique-se de ter esta propriedade
        public virtual ICollection<ValorOpcaoProduto> Valores { get; set; }

        public OpcaoProduto()
        {
            Valores = new List<ValorOpcaoProduto>();
        }
    }

}