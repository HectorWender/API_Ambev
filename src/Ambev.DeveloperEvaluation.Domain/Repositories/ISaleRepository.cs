using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}