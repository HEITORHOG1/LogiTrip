using LogiTrip.Model.Entity;

namespace LogiTrip.Hybrid.Shared.Services.Interfaces
{
    public interface IOpcaoProdutoService
    {
        // Métodos já existentes
        Task<List<OpcaoProduto>> GetOpcoesByProdutoAsync(int estabelecimentoId, int produtoId);
        Task<OpcaoProduto> GetOpcaoProdutoByIdAsync(int estabelecimentoId, int produtoId, int opcaoId);
        Task<bool> CreateOpcaoProdutoAsync(int estabelecimentoId, int produtoId, OpcaoProduto opcao);
        Task<bool> UpdateOpcaoProdutoAsync(int estabelecimentoId, int produtoId, OpcaoProduto opcao);
        Task<bool> DeleteOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId);

        // Métodos para ValorOpcaoProduto a serem implementados
        Task<List<ValorOpcaoProduto>> GetValoresByOpcaoAsync(int estabelecimentoId, int produtoId, int opcaoId);
        Task<bool> CreateValorOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId, ValorOpcaoProduto valor);
        Task<bool> UpdateValorOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId, ValorOpcaoProduto valor);
        Task<bool> DeleteValorOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId, int valorId);
        Task<bool> ReplicarOpcoesParaCategoriaAsync(int categoriaId, int produtoOrigemId);

    }
}