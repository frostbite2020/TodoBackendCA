using Application.TodoCategories.Queries.ExportTodoCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoCategoryItemFileRecord> records);
    }
}
