using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Seguridad;

namespace GourmetGo.Desktop.Services;

public class AuthService
{
    private readonly ApiClient _api;

    public AuthService(ApiClient api)
    {
        _api = api;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto)
    {
        // La API devuelve LoginResponseDTO directo (no Result<>)
        var response = await _api.PostAsync<LoginRequestDTO, LoginResponseDTO>("/api/Auth/login", dto);

        if (!string.IsNullOrWhiteSpace(response.Token))
            TokenStore.SetToken(response.Token);

        return response;
    }

    public void Logout() => TokenStore.Clear();
}