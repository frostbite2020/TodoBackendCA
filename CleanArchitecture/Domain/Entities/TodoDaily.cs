using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TodoDaily
    {
        public int Id { get; set; }
        public string TodoDailyActivity { get; set; }
        public bool Check { get; set; }
        public DateTime MadeSince { get; set; }
        public DateTime? MadeUntil { get; set; }
        public UserProperty UserProperty { get; set; }
        public int UserPropertyId { get; set; }

    }
}
