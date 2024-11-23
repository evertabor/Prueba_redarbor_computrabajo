using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitectrure.WebApi.Converters
{
    /// <summary>
    /// Clase personalizada para convertir objetos DateTime a formato JSON y viceversa.
    /// Se utilizan varios formatos de fecha para asegurar la correcta deserialización desde JSON.
    /// </summary>
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string[] _formats =
        {
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd"
         };

        /// <summary>
        /// Lee una fecha desde el JSON y la convierte a un objeto DateTime.
        /// Intenta parsear la fecha utilizando los formatos definidos en _formats.
        /// </summary>
        /// <param name="reader">El lector de JSON que contiene el valor de fecha.</param>
        /// <param name="typeToConvert">El tipo al que se desea convertir (en este caso, DateTime).</param>
        /// <param name="options">Opciones de configuración de la serialización.</param>
        /// <returns>El valor de fecha como DateTime.</returns>
        /// <exception cref="JsonException">Lanzada si el valor no puede ser convertido a una fecha válida.</exception>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, out var date))
            {
                return date;
            }

            throw new JsonException($"Fecha inválida: {value}");
        }

        /// <summary>
        /// Escribe un objeto DateTime a JSON en un formato estándar.
        /// </summary>
        /// <param name="writer">El escritor de JSON que recibirá el valor de fecha.</param>
        /// <param name="value">El objeto DateTime a serializar.</param>
        /// <param name="options">Opciones de configuración de la serialización.</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }

}
