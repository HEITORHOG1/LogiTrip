using LogiTrip.Model.Entity;

namespace LogiTrip.Hybrid.Shared.Services.Interfaces
{
    public interface IFornecedorService
    {
        Task<IEnumerable<Fornecedor>> GetFornecedoresByEstabelecimentoAsync(int estabelecimentoId);
        Task<bool> CreateFornecedorAsync(Fornecedor fornecedor);

        Task<bool> UpdateFornecedorAsync(Fornecedor fornecedor);
        Task<Fornecedor> GetFornecedorByIdAsync(int id);
    }
}