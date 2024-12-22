using StudentGradings.BLL.Integrations;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;

namespace StudentGradings.BLL;

public class GraduatesService : IGraduatesService
{
    private readonly CommonHttpClient<UserGraduate> _httpClient;

    public GraduatesService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient<UserGraduate>("https://jsonplaceholder.typicode.com/", handler);
    }

    public void SendGraduate(GraduateModelBll order)
    {
        // check bussines rules against some order
        var user = _httpClient.SendGetRequest($"users/{order.UserId}");
        // process delivery
    }
}
