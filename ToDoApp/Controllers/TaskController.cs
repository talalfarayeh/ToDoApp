using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Repository;

namespace ToDoApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;

        
        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var tasks = _repository.GetAll();
            ViewBag.PendingTasks = tasks.Where(t => !t.IsCompleted).ToList();
            ViewBag.CompletedTasks = tasks.Where(t => t.IsCompleted).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(task);
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = _repository.GetById(id);
            if (task != null)
            {
                task.IsCompleted = true;
                _repository.Update(task);
            }
            return Ok(); 
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
