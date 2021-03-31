using Application.Common.Mappings;
using Application.TodoDailys.Queries.GetAllTodoDailys;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoDailys.Queries.GetTodoDailyHistories
{
    public class TodoDailyHistoryDto : IMapFrom<TodoDailyHistory>
    {
        public int Id { get; set; }
        public bool CheckStatus { get; set; }
        public TodoDailyDto TodoDaily { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
