using GourmetGo.Desktop.Services;
using GourmetGo.Application.DTOs.Seguridad;

namespace GourmetGo.Desktop.ViewModels;

public class LoginViewModel
{
    private readonly AuthService _authService;

    public string Correo { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponseDTO> LoginAsync()
    {
        var dto = new LoginRequestDTO
        {
            Correo = Correo,
            Contrasena = Contrasena
        };

        return await _authService.LoginAsync(dto);
    }

    public void Logout() => _authService.Logout();
}