using Moq;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Tests;

public class GradeBooksServiceTests
{
    //private Mock<ICoursesRepository> _coursesRepositoryMock;
    //private Mock<IUsersRepository> _usersRepositoryMock;
    //private Mock<IGradeBooksRepository> _gradeBooksRepositoryMock;
    //private UserCoursesService _sut;
    //public GradeBooksServiceTests()
    //{
    //    _coursesRepositoryMock = new Mock<ICoursesRepository>();
    //    _usersRepositoryMock = new Mock<IUsersRepository>();
    //    _gradeBooksRepositoryMock = new Mock<IGradeBooksRepository>();
    //    _sut = new GradeBooksService(_gradeBooksRepositoryMock.Object, _coursesRepositoryMock.Object, _usersRepositoryMock.Object);
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_ExistingActiveCourseAndExistingActiveUser_StudentReceivedGrade()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId });
    //    _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(new UserDto() { Id = userId, Role = UserRole.Student });
    //    //Act
    //    await _sut.AddGradeByCourseIdAsync(courseId, userId);
    //    //Assert
    //    _gradeBooksRepositoryMock.Verify(c => c.AddGradeByCourseIdAsync(It.IsAny<GradeBookDto>()), Times.Once);
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_ExistingActiveCourseAndExistingActiveUser_StudentReceivedGrade()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var existingCourse = new CourseDto() { Id = courseId };
    //    var existingStudent = new UserDto() { Id = userId, Role = UserRole.Student };
    //    _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(existingCourse);
    //    _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(existingStudent);
    //    //Act
    //    await _sut.AddGradeByCourseIdAndUserIdAsync(courseId, userId, float grade);
    //    //Assert
    //    _gradeBooksRepositoryMock.Verify(c => 
    //        c.AddGradeByCourseIdAndUserIdAsync(It.Is<GradeBookDto>(c => c.Course == existingCourse && c.User == existingStudent)),
    //        Times.Once
    //    );
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_NotExistingCourseSent_EntityNotFoundExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var message = $"Course with id{courseId} was not found.";
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.AddGradeByCourseIdAndUserIdAsync(courseId, Guid.NewGuid()));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_DeactivatedCourseSent_EntityConflictExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var message = $"Course with id{courseId} is deactivated.";
    //    _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId, IsDeactivated = true });
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByCourseIdAndUserIdAsync(courseId, Guid.NewGuid()));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_ActiveCourseAndNotExistingUserSent_EntityNotFoundExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var message = $"User with id{userId} was not found.";
    //    _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId });
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.AddGradeByCourseIdAndUserIdAsync(courseId, userId));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_ActiveCourseAndDeactivatedUserSent_EntityConflictExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var message = $"User with id{userId} is deactivated.";
    //    _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(new CourseDto() { Id = courseId });
    //    _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(new UserDto() { Id = userId, IsDeactivated = true });
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByCourseIdAndUserIdAsync(courseId, userId));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}

    //[Fact]
    //public async Task AddGradeByCourseIdAsync_TeacherTriesToAddGradeTwice_EntityConflictExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var course = new CourseDto() { Id = courseId };
    //    var user = new UserDto() { Id = userId, Role = UserRole.Student };
    //    var message = $"Grade with user id{userId} and course id{courseId} already exists.";
    //    _coursesRepositoryMock.Setup(c => c.GetCourseByIdAsync(courseId)).ReturnsAsync(course);
    //    _usersRepositoryMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(user);
    //    _gradeBooksRepositoryMock.Setup(c => c.GetGradeBookAsync(courseId, userId)).ReturnsAsync(new GradeBookDto() { Course = course, User = user });
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByCourseIdAndUserIdAsync(courseId, userId));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}

    //[Fact]
    //public async Task UpdateGradeByCourseIdAsync_ExistingActiveGradeBookAndExistingActiveNewGradeBook_TeacherChangeGrade()
    //{
    //    //Arrange
    //    var id = Guid.NewGuid();
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var gradeBook = new GradeBookModel();
    //    _gradeBooksRepositoryMock.Setup(c => c.GetGradeBookAsync(gradeBook.CourseId,gradeBook.UserId)).ReturnsAsync(new GradeBookDto() {});
    //    //Act
    //    await _sut.UpdateGradeByCourseIdAsync(id, gradeBook);
    //    //Assert
    //    _gradeBooksRepositoryMock.Verify(c => c.UpdateGradeByCourseIdAsync(It.IsAny<GradeBookDto>(),gradeBook.Grade), Times.Once);
    //}


    //[Fact]
    //public async Task UpdateGradeByCourseIdAsync_NotExistingGradeBookSent_EntityNotFoundExceptionThrown()
    //{
    //    //Arrange
    //    var courseId = Guid.NewGuid();
    //    var userId = Guid.NewGuid();
    //    var gradeBook = new GradeBookModel();
    //    var message = $"GradeBook with course id{gradeBook.CourseId} and user id{gradeBook.UserId} id was not found.";
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.UpdateGradeByCourseIdAsync(Guid.NewGuid(), gradeBook));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}

    //[Fact]
    //public async Task UpdateGradeByCourseIdAsync_NotExistingNewGradeBookSent_EntityNotFoundExceptionThrown()
    //{
    //    //Arrange
    //    var id = Guid.NewGuid();
    //    var gradeBook = new GradeBookModelBll();
    //    var message = $"NewGradeBook with id{id} was not found.";
    //    //Act
    //    var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.UpdateGradeByCourseIdAsync(Guid.NewGuid(), gradeBook));
    //    //Assert
    //    Assert.Equal(message, exception.Message);
    //}
}