using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoCategory> TodoCategories { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
