using AutoFixture;
using Moq;
using Moq.Protected;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;
using StudentGradings.BLL.Models.UserGraduate;
using System.Net;
using System.Text.Json;

namespace StudentGradings.BLL.Tests;

public class GraduateServiceTests
{
    private IGraduatesService _sut;
    private Mock<HttpMessageHandler> _messageHandlerMock = new Mock<HttpMessageHandler>();
    private const string _baseAddress = "https://jsonplaceholder.typicode.com/";

    public GraduateServiceTests()
    {
        _sut = new GraduatesService(_messageHandlerMock.Object);
    }

    [Fact]
    public void SendGraduate_CorrectUserIdPassed_SuccessReceived()
    {
        //Arrange
        var userId = 5;
        var apiEndpoint = $"users/{userId}";
        var fixture = new Fixture();
        var user = fixture.Create<UserGraduate>();
        var response = JsonSerializer.Serialize(user);

        var mockedProtected = _messageHandlerMock.Protected();
        var setupApiRequest = mockedProtected
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => m.RequestUri!.Equals(_baseAddress + apiEndpoint)),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            });
        //Act
        _sut.SendGraduate(new GraduateModel { UserId = userId });
        //Assert
    }
}
