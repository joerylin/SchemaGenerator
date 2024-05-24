namespace SchemaGenerator.BaseFactory.Models
{
    public class TableSchema
    {

        public Int32? ColumnSeq
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

        public string ColumnName

        {
            get; set;
        }

        public string DataType

        {
            get; set;
        }
        public string? ColumnDescription
        {
            get; set;
        }
        public Boolean PK
        {
            get; set;
        }
        public Boolean FK
        {
            get; set;
        }
        public Boolean Nullable
        {
            get; set;
        }
        public string? ColumnOriginalName
        {
            get; set;
        }

        public List<ExtensionData> ExtensionData
        {
            get; set;
        }
    }
}
