using Application.TodoDailys.Queries.GetAllTodoDailys;
using Application.TodoDailys.Queries.GetTodoDailyHistories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITodoDaily
    {
        Task<IList<TodoDailyDto>> Get(int userPropertyId);
        Task<TodoDailyDto> Add(TodoDaily todoDaily, CancellationToken cancellationToken);
        Task<bool> Update(int todoDailyId, CancellationToken cancellationToken);
        Task<bool> UnceklistHistory(int todoDailyHistoryId, CancellationToken cancellationToken);
        Task<bool> RemoveIfTrue(int todoDailyId);
        Task<bool> IsChecked(int todoDailyId);
        Task<int> Delete(int todoDailyId, CancellationToken cancellationToken);
        Task<IList<TodoDailyHistoryDto>> GetTodoDailyHistory(int todoDailyId, CancellationToken cancellationToken);
    }
}