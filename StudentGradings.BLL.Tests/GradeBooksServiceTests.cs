using Moq;
using StudentGradings.BLL.Exeptions;
using StudentGradings.CORE;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Tests;

public class GradeBooksServiceTests
{
    private Mock<ICoursesRepository> _coursesRepositoryMock;
    private Mock<IUsersRepository> _usersRepositoryMock;
    private Mock<IGradeBooksRepository> _gradeBooksRepositoryMock;
    private GradeBooksService _sut;
    public GradeBooksServiceTests()
    {
        _coursesRepositoryMock = new Mock<ICoursesRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _gradeBooksRepositoryMock = new Mock<IGradeBooksRepository>();
        _sut = new GradeBooksService(_gradeBooksRepositoryMock.Object, _coursesRepositoryMock.Object, _usersRepositoryMock.Object);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_ExistingActiveCourseAndExistingActiveUser_StudentReceivedGrade()
    {
        //Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId });
        _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(new UserDto() { Id = userId, Role = UserRole.Student });
        //Act
        await _sut.AddGradeByCourseIdAsync(courseId, userId);
        //Assert
        _gradeBooksRepositoryMock.Verify(c => c.AddGradeByCourseIdAsync(It.IsAny<GradeBookDto>()), Times.Once);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_NotExistingCourseSent_EntityNotFoundExceptionThrown()
    {
        //Arrange
        var courseId = Guid.NewGuid();
        var message = $"Course with id{courseId} was not found.";
        //Act
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.AddGradeByCourseIdAsync(courseId, Guid.NewGuid()));
        //Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_DeactivatedCourseSent_EntityConflictExceptionThrown()
    {
        //Arrange
        var courseId = Guid.NewGuid();
        var message = $"Course with id{courseId} is deactivated.";
        _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId, IsDeactivated = true });
        //Act
        var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByCourseIdAsync(courseId, Guid.NewGuid()));
        //Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_ActiveCourseAndNotExistingUserSent_EntityNotFoundExceptionThrown()
    {
        //Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var message = $"User with id{userId} was not found.";
        _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId });
        //Act
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.AddGradeByCourseIdAsync(courseId, userId));
        //Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_ActiveCourseAndDeactivatedUserSent_EntityConflictExceptionThrown()
    {
        //Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var message = $"User with id{userId} is deactivated.";
        _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId });
        _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(new UserDto() { Id = userId, IsDeactivated = true });
        //Act
        var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByCourseIdAsync(courseId, userId));
        //Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_TeacherTriesToAddGradeTwice_EntityConflictExceptionThrown()
    {
        //Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var course = new CourseDto() { Id = courseId };
        var user = new UserDto() { Id = userId, Role = UserRole.Student };
        var message = $"Grade with user id{userId} and course id {userId} already exists.";
        _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(course);
        _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(user);
        _gradeBooksRepositoryMock.Setup(c => c.GetGradeBookAsync(courseId, userId)).ReturnsAsync(new GradeBookDto() { Course = course, User = user });
        //Act
        var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByCourseIdAsync(courseId, userId));
        //Assert
        Assert.Equal(message, exception.Message);
    }

    //[Fact]
    //public void UpdateGradeByCourseId_NotExistingCourseSent_EntityNotFoundExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var course = new CourseDto() { Id = courseId };
    //    var user = new UserDto() { Id = userId, Role = UserRole.Student };
    //    var gradeBook = new GradeBookDto() { };
    //    var message = $"GradeBook with course id{gradeBook.CourseId} and user{gradeBook.UserId} id was not found.";
    //    _coursesRepositoryMock.Setup(c => c.GetCourseById(courseId)).Returns(course);
    //    _usersRepositoryMock.Setup(c => c.GetUserById(userId)).Returns(user);
    //    _gradeBooksRepositoryMock.Setup(c => c.GetGradeBook(courseId, userId)).Returns(new GradeBookDto() { Course = course, User = user, Grade = gradeBook.Grade });
    //    //Act
    //    var exception = Assert.Throws<EntityNotFoundException>(() => _sut.UpdateGradeByCourseId(gradeBook.grade));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}
}