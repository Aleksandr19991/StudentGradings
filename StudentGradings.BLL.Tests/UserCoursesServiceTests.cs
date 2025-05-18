using Moq;
using StudentGradings.BLL.Exeptions;
using StudentGradings.CORE;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Tests;

public class UserCoursesServiceTests
{
    private Mock<IUsersRepository> _usersRepositoryMock;
    private Mock<ICoursesRepository> _coursesRepositoryMock;
    private Mock<IUserCoursesRepository> _userCoursesRepositoryMock;
    private UserCoursesService _sut;
    public UserCoursesServiceTests()
    {
        _coursesRepositoryMock = new Mock<ICoursesRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _userCoursesRepositoryMock = new Mock<IUserCoursesRepository>();
        _sut = new UserCoursesService(_userCoursesRepositoryMock.Object, _coursesRepositoryMock.Object, _usersRepositoryMock.Object);
    }

    [Fact]
    public async Task AddGradeByUserIdAndCourseIdAsync_ExistingActiveCourseAndExistingActiveUser_StudentReceivedGrade()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        float grade = 4;

        var user = new UserDto
        {
            Id = userId,
            IsDeactivated = false
        };

        var course = new CourseDto
        {
            Id = courseId,
            IsDeactivated = false
        };

        _usersRepositoryMock.Setup(r => r.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        _coursesRepositoryMock.Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync(course);

        _userCoursesRepositoryMock.Setup(r => r.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _sut.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade);

        // Assert
        _userCoursesRepositoryMock.Verify(r => r.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade), Times.Once);
    }

    [Fact]
    public async Task AddGradeByUserIdAndCourseIdAsync_NotExistingCourseSent_EntityNotFoundExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var grade = 4;

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByIdAsync(userId))
            .ReturnsAsync(new UserDto { Id = userId, IsDeactivated = false });

        _coursesRepositoryMock
            .Setup(repo => repo.GetCourseByIdAsync(courseId))
            .ReturnsAsync((CourseDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _sut.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade));

        Assert.Equal($"Course with id {courseId} was not found.", exception.Message);

        _userCoursesRepositoryMock.Verify(
            repo => repo.AddGradeByUserIdAndCourseIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<float>()),
            Times.Never);
    }

    [Fact]
    public async Task AddGradeByUserIdAndCourseIdAsync_DeactivatedCourseSent_EntityConflictExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        float grade = 4;

        _usersRepositoryMock
            .Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(new UserDto { Id = userId, IsDeactivated = false });

        _coursesRepositoryMock
            .Setup(x => x.GetCourseByIdAsync(courseId))
            .ReturnsAsync(new CourseDto { Id = courseId, IsDeactivated = true });

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityConflictException>(() =>
            _sut.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade));

        Assert.Equal($"Course with id {courseId} is deactivated.", exception.Message);

        _userCoursesRepositoryMock.Verify(x => x.AddGradeByUserIdAndCourseIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<float>()), Times.Never);
    }

    [Fact]
    public async Task AddGradeByUserIdAndCourseIdAsync_ActiveCourseAndNotExistingUserSent_EntityNotFoundExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var grade = 4;

        _usersRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
            .ReturnsAsync((UserDto)null);

        _coursesRepositoryMock.Setup(repo => repo.GetCourseByIdAsync(courseId))
            .ReturnsAsync(new CourseDto { IsDeactivated = false });

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _sut.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade));

        Assert.Equal($"User with id {userId} was not found.", exception.Message);

        _userCoursesRepositoryMock.Verify(repo => repo.AddGradeByUserIdAndCourseIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<float>()), Times.Never);
    }

    [Fact]
    public async Task AddGradeByUserIdAndCourseIdAsync_ActiveCourseAndDeactivatedUserSent_EntityConflictExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        float grade = 4;

        var deactivatedUser = new UserDto
        {
            Id = userId,
            IsDeactivated = true
        };

        var activeCourse = new CourseDto
        {
            Id = courseId,
            IsDeactivated = false
        };

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByIdAsync(userId))
            .ReturnsAsync(deactivatedUser);

        _coursesRepositoryMock
            .Setup(repo => repo.GetCourseByIdAsync(courseId))
            .ReturnsAsync(activeCourse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityConflictException>(() =>
            _sut.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade));

        Assert.Equal($"User with id {userId} is deactivated.", exception.Message);

        _userCoursesRepositoryMock.Verify(
            repo => repo.AddGradeByUserIdAndCourseIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<float>()),
            Times.Never);
    }

    [Fact]
    public async Task AddGradeByCourseIdAsync_TeacherTriesToAddGradeTwice_EntityConflictExceptionThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var grade = 4;

        var user = new UserDto { Id = userId, IsDeactivated = false, Role = UserRole.Student };
        var course = new CourseDto { Id = courseId, IsDeactivated = false };
        var message = $"Grade with user id {userId} and course id {courseId} already exists.";

        _usersRepositoryMock.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        _coursesRepositoryMock.Setup(x => x.GetCourseByIdAsync(courseId))
            .ReturnsAsync(course);

        _userCoursesRepositoryMock.Setup(c => c.GetUserCourseAsync(userId, courseId)).ReturnsAsync(new UserCourseDto() { User = user, Course = course, Grade = 4 });

        var exception = await Assert.ThrowsAsync<EntityConflictException>(async () => await _sut.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade));

        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task UpdateGradeByCourseIdAndUserIdAsync_WhenUserCourseNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        float grade = 4.5f;

        _userCoursesRepositoryMock
            .Setup(repo => repo.GetUserCourseAsync(courseId, userId))
            .ReturnsAsync((UserCourseDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _sut.UpdateGradeByCourseIdAndUserIdAsync(userId, courseId, grade));

        Assert.Contains($"UserCourse with user {userId} and course {courseId} was not found.", exception.Message);

        _userCoursesRepositoryMock.Verify(repo =>
            repo.UpdateGradeByCourseIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<float>()), Times.Never);
    }

    [Fact]
    public async Task UpdateGradeByCourseIdAndUserIdAsync_WhenUserCourseExists_UpdatesGrade()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        float grade = 4.5f;

        var userCourse = new UserCourseDto();

        _userCoursesRepositoryMock
            .Setup(repo => repo.GetUserCourseAsync(courseId, userId))
            .ReturnsAsync(userCourse);

        _userCoursesRepositoryMock
            .Setup(repo => repo.UpdateGradeByCourseIdAndUserIdAsync(userId, courseId, grade))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _sut.UpdateGradeByCourseIdAndUserIdAsync(userId, courseId, grade);

        // Assert
        _userCoursesRepositoryMock.Verify(repo =>
            repo.UpdateGradeByCourseIdAndUserIdAsync(userId, courseId, grade), Times.Once);
    }

    [Fact]
    public async Task DeleteGradeByCourseIdAndUserIdAsync_GradeExists_DeletesGrade()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        _userCoursesRepositoryMock
            .Setup(repo => repo.GradeExistsByCourseIdAndUserIdAsync(userId, courseId))
            .ReturnsAsync(true);

        _userCoursesRepositoryMock
            .Setup(repo => repo.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _sut.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId);

        // Assert
        _userCoursesRepositoryMock.Verify(repo => repo.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId), Times.Once);
    }

    [Fact]
    public async Task DeleteGradeByCourseIdAndUserIdAsync_GradeDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        _userCoursesRepositoryMock
            .Setup(repo => repo.GradeExistsByCourseIdAndUserIdAsync(userId, courseId))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _sut.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId));

        Assert.Contains($"Grade for user {userId} and course {courseId}  not found.", exception.Message);

        _userCoursesRepositoryMock.Verify(repo => repo.DeleteGradeByCourseIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
    }
}