using System.Net.Http.Json;
using GourmetGo.Web.Models;

namespace GourmetGo.Web.Services
{
    public class ReservaApiService
    {
        private readonly HttpClient _httpClient;

        public ReservaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<ReservaDTO>> CrearReservaAsync(CreateReservaDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Reserva", dto);
                return await response.Content.ReadFromJsonAsync<Result<ReservaDTO>>();
            }
            catch (Exception)
            {
                return new Result<ReservaDTO> { Success = false, Message = "Error de conexión al reservar." };
            }
        }
        public async Task<List<ReservaDTO>> ObtenerReservasPorUsuarioAsync(int usuarioId)
        {
            try
            {
               
                var response = await _httpClient.GetFromJsonAsync<Result<List<ReservaDTO>>>($"api/Reserva/usuario/{usuarioId}");
                if (response != null && response.Success)
                {
                    return response.Data;
                }
                return new List<ReservaDTO>();
            }
            catch (Exception)
            {
                return new List<ReservaDTO>();
            }
        }
    }
}