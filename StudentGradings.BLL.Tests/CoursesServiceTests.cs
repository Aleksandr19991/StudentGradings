using Moq;
using StudentGradings.DAL.Interfaces;

namespace StudentGradings.BLL.Tests;

public class CoursesServiceTests
{
    private Mock<ICoursesRepository> _coursesRepositoryMock;
    private CoursesService _sut;
    public CoursesServiceTests()
    {
        _coursesRepositoryMock = new Mock<ICoursesRepository>();
        _sut = new CoursesService(_coursesRepositoryMock.Object);
    }

    //[Fact]
    //public void AddCourse__ValidCourse
    //{
    //    //Arrange

    //    //Act

    //    //Assert
    //}
}
