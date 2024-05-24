using System.Text.Json.Serialization;

namespace TableSchemaGenerator.Model
{
    public class AppData
    {
        [JsonPropertyOrder(1)]
        public List<DataItem> SourceTypes
        {
            get; set;
        }
        [JsonPropertyOrder(2)]
        public List<DataItem> DatabaseTypes
        {
            get; set;
        }

        [JsonPropertyOrder(3)]
        public string DefaultDestinatinalPath
        {
            get; set;
        }
    }

    public class DataItem
    {
        public DataItem()
        {
        }
        public DataItem(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        [JsonPropertyOrder(1)]
        public string Type
        {
            get; set;
        }
        [JsonPropertyOrder(2)]
        public string Name
        {
            get; set;
        }
    }


}
