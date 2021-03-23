using Domain.Common;
using Domain.Enums;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TodoItem : AuditableEntity
    {
        public int Id { get; set; }
        public TodoCategory Category { get; set; }
        public int CategoryId { get; set; }
        public string ActivityTitle { get; set; }
        public string Note { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && value != false)
                {
                    DomainEvents.Add(new TodoItemCompletedEvent(this));
                }
                _done = value;
            }
        }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
