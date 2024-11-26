using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoApp.Controllers;
using ToDoApp.Models;
using ToDoApp.Repository;
using Xunit;

namespace ToDoApp.Tests
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskRepository> _mockRepository;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _mockRepository = new Mock<ITaskRepository>();
            _controller = new TaskController(_mockRepository.Object);
        }

        [Fact]
        public void Index_ShouldReturnViewResult_WithTasks()
        {
            // Arrange
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Pending Task", IsCompleted = false },
                new TaskItem { Id = 2, Title = "Completed Task", IsCompleted = true }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(tasks);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(tasks.Count, ((List<TaskItem>)viewResult.ViewData["PendingTasks"]).Count + ((List<TaskItem>)viewResult.ViewData["CompletedTasks"]).Count);
        }

        [Fact]
        public void Add_ValidTask_ShouldCallRepositoryAdd()
        {
            // Arrange
            var task = new TaskItem { Title = "New Task" };

            // Act
            _controller.Add(task);

            // Assert
            _mockRepository.Verify(repo => repo.Add(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public void Delete_Task_ShouldCallRepositoryDelete()
        {
            // Arrange
            var taskId = 1;

            // Act
            _controller.Delete(taskId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(taskId), Times.Once);
        }
    }
}
