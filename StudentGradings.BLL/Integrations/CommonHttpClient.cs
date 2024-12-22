using StudentGradings.BLL.Exeptions;
using System.Text.Json;

namespace StudentGradings.BLL.Integrations;

internal class CommonHttpClient<T> // CommonHttpClient<User>
{
    private readonly HttpClient _httpClient = new HttpClient();
    private JsonSerializerOptions _options;

    public CommonHttpClient(string baseUrl, HttpMessageHandler? handler = null)
    {
        if (handler != null)
        {
            _httpClient = new HttpClient(handler);
        }

        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 5, 0);
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public T SendGetRequest(string path) // public User SendGetRequest(string path)
    {
        try
        {
            var response = (_httpClient.GetAsync(path)).Result;
            response.EnsureSuccessStatusCode();

            var content = (response.Content.ReadAsStringAsync()).Result;
            var data = JsonSerializer.Deserialize<T>(content, _options); // var user = JsonSerializer.Deserialize<user>(content, _options);
            return data;
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Request to jsonplaceholder failed");
        }
    }
}
