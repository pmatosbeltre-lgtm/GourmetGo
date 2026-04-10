using System.Net.Http.Json;
using GourmetGo.Web.Models;

namespace GourmetGo.Web.Services
{
    public class RestauranteApiService
    {
        private readonly HttpClient _httpClient;

        public RestauranteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RestauranteDTO>> ObtenerTodosAsync()
        {
            try
            {

                var response = await _httpClient.GetFromJsonAsync<Result<List<RestauranteDTO>>>("api/restaurante");

                if (response != null && response.Success)
                {
                    return response.Data;
                }


                return new List<RestauranteDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de red al conectar con la API: {ex.Message}");
                return new List<RestauranteDTO>();
            }
        }


        public async Task<Result<string>> CrearAsync(RestauranteDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Restaurante", dto);
                if (response.IsSuccessStatusCode)
                {

                    return await response.Content.ReadFromJsonAsync<Result<string>>();
                }
                return new Result<string> { Success = false, Message = "Error en el servidor" };
            }
            catch (Exception ex)
            {
                return new Result<string> { Success = false, Message = ex.Message };
            }
        }
        public async Task<Result<RestauranteDTO>> ObtenerPorIdAsync(int id)
        {
            try
            {

                var response = await _httpClient.GetFromJsonAsync<Result<RestauranteDTO>>($"api/restaurante/{id}");

                if (response != null && response.Success)
                {
                    return response;
                }

                return new Result<RestauranteDTO> { Success = false, Message = "No se pudo obtener el restaurante." };
            }
            catch (Exception ex)
            {
                return new Result<RestauranteDTO> { Success = false, Message = ex.Message };
            }
        }
    }
}