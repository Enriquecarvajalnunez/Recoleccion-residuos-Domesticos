using System.Globalization;
using System.Text;

namespace WebApi_Recoleccion_residuos_Domesticos.Helpers
{
    // Definimos la clase como estática ya que no necesitamos instanciarla, se utiliza como un contenedor de métodos de ayuda.
    public static class StringHelper
    {
        /// Elimina diacríticos (acentos y otros) de la cadena proporcionada.
        /// <param name="text">El texto a normalizar.</param>
        /// <returns>La versión sin diacríticos del texto.</returns>
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Normaliza a FormD para separar los caracteres base de los diacríticos.
            string normalizedString = text.Normalize(NormalizationForm.FormD); // Normalizamos a FormD para separar los caracteres base de los diacríticos
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            // Normalizamos de nuevo a FormC para obtener la forma compuesta
            return stringBuilder.ToString() // Normalizamos a FormC para obtener la forma compuesta
                                .Normalize(NormalizationForm.FormC) // Normalizamos a FormC para obtener la forma compuesta
                                .Trim() // Trim para eliminar espacios al inicio y al final
                                .ToLowerInvariant() // Convertimos a minúsculas para una comparación más sencilla
                                .Replace(" ", ""); // Reemplazamos los espacios en blanco por nada
        }
    }
}
