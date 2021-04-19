using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoDailys.Commands.UpdateTodoDaily
{
    public class UpdateTodoDailyDto
    {
        public int Id { get; set; }
        public bool CheckStatus { get; set; }
    }
}
