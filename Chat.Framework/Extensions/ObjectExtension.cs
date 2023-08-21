using System.Text.Json;

namespace Chat.Framework.Extensions
{
    public static class ObjectExtension
    {
        public static string Serialize(this object? obj)
        {
            if (obj is null) return string.Empty;
            try
            {
                var jsonText = JsonSerializer.Serialize(obj);
                return jsonText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
    }
}