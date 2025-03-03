namespace LogiTrip.Model.Entity
{
    public class HorarioFuncionamentoRequest
    {
        public int EstabelecimentoId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public string HoraAbertura { get; set; }
        public string HoraFechamento { get; set; }
    }
}