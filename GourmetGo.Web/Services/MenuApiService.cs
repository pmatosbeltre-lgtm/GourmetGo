using GourmetGo.Web.Models;
using System.Net.Http.Json;

namespace GourmetGo.Web.Services
{
    public class MenuApiService
    {
        private readonly HttpClient _httpClient;
        public MenuApiService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<Result<MenuDTO>> ObtenerPorRestauranteAsync(int restauranteId)
        {
            try
            {
             
                return await _httpClient.GetFromJsonAsync<Result<MenuDTO>>($"api/Menu/restaurante/{restauranteId}");
            }
            catch (Exception ex)
            {
                return new Result<MenuDTO> { Success = false, Message = ex.Message };
            }
        }
        public async Task<Result<MenuDTO>> CrearAsync(MenuDTO menu)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Menu", menu);

                // Si la respuesta es exitosa (200 o 201)
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new Result<MenuDTO> { Success = true, Data = menu };
                    }

                    return await response.Content.ReadFromJsonAsync<Result<MenuDTO>>();
                }

                
                var errorRaw = await response.Content.ReadAsStringAsync();
                return new Result<MenuDTO>
                {
                    Success = false,
                    Message = $"Error {(int)response.StatusCode}: {errorRaw}"
                };
            }
            catch (Exception ex)
            {
                return new Result<MenuDTO> { Success = false, Message = $"Error inesperado: {ex.Message}" };
            }
        }
    }
}
