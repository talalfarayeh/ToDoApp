using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Repository
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly JsonFileService _jsonFileService;
        private List<TaskItem> _tasks;

        public JsonTaskRepository(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
            _tasks = _jsonFileService.LoadTasks();  
        }

        public IEnumerable<TaskItem> GetAll() => _tasks;

        public TaskItem GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        public void Add(TaskItem task)
        {
            task.Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;  
            _tasks.Add(task);
            _jsonFileService.SaveTasks(_tasks);  
        }

        public void Update(TaskItem task)
        {
            var existing = GetById(task.Id);
            if (existing != null)
            {
                existing.Title = task.Title;
                existing.IsCompleted = task.IsCompleted;
                _jsonFileService.SaveTasks(_tasks);  
            }
        }

        public void Delete(int id)
        {
            _tasks = _tasks.Where(t => t.Id != id).ToList();
            _jsonFileService.SaveTasks(_tasks);  
        }
    }
}
