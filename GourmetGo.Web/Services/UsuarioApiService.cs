using System.Net.Http.Json;
using GourmetGo.Web.Models;

namespace GourmetGo.Web.Services
{
    public class UsuarioApiService
    {
        private readonly HttpClient _httpClient;

        public UsuarioApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<Result<UsuarioDTO>> CrearUsuarioAsync(CreateUsuarioDTO dto)
        {
            try
            {

                using var clienteTemporal = new HttpClient();
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5043/api/Usuario", dto);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Result<UsuarioDTO>>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new Result<UsuarioDTO> { Success = false, Message = $"Error: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return new Result<UsuarioDTO> { Success = false, Message = "No se pudo conectar con la API." };
            }
        }
        public async Task<Result<LoginResponseDTO>> LoginAsync(LoginRequestDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", dto);

                if (response.IsSuccessStatusCode)
                {
                    var loginData = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                    if (loginData != null && !string.IsNullOrEmpty(loginData.Token))
                    {
                        // Ahora devolvemos TODO el objeto loginData en la propiedad Data
                        return new Result<LoginResponseDTO>
                        {
                            Success = true,
                            Data = loginData,
                            Message = "¡Bienvenido a GourmetGo!"
                        };
                    }
                }

                return new Result<LoginResponseDTO> { Success = false, Message = "Correo o contraseña incorrectos." };
            }
            catch (Exception)
            {
                return new Result<LoginResponseDTO> { Success = false, Message = "Error de conexión con el servidor." };
            }
        }
    }
}
