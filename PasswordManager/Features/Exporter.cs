using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Features
{
    public enum ExportType
    {
        Json,
        Xml
    }
    
    public class Exporter
    {
        public ExportType ExportType { get; set; }

        public Exporter(ExportType exportType)
        {
            ExportType = exportType;
        }

        public string GenerateString<T>(T obj)
        {
            return ExportType switch
            {
                ExportType.Json => GenerateJsonString(obj),
                ExportType.Xml => GenerateXmlString(obj),
                _ => null
            };
        }

        private string GenerateJsonString<T>(T obj)
        {
            var options = new JsonSerializerOptions {WriteIndented = true};
            var result = JsonSerializer.Serialize(obj, options);
            
            return result;
        }
        
        private string GenerateXmlString<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            using var stringWriter = new StringWriter();
            using var xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
            xmlSerializer.Serialize(xmlWriter, obj);
            var xml = stringWriter.ToString();

            return xml;
        }
    }
}