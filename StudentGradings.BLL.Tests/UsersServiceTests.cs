using Moq;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentGradings.BLL.Tests;

public class UsersServiceTests
{
    private Mock<IUsersRepository> _usersRepositoryMock;
    private UsersService _sut;
    public UsersServiceTests()
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _sut = new UsersService(_usersRepositoryMock.Object);
    }

    [Fact]
    public async Task AuthenticateAsync_ValidEmailAndPassword_ReturnsToken()
    {
        // Arrange
        var email = "a@example.com";
        var password = "password123";

        var user = new UserDto
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = password,
            Role = UserRole.Administrator
        };

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByEmailAsync(email))
            .ReturnsAsync(user);

        // Act
        var token = await _sut.AuthenticateAsync(email, password);

        // Assert
        Assert.NotNull(token);
        Assert.IsType<string>(token);
        Assert.NotEmpty(token);

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
        Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == user.Role.ToString());
    }

    [Fact]
    public async Task AuthenticateAsync_UserNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var email = "a@example.com";
        var password = "Password123";

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByEmailAsync(email))
            .ReturnsAsync((UserDto?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.AuthenticateAsync(email, password));
        Assert.Equal("The Email or Password is entered incorrectly, please try again", exception.Message);
    }

    [Fact]
    public async Task AuthenticateAsync_InvalidPassword_ThrowsEntityNotFoundException()
    {
        // Arrange
        var email = "a@example.com";
        var correctPassword = "correctPassword";
        var wrongPassword = "wrongPassword";

        var user = new UserDto
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = correctPassword,
            Role = UserRole.Administrator
        };

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByEmailAsync(email))
            .ReturnsAsync(user);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.AuthenticateAsync(email, wrongPassword));
        Assert.Equal("The Email or Password is entered incorrectly, please try again", exception.Message);
    }

    [Fact]
    public async Task AddUserAsync_WhenUserModelIsNull_ThrowsEntityNotFoundException()
    {
        // Arrange
        UserModel userModel = null;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.AddUserAsync(userModel));
        Assert.Contains("User with id", exception.Message);
    }

    [Fact]
    public async Task UpdateUserAsync_UserNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.GetUserByIdAsync(userId))
            .ReturnsAsync((UserDto)null);

        var userModel = new UserModel();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _sut.UpdateUserAsync(userId, userModel));

        Assert.Equal($"User with id {userId} was not found.", exception.Message);

        _usersRepositoryMock.Verify(r => r.UpdateUserAsync(It.IsAny<UserDto>(), It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public async Task UpdatePasswordAsync_UserExists_UpdatesPassword()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new UserDto { Id = userId};
        var newPassword = "Password123";

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        _usersRepositoryMock
            .Setup(repo => repo.UpdatePasswordByUserIdAsync(user, newPassword))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _sut.UpdatePasswordAsync(userId, newPassword);

        // Assert
        _usersRepositoryMock.Verify(repo => repo.UpdatePasswordByUserIdAsync(user, newPassword), Times.Once);
    }

    [Fact]
    public async Task UpdatePasswordAsync_UserDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var newPassword = "Password123";

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByIdAsync(userId))
            .ReturnsAsync((UserDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.UpdatePasswordAsync(userId, newPassword));
        Assert.Equal($"User with id {userId} was not found.", exception.Message);

        _usersRepositoryMock.Verify(repo => repo.UpdatePasswordByUserIdAsync(It.IsAny<UserDto>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _usersRepositoryMock
            .Setup(repo => repo.GetUserByIdAsync(userId))
            .ReturnsAsync((UserDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.GetUserByIdAsync(userId));
        Assert.Equal($"User with id {userId} was not found.", exception.Message);
    }

    [Fact]
    public async Task GetUserWithCoursesAndGradesAsync_UserExists_ReturnsMappedUserModel()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new UserDto{ Id = userId};

        _usersRepositoryMock
            .Setup(repo => repo.GetUserWithCoursesAndGradesAsync(userId))
            .ReturnsAsync(userDto);

        // Act
        var result = await _sut.GetUserWithCoursesAndGradesAsync(userId);

        // Assert
        Assert.NotNull(result);
        _usersRepositoryMock.Verify(repo => repo.GetUserWithCoursesAndGradesAsync(userId), Times.Once);
    }

    [Fact]
    public async Task GetUserWithCoursesAndGradesAsync_UserNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _usersRepositoryMock
            .Setup(repo => repo.GetUserWithCoursesAndGradesAsync(userId))
            .ReturnsAsync((UserDto)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.GetUserWithCoursesAndGradesAsync(userId));
        Assert.Equal($"User with id {userId} was not found.", exception.Message);

        _usersRepositoryMock.Verify(repo => repo.GetUserWithCoursesAndGradesAsync(userId), Times.Once);
    }

    [Fact]
    public async Task DeactivateUser_UserExists_CallsDeactivateUserAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new UserDto { Id = userId };
        _usersRepositoryMock.Setup(r => r.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        await _sut.DeactivateUser(userId);

        // Assert
        _usersRepositoryMock.Verify(r => r.DeactivateUserAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeactivateUser_UserNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.GetUserByIdAsync(userId))
            .ReturnsAsync((UserDto)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeactivateUser(userId));
        Assert.Equal($"User with id {userId} was not found.", ex.Message);
    }

    [Fact]
    public async Task DeleteUserAsync_UserExists_CallsDeleteUserAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new UserDto { Id = userId };
        _usersRepositoryMock.Setup(r => r.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        await _sut.DeleteUserAsync(userId);

        // Assert
        _usersRepositoryMock.Verify(r => r.DeleteUserAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_UserNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.GetUserByIdAsync(userId))
            .ReturnsAsync((UserDto)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeleteUserAsync(userId));
        Assert.Equal($"User with id {userId} was not found.", ex.Message);
    }
}