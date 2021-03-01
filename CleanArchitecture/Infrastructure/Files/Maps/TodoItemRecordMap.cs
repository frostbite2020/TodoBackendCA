using Application.TodoCategories.Queries.ExportTodoCategory;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Infrastructure.Files.Maps
{
    public class TodoItemRecordMap : ClassMap<TodoCategoryItemFileRecord>
    {
        public TodoItemRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
