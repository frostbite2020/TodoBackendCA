﻿using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoDailys.Queries.GetTodoDailyHistories
{
    public class TodoDailyHistoryVm
    {
        public PaginatedList<TodoDailyHistoryDto> TodoDailyHistories { get; set; }
    }
}
