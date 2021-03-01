namespace Application.TodoCategories.Queries.ExportTodoCategory
{
    public class ExportTodoCategoriesVm
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
