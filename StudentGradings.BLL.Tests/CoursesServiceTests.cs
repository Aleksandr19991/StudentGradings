using Moq;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Tests;

public class CoursesServiceTests
{
    private Mock<ICoursesRepository> _coursesRepositoryMock;
    private readonly CoursesService _sut;
    public CoursesServiceTests()
    {
        _coursesRepositoryMock = new Mock<ICoursesRepository>();
        _sut = new CoursesService(_coursesRepositoryMock.Object);
    }

    [Fact]
    public async Task AddCourseAsync_ShouldReturnGuid_WhenCourseIsValid()
    {
        // Arrange
        var courseModel = new CourseModel();
        var expectedGuid = Guid.NewGuid();

        _coursesRepositoryMock
            .Setup(r => r.AddCourseAsync(It.IsAny<CourseDto>()))
            .ReturnsAsync(expectedGuid);

        // Act
        var result = await _sut.AddCourseAsync(courseModel);

        // Assert
        Assert.Equal(expectedGuid, result);
        _coursesRepositoryMock.Verify(r => r.AddCourseAsync(It.IsAny<CourseDto>()), Times.Once);
    }

    [Fact]
    public async Task AddCourseAsync_ShouldThrowEntityNotFoundException_WhenCourseModelIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.AddCourseAsync(null));
    }

    [Fact]
    public async Task UpdateCourseAsync_ShouldCallUpdate_WhenCourseExists()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();
        var courseModel = new CourseModel();
        var existingCourseDto = new CourseDto();

        _coursesRepositoryMock
            .Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync(existingCourseDto);

        // Act
        await _sut.UpdateCourseAsync(courseId, courseModel);

        // Assert
        _coursesRepositoryMock.Verify(r => r.UpdateCourseAsync(existingCourseDto, It.IsAny<CourseDto>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCourseAsync_ShouldThrowEntityNotFoundException_WhenCourseModelIsNull()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.UpdateCourseAsync(courseId, null));
    }

    [Fact]
    public async Task UpdateCourseAsync_ShouldThrowEntityNotFoundException_WhenCourseNotFound()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();
        var courseModel = new CourseModel();

        _coursesRepositoryMock
            .Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync((CourseDto)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.UpdateCourseAsync(courseId, courseModel));
    }

    [Fact]
    public async Task GetCourseByIdAsync_ShouldReturnCourseModel_WhenCourseExists()
    {
        // Arrange
        Guid courseId = Guid.NewGuid();
        var courseDto = new CourseDto();

        _coursesRepositoryMock
            .Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync(courseDto);

        // Act & Assert
        var result = await _sut.GetCourseByIdAsync(courseId);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCourseByIdAsync_CourseDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        _coursesRepositoryMock
            .Setup(repo => repo.GetCourseByIdAsync(courseId))
            .ReturnsAsync((CourseDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.GetCourseByIdAsync(courseId));
        Assert.Equal($"Course with id {courseId} was not found.", exception.Message);

        _coursesRepositoryMock.Verify(repo => repo.GetCourseByIdAsync(courseId), Times.Once);
    }

    [Fact]
    public async Task GetCourseWithUsersAndGradesAsync_ReturnsCourseModel_WhenCourseExists()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var courseDto = new CourseDto
        {
            Id = courseId,
        };

        _coursesRepositoryMock
            .Setup(repo => repo.GetCourseWithUsersAndGradesAsync(courseId))
            .ReturnsAsync(courseDto);

        // Act
        var result = await _sut.GetCourseWithUsersAndGradesAsync(courseId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(courseId, result.Id);
    }

    [Fact]
    public async Task GetCourseWithUsersAndGradesAsync_ThrowsEntityNotFoundException_WhenCourseDoesNotExist()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        _coursesRepositoryMock
            .Setup(repo => repo.GetCourseWithUsersAndGradesAsync(courseId))
            .ReturnsAsync((CourseDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _sut.GetCourseWithUsersAndGradesAsync(courseId));

        Assert.Equal($"Course with id {courseId} was not found.", exception.Message);
    }

    [Fact]
    public async Task DeactivateCourseAsync_WhenCourseExists_CallsDeactivateCourseAsync()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var course = new CourseDto { Id = courseId };
        _coursesRepositoryMock.Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync(course);

        // Act
        await _sut.DeactivateCourseAsync(courseId);

        // Assert
        _coursesRepositoryMock.Verify(r => r.DeactivateCourseAsync(course), Times.Once);
    }

    [Fact]
    public async Task DeactivateCourseAsync_WhenCourseDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        _coursesRepositoryMock.Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync((CourseDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeactivateCourseAsync(courseId));
        Assert.Equal($"Course with id {courseId} was not found.", exception.Message);
    }

    [Fact]
    public async Task DeleteCourseAsync_WhenCourseExists_CallsDeleteCourseAsync()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var course = new CourseDto { Id = courseId };
        _coursesRepositoryMock.Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync(course);

        // Act
        await _sut.DeleteCourseAsync(courseId);

        // Assert
        _coursesRepositoryMock.Verify(r => r.DeleteCourseAsync(course), Times.Once);
    }

    [Fact]
    public async Task DeleteCourseAsync_WhenCourseDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        _coursesRepositoryMock.Setup(r => r.GetCourseByIdAsync(courseId))
            .ReturnsAsync((CourseDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeleteCourseAsync(courseId));
        Assert.Equal($"Course with id{courseId} was not found.", exception.Message);
    }
}