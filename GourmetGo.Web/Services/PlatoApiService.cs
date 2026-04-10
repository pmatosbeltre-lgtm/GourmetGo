using GourmetGo.Web.Models;
using System.Net.Http.Json;

namespace GourmetGo.Web.Services
{
    public class PlatoApiService
    {
        private readonly HttpClient _httpClient;

        public PlatoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<Result<List<PlatoDTO>>> ObtenerPorMenuAsync(int menuId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Result<List<PlatoDTO>>>($"api/Plato/menu/{menuId}");
                return response ?? new Result<List<PlatoDTO>> { Success = false, Message = "No se recibió respuesta." };
            }
            catch (Exception ex)
            {

                return new Result<List<PlatoDTO>> { Success = false, Message = ex.Message };
            }
        }


        public async Task<Result<bool>> CrearPlatoAsync(CreatePlatoDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Plato", dto);

                if (response.IsSuccessStatusCode)
                {
                    // Si la API responde bien, devolvemos éxito sin importar el contenido del JSON
                    return new Result<bool> { Success = true, Message = "Plato creado correctamente" };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new Result<bool> { Success = false, Message = $"Error: {errorContent}" };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Success = false, Message = $"Error de conexión: {ex.Message}" };
            }
        }
    }
}