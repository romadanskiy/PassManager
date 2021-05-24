using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using PasswordManager.ViewModels;

namespace PasswordManager
{
    public enum ImportType
    {
        Json,
        Xml
    }
    
    public class Importer
    {
        public ImportType ImportType { get; set; }

        public Importer(ImportType importType)
        {
            ImportType = importType;
        }

        public static Importer CreateImporter(string contentType)
        {
            var importer = contentType switch
            {
                "application/json" => new Importer(ImportType.Json),
                "application/xml" => new Importer(ImportType.Xml),
                "text/xml" => new Importer(ImportType.Xml),
                _ => null
            };
            
            return importer;
        }

        public T ParseContentString<T>(string contentString)
        {
            return ImportType switch
            {
                ImportType.Json => ParseJsonContentString<T>(contentString),
                ImportType.Xml => ParseXmlContentString<T>(contentString),
                _ => default(T)
            };
        }

        private T ParseJsonContentString<T>(string contentString)
        {
            var model = JsonSerializer.Deserialize<T>(contentString);
            return model;
        }

        private T ParseXmlContentString<T>(string contentString)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var streamReader = new StringReader(contentString);
            var model = (T) xmlSerializer.Deserialize(streamReader);
            return model;
        }
    }
}