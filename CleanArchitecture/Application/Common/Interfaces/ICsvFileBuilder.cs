using Application.TodoCategories.Queries.ExportTodoCategory;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoCategoryItemFileRecord> records);
    }
}
