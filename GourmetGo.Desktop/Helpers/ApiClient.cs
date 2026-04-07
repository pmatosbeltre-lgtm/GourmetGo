using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GourmetGo.Desktop.Configuration;

namespace GourmetGo.Desktop.Helpers;

public class ApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiClient()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri(ApiConfiguration.BaseUrl),
            Timeout = ApiConfiguration.Timeout
        };
    }

    private void AttachAuthorizationHeader()
    {
        _http.DefaultRequestHeaders.Authorization = null;

        if (TokenStore.HasToken)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.AccessToken);
        }
    }

    public async Task<T> GetAsync<T>(string relativeUrl)
    {
        AttachAuthorizationHeader();

        var response = await _http.GetAsync(relativeUrl);
        var body = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<T>(body, JsonOptions)
               ?? throw new InvalidOperationException("La API devolvió una respuesta vacía.");
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest request)
    {
        AttachAuthorizationHeader();

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(relativeUrl, content);
        var body = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<TResponse>(body, JsonOptions)
               ?? throw new InvalidOperationException("La API devolvió una respuesta vacía.");
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string relativeUrl, TRequest request)
    {
        AttachAuthorizationHeader();

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PutAsync(relativeUrl, content);
        var body = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<TResponse>(body, JsonOptions)
               ?? throw new InvalidOperationException("La API devolvió una respuesta vacía.");
    }

    public async Task DeleteAsync(string relativeUrl)
    {
        AttachAuthorizationHeader();

        var response = await _http.DeleteAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
    }
}