using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TodoDailyHistory
    {
        public int Id { get; set; }
        public string TodoDailyActivity { get; set; }
        public bool CheckStatus { get; set; }
        public DateTime MadeSince { get; set; }
        public DateTime CheckDate { get; set; }
        public UserProperty UserProperty { get; set; }
        public int UserPropertyId { get; set; }
    }
}
