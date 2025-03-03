namespace LogiTrip.Model.Entity
{
    public class ValorOpcaoProduto
    {
        public int Id { get; set; }
        public int OpcaoProdutoId { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoAdicional { get; set; }
    }
}