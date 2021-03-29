using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public TodoCategory Category { get; set; }
        public int CategoryId { get; set; }
        public string ActivityTitle { get; set; }
        public string Note { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool Done{ get; set; }
    }
}
