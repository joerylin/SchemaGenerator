namespace SchemaGenerator.BaseFactory.Models
{
    public class TableSummary
    {
        public Int32 Seq
        {
            get; set;
        }

        public string? Area
        {
            get; set;
        }
        public string? SubjectArea
        {
            get; set;
        }
        public string? DataBase
        {
            get; set;
        }
        public string? Schema
        {
            get; set;
        }

        public string TableName
        {
            get; set;
        }

        public string TableComment
        {
            get; set;
        }

        public string? TableOriginalName
        {
            get; set;
        }
        public string? TableDescription
        {
            get; set;
        }
        public Boolean IsChecked
        {
            get; set;
        } = false; 
        
        public List<ExtensionData> ExtensionData
        {
            get; set;
        }
    }
}
