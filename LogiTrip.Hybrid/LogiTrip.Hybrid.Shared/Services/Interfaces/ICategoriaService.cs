using LogiTrip.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrip.Hybrid.Shared.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetCategoriasByEstabelecimentoAsync(int estabelecimentoId);
        Task<bool> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria> GetCategoriaByIdAsync(int estabelecimentoId, int id);
        Task<bool> UpdateCategoriaAsync(Categoria categoria);
    }
}
