using Application.Common.Exceptions;
using Application.Common.Interfaces;
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

        public async Task<TodoDailyDto> Add(TodoDaily todoDaily, CancellationToken cancellationToken)
        {


            _context.TodoDailys.Add(todoDaily);
            await _context.SaveChangesAsync(cancellationToken);
            return await _context.TodoDailys
                .ProjectTo<TodoDailyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> Delete(int todoDailyId, CancellationToken cancellationToken)
        {
            var todoDaily = await _context.TodoDailys.FindAsync(todoDailyId);
            if(todoDaily != null)
            {
                _context.TodoDailys.Remove(todoDaily);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return todoDaily.Id;
        }


        public async Task<bool> IsChecked(int todoDailyId)
        {
            return await _context.TodoDailys.AnyAsync(x => x.Check == true);
        }
        public async Task<bool> RemoveIfTrue(int todoDailyId)
        {
            var todoDailyAsset = await _context.TodoDailys.FindAsync(todoDailyId);
            var isAlredyChecked = await IsChecked(todoDailyAsset.Id);
            if (isAlredyChecked)
            {
                _context.TodoDailys.Remove(todoDailyAsset);
            }

            return true;
        }

        public async Task<bool> Update(int todoDailyId, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var todoDailyAsset = await _context.TodoDailys.FindAsync(todoDailyId);

            //check if it is null then return Exception
            if (todoDailyAsset == null)
                throw new NotFoundException("Todo Daily Not Found");

            //Update with a new Data
            todoDailyAsset.MadeUntil = now;
            todoDailyAsset.Check = true;
            _context.TodoDailys.Update(todoDailyAsset);
            await _context.SaveChangesAsync(cancellationToken);

            //Make a new History of todo daily data
            var todoDailyHistory = new TodoDailyHistory
            {
                TodoDailyActivity = todoDailyAsset.TodoDailyActivity,
                CheckStatus = true,
                MadeSince = todoDailyAsset.MadeSince,
                CheckDate = now,
                UserPropertyId = todoDailyAsset.UserPropertyId
            };

            _context.TodoDailyHistories.Add(todoDailyHistory);
            await _context.SaveChangesAsync(cancellationToken);

            //delete data if checked
            
            if (todoDailyAsset.Check)
            {
                _context.TodoDailys.Remove(todoDailyAsset);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;

        }
        public async Task<bool> UnceklistHistory(int todoDailyHistoryId, CancellationToken cancellationToken)
        {
            var assetTodoHistory = await _context.TodoDailyHistories.FindAsync(todoDailyHistoryId);
            
            if (assetTodoHistory == null)
                throw new NotFoundException();

            assetTodoHistory.CheckStatus = false;
            _context.TodoDailyHistories.Update(assetTodoHistory);
            await _context.SaveChangesAsync(cancellationToken);

            // Re Add todo daily
            var todoDaily = new TodoDaily
            {
                UserPropertyId = assetTodoHistory.UserPropertyId,
                TodoDailyActivity = assetTodoHistory.TodoDailyActivity,
                Check = false,
                MadeSince = assetTodoHistory.MadeSince,
                MadeUntil = DateTime.UtcNow.AddDays(1)
            };
            _context.TodoDailys.Add(todoDaily);
            await _context.SaveChangesAsync(cancellationToken);

            //delete cause it got unchecked
            if(assetTodoHistory.CheckStatus == false)
            {
                _context.TodoDailyHistories.Remove(assetTodoHistory);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
        public async Task<IList<TodoDailyHistoryDto>> GetTodoDailyHistory(int todoDailyId, CancellationToken cancellationToken)
        {
            var isAlredyChecked = await IsChecked(todoDailyId);
            if (isAlredyChecked)
            {
                return await _context.TodoDailyHistories
                    .ProjectTo<TodoDailyHistoryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            throw new NotFoundException("Not found");
        }

        private static DateTime GetDefaultDateDue(DateTime now) => now.AddDays(DefaultDateDueDays);

    }
}
