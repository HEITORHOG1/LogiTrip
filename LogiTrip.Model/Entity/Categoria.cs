namespace LogiTrip.Model.Entity
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int EstabelecimentoId { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}