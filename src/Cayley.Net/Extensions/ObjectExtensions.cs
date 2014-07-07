using System.IO;
using Newtonsoft.Json;

namespace Cayley.Net.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToEmitString(this object data)
        {
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            var writer = new JsonTextWriter(stringWriter) { QuoteName = false };
            serializer.Serialize(writer, data);
            writer.Close();
            return stringWriter.ToString();
        }
    }
}
