namespace GourmetGo.Web.Models
{
    public class Result<T>
    {
        // esta clase es exclusiva para que el frontend lea las respuestas de la API.

        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

    }
}