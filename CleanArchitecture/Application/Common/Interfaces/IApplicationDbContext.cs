using Application.Common.Models.UserModels;
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
        DbSet<UserProperty> UserProps { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
