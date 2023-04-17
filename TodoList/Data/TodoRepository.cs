using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoRepository : ITodoRepository
    {
        private string _connectionString = "";

        public static Dictionary<int, string> CategoryDictionary { get; private set; }
        public TodoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetCategories()
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var categories = db.Query<Category>("SELECT * FROM Category");
                CategoryDictionary = categories.ToDictionary(category => category.Id, category => category.Name);

                return categories;
            }
        }

        public IEnumerable<TodoTask> GetTasks()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<TodoTask>("SELECT * FROM Task");
            }

        }

        public void AddTask(TodoTask task)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("INSERT INTO Task(Name, DueTo, IsCompleted, Category) " +
                    "VALUES(@Name, @DueTo, @IsCompleted, @Category)",
                    new { task.Name, task.DueTo, task.IsCompleted, task.Category});
            }
        }

        public void MarkTask(int id, bool mark)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                db.Execute("UPDATE Task SET IsCompleted = @mark WHERE id = @id", 
                    new { mark, id});
            }
        }

        public void RemoveTask(int id)
        {
            Console.WriteLine("Trying to remove " + id);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("DELETE FROM Task WHERE id = @id", new { id});
            }
        }

        public void AddCategory(Category category)
        {
            Console.WriteLine("Trying to add category = " + category.Name);

            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("INSERT INTO Category(Name) VALUES(@Name)", new { category.Name });
            }
        }

        public void RemoveCategory(int id)
        {
            Console.WriteLine("Trying to remove category = " + id);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("DELETE FROM Category WHERE id = @id", new { id });
            }
        }
    }
}
