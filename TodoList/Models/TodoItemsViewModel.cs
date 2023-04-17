namespace TodoList.Models
{
    public class TodoItemsViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();

        public IEnumerable<TodoTask> TodoTasks { get; set; } = Enumerable.Empty<TodoTask>();

        public TodoItemsViewModel(IEnumerable<Category> categories, IEnumerable<TodoTask> todoTasks)
        {
            Categories = categories; 
            TodoTasks = todoTasks;
        }
    }
}
