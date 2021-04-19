using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.TodoDailys.Commands.UpdateTodoDaily;
using Application.TodoDailys.Queries.GetAllTodoDailys;
using Application.TodoDailys.Queries.GetTodoDailyHistories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TodoDailyServices : ITodoDaily
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;
        private const int DefaultDateDueDays = 1;
        public TodoDailyServices(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<TodoDailyDto>> Get(int userPropertyId)
        {
            var todoDaily = await _context.TodoDailys
                .Where(x => x.UserProperty.Id == userPropertyId)
                .ProjectTo<TodoDailyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return todoDaily;
        }

        public async Task<TodoDailyHistoryVm> GetTodoDailyHistory(int userPropertyId, int pageNumber, int pageSize)
        {
            return new TodoDailyHistoryVm
            {
                TodoDailyHistories = await _context.TodoDailyHistories
                    .Where(x => x.UserPropertyId == userPropertyId)
                    .ProjectTo<TodoDailyHistoryDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(pageNumber, pageSize)
            };
        }

        public async Task<int> Add(TodoDaily todoDaily, CancellationToken cancellationToken)
        {
            _context.TodoDailys.Add(todoDaily);
            await _context.SaveChangesAsync(cancellationToken);
            return todoDaily.Id;
        }

        public async Task<bool> Update(UpdateTodoDailyDto todoDaily, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            var todoDailyAsset = await _context.TodoDailys.FindAsync(todoDaily.Id);

            //check if it is null then return Exception
            if (todoDailyAsset == null)
                throw new NotFoundException("Todo Daily Not Found");

            //Update with a new Data
            todoDailyAsset.MadeUntil = now;
            todoDailyAsset.Check = todoDaily.CheckStatus;
            _context.TodoDailys.Update(todoDailyAsset);
            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }

        public async Task<int> Delete(int todoDailyId, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var assetTodoDaily = await _context.TodoDailys.FindAsync(todoDailyId);

            if (assetTodoDaily == null)
                throw new NotFoundException();

            var todoHistory = new TodoDailyHistory
            {
                UserPropertyId = assetTodoDaily.UserPropertyId,
                TodoDailyActivity = assetTodoDaily.TodoDailyActivity,
                MadeSince = assetTodoDaily.MadeSince,
                CheckDate = now,
                CheckStatus = assetTodoDaily.Check
            };
            _context.TodoDailyHistories.Add(todoHistory);

            _context.TodoDailys.Remove(assetTodoDaily);
            await _context.SaveChangesAsync(cancellationToken);

            return assetTodoDaily.Id;
        }

        public async Task<int> DeleteHistory(int todoDailyHistoryId, CancellationToken cancellationToken)
        {
            var assetTodoDailyHistory = await _context.TodoDailyHistories.FindAsync(todoDailyHistoryId);

            if (assetTodoDailyHistory == null)
                throw new NotFoundException();

            _context.TodoDailyHistories.Remove(assetTodoDailyHistory);
            await _context.SaveChangesAsync(cancellationToken);

            return assetTodoDailyHistory.Id;
        }

        private static DateTime GetDefaultDateDue(DateTime now) => now.AddDays(DefaultDateDueDays);

    }
}
