using System;
using System.Collections.Generic;
using System.Text.Json;
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
            return default(T);
        }
    }
}