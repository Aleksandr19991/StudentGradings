using Moq;
using StudentGradings.DAL.Interfaces;

namespace StudentGradings.BLL.Tests;

public class GradeBooksTests
{
    private IMock<ICoursesRepository> _coursesRepositoryMock;
    private IMock<IUsersRepository> _usersRepositoryMock;
    private IMock<IGradeBooksRepository> _gradeBooksRepositoryMock;
    private GradeBooksService _sut;
    public GradeBooksTests()
    {
        _coursesRepositoryMock = new Mock<ICoursesRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _gradeBooksRepositoryMock = new Mock<IGradeBooksRepository>();
        _sut = new GradeBooksService(_gradeBooksRepositoryMock.Object, _coursesRepositoryMock.Object, _usersRepositoryMock.Object);
    }

    [Fact]
    public void Test1()
    {
        //Arrange

        //Act

        //Assert
    }
}