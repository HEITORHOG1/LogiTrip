using LogiTrip.Model.Entity;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Text.Json;

namespace LogiTrip.Hybrid.Shared.Services.Interfaces
{
    public interface IEstabelecimentoService
    {
        Task<IEnumerable<Estabelecimento>> GetEstabelecimentosByProprietarioAsync(string usuarioId);

        Task<IEnumerable<Usuario>> GetUsuariosByEstabelecimentoAsync(int estabelecimentoId);

        Task<bool> CreateEstabelecimentoAsync(CreateEstabelecimentoModel estabelecimento,IBrowserFile imagem);

        Task<Estabelecimento> GetEstabelecimentoByIdAsync(int id);

        Task<bool> UpdateEstabelecimentoAsync(Estabelecimento estabelecimento, IBrowserFile imagem);

        Task<EnderecoDto> BuscarEnderecoPorCepAsync(string cep);

        Task<IEnumerable<HorarioFuncionamento>> GetHorariosByEstabelecimentoIdAsync(int estabelecimentoId);

        Task<bool> CreateHorarioFuncionamentoAsync(HorarioFuncionamentoRequest horario);

        Task<bool> UpdateHorarioFuncionamentoAsync(HorarioFuncionamento horario);

        Task<bool> DeleteHorarioFuncionamentoAsync(int id);
    }
}