using Microsoft.AspNetCore.Mvc;
using Moq;
using PreventorTest.Api.Controllers;
using PreventorTest.Application.Students;
using PreventorTest.Application.Students.Domain;
using PreventorTest.Application.Students.Interfaces;
using Xunit;

namespace PreventorBackEndTest.Infrastructure.Tests
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentRepository> _mockRepository;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockRepository = new Mock<IStudentRepository>();
            _controller = new StudentsController(_mockRepository.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            var students = new List<Student> { new Student { StudentId = 1, Name = "Test", Surname = "User" } };
            _mockRepository.Setup(repo => repo.Get()).ReturnsAsync(students);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Student>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenStudentExists()
        {
            // Arrange
            var student = new Student { StudentId = 1, Name = "Test", Surname = "User" };
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(student);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(1, returnValue.StudentId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsOkResult_WithCreatedStudent()
        {
            // Arrange
            var studentDto = new StudentDTO { Name = "Test", Surname = "User" };
            var student = new Student { StudentId = 1, Name = "Test", Surname = "User" };
            _mockRepository.Setup(repo => repo.Add(studentDto)).ReturnsAsync(student);

            // Act
            var result = await _controller.Post(studentDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(1, returnValue.StudentId);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var studentDto = new StudentDTO { Name = "Updated", Surname = "User" };
            var student = new Student { StudentId = 1, Name = "Updated", Surname = "User" };
            _mockRepository.Setup(repo => repo.Update(studentDto, 1)).ReturnsAsync(student);

            // Act
            var result = await _controller.Put(studentDto, 1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            var studentDto = new StudentDTO { Name = "Updated", Surname = "User" };
            _mockRepository.Setup(repo => repo.Update(studentDto, 1)).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.Put(studentDto, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteById(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteById(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}