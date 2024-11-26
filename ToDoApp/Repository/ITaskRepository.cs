using ToDoApp.Models;

namespace ToDoApp.Repository
{
    public interface ITaskRepository
    {
        IEnumerable<TaskItem> GetAll();  
        TaskItem GetById(int id);        
        void Add(TaskItem task);         
        void Update(TaskItem task);     
        void Delete(int id);             
    }
}
