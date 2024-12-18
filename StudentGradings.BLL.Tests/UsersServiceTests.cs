using Moq;
using StudentGradings.DAL.Interfaces;

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
    public void AddUser__()
    {
        //Arrange

        //Act

        //Assert
    }
}
