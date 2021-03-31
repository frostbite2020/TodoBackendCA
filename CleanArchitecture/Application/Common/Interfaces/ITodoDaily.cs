using Application.Common.Models;
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
        Task<int> Add(TodoDaily todoDaily, CancellationToken cancellationToken);
        Task<bool> Update(int todoDailyId, CancellationToken cancellationToken);
        Task<bool> UnceklistHistory(int todoDailyHistoryId, CancellationToken cancellationToken);
        Task<int> Delete(int todoDailyId, CancellationToken cancellationToken);
        Task<int> DeleteHistory(int todoDailyHistoryId, CancellationToken cancellationToken);
        Task<TodoDailyHistoryVm> GetTodoDailyHistory(int todoDailyId, int pageNumber, int pageSize);
    }
}