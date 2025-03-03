using System.Text.Json.Serialization;

namespace LogiTrip.Model.Entity
{
    public class HorarioFuncionamento
    {
        public int Id { get; set; }
        public int EstabelecimentoId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraFechamento { get; set; }
    }
}