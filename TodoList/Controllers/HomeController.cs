using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITodoRepository _repo;
        private IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _repo = new TodoRepository(_config.GetConnectionString("MyDatabase"));
        }
        
        public IActionResult Index() 
        {
            var viewModel = new TodoItemsViewModel(
                _repo.GetCategories(),
                _repo.GetTasks());

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateTodoTask(TodoTask task)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            _repo.AddTask(task);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Mark(TaskMarkViewModel taskMark)
        {
            _repo.MarkTask(taskMark.Id, taskMark.IsCompleted);

            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult DeleteTodoTask(TodoTaskViewModel taskModel)
        {
            _repo.RemoveTask(taskModel.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult AddCategory(Category category)
        {
            _repo.AddCategory(category);

            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult RemoveCategory(CategoryViewModel categoryModel)
        {
            _repo.RemoveCategory(categoryModel.Id);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}