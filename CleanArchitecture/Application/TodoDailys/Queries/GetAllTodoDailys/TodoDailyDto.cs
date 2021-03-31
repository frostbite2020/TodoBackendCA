using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoDailys.Queries.GetAllTodoDailys
{
    public class TodoDailyDto : IMapFrom<TodoDaily>
    {
        public string TodoDailyActivity { get; set; }
        public bool Check { get; set; }
    }
}
