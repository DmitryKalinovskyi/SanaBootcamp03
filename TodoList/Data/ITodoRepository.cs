using TodoList.Models;

namespace TodoList.Data
{
    public interface ITodoRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<TodoTask> GetTasks();

        void AddTask(TodoTask task);
        void MarkTask(int id, bool mark);
        void RemoveTask(int id);

        void AddCategory(Category category);
        void RemoveCategory(int id);

    }
}
