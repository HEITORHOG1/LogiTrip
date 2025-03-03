using LogiTrip.Model.Entity;
using LogiTrip.Model.Paginacao;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace LogiTrip.Hybrid.Shared.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetProdutosByEstabelecimentoAsync(int estabelecimentoId, string search = null);
        Task<Produto> GetProdutoByIdAsync(int estabelecimentoId, int id);
        Task<bool> CreateProdutoAsync(int estabelecimentoId, Produto produto, IBrowserFile imagem);
        Task<bool> UpdateProdutoAsync(int estabelecimentoId, Produto produto, IBrowserFile imagem = null);
        Task<bool> DeleteProdutoAsync(int estabelecimentoId, int id);
    }
}