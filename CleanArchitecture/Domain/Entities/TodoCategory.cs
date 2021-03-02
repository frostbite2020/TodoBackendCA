using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TodoCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }
        public IList<TodoItem> Lists { get; private set; } = new List<TodoItem>();
    }
}
