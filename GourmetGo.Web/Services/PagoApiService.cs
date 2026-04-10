using System.Net.Http.Json;
using GourmetGo.Web.Models;

namespace GourmetGo.Web.Services
{
    public class PagoApiService
    {
        private readonly HttpClient _httpClient;

        public PagoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<PagoDTO>> RegistrarPagoAsync(CreatePagoDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Pago", dto);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<Result<PagoDTO>>();
                    return resultado ?? new Result<PagoDTO> { Success = false, Message = "Respuesta vacía del servidor." };
                }

                var error = await response.Content.ReadAsStringAsync();
                
                return new Result<PagoDTO>
                {
                    Success = false,
                    Message = $"Error en el servidor: {error}"
                };
            }
            catch (Exception ex)
            {
                return new Result<PagoDTO>
                {
                    Success = false,
                    Message = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<Result<PagoDTO>> ObtenerPagoPorOrdenAsync(int ordenId)
        {
            try
            {
                var resultado = await _httpClient.GetFromJsonAsync<Result<PagoDTO>>($"api/Pago/orden/{ordenId}");
                return resultado ?? new Result<PagoDTO> { Success = false, Message = "No se encontraron datos." };
            }
            catch (Exception)
            {
                
                return new Result<PagoDTO>
                {
                    Success = false,
                    Message = "No se pudo obtener el estado del pago."
                };
            }
        }
    }
}