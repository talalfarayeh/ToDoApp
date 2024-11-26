using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Repository;
using ToDoApp.Services;

namespace ToDoApp.Tests
{
    public class JsonTaskRepositoryTests
    {
        private readonly string _testFilePath = "test_tasks.json";
        private readonly JsonFileService _jsonFileService;
        private readonly JsonTaskRepository _repository;

        public JsonTaskRepositoryTests()
        {
             _jsonFileService = new JsonFileService(_testFilePath);
            _repository = new JsonTaskRepository(_jsonFileService);
        }

         public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Fact]
        public void Add_Task_ShouldIncreaseCount()
        {
             var task = new TaskItem { Title = "Test Task" };

             _repository.Add(task);

             var tasks = _repository.GetAll();
            Assert.Single(tasks);
            Assert.Equal("Test Task", tasks.First().Title);
        }

        [Fact]
        public void Mark_Task_As_Completed_ShouldUpdateTask()
        {
             var task = new TaskItem { Title = "Test Task" };
            _repository.Add(task);

            
            task.IsCompleted = true;
            _repository.Update(task);

             
            var updatedTask = _repository.GetById(task.Id);
            Assert.True(updatedTask.IsCompleted);
        }

        [Fact]
        public void Delete_Task_ShouldReduceCount()
        {
             
            var task = new TaskItem { Title = "Test Task" };
            _repository.Add(task);

             
            _repository.Delete(task.Id);

             
            var tasks = _repository.GetAll();
            Assert.Empty(tasks);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenNoTasksExist()
        {
             
            var tasks = _repository.GetAll();

             
            Assert.Empty(tasks);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectTask()
        {
            
            var task1 = new TaskItem { Title = "Task 1" };
            var task2 = new TaskItem { Title = "Task 2" };
            _repository.Add(task1);
            _repository.Add(task2);

            
            var retrievedTask = _repository.GetById(task2.Id);

             
            Assert.NotNull(retrievedTask);
            Assert.Equal("Task 2", retrievedTask.Title);
        }
    }
}
