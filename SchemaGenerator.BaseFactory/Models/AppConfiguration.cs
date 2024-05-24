using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SchemaGenerator.BaseFactory.Models
{

    /// <summary>
    /// Excel 設定
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Excel 檔案路徑
        /// </summary>
        [JsonPropertyOrder(0)]
        [Description("資料來源類型")]
        public string DataSourceType
        {
            get; set;
        }

        /// <summary>
        /// Table 清單 Worksheet Name
        /// </summary>
        [JsonPropertyOrder(2)]
        [Description("Table 清單 Worksheet Name")]
        public string TableSummaryWorksheet
        {
            get; set;
        }

        /// <summary>
        /// Table Schema Worksheet Name
        /// </summary>
        [JsonPropertyOrder(3)]
        [Description("Table Schema Worksheet Name")]
        public string TableSchemaWorksheet
        {
            get; set;
        }

        /// <summary>
        /// Table 設定資訊
        /// </summary>
        [JsonPropertyOrder(4)]
        [Description("Table 設定資訊")]
        public TableConfiguration TableConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Schema 設定資訊
        /// </summary>
        [JsonPropertyOrder(5)]
        [Description("Schema 設定資訊")]
        public SchemaConfiguration SchemaConfiguration
        {
            get; set;
        }
    }


    /// <summary>
    /// Table 清單設定
    /// </summary>
    public class TableConfiguration
    {
        /// <summary>
        /// 資料啟始欄位索引值
        /// </summary>
        [JsonPropertyOrder(99)]
        [Description("資料啟始欄位索引值")]
        public Int32 IndexOfStartRow
        {
            get; set;
        } = 0;

        /// <summary>
        /// 序號的欄位索引值
        /// </summary>
        [JsonPropertyOrder(1)]
        [Description("序號的欄位索引值")]
        public Int32? IndexOfSeq
        {
            get; set;
        }

        /// <summary>
        /// Area的欄位索引值
        /// </summary>
        [JsonPropertyOrder(2)]
        [Description("Area的欄位索引值")]
        public Int32? IndexOfArea
        {
            get; set;
        }

        /// <summary>
        /// SubjectArea的欄位索引值
        /// </summary>
        [JsonPropertyOrder(3)]
        [Description("SubjectArea的欄位索引值")]
        public Int32? IndexOfSubjectArea
        {
            get; set;
        }

        /// <summary>
        /// DataBase的欄位索引值
        /// </summary>
        [JsonPropertyOrder(4)]
        [Description("DataBase的欄位索引值")]
        public Int32? IndexOfDataBase
        {
            get; set;
        }

        /// <summary>
        /// Schema的欄位索引值
        /// </summary>
        [JsonPropertyOrder(5)]
        [Description("Schema的欄位索引值")]
        public Int32? IndexOfSchema
        {
            get; set;
        }

        /// <summary>
        /// Table Name的欄位索引值
        /// </summary>
        [JsonPropertyOrder(6)]
        [Description("Table Name的欄位索引值")]
        public Int32? IndexOfTableName
        {
            get; set;
        }

        /// <summary>
        /// TableComment 的欄位索引值
        /// </summary>
        [JsonPropertyOrder(7)]
        [Description("Table Comment 的欄位索引值")]
        public Int32? IndexOfTableComment
        {
            get; set;
        }

        /// <summary>
        /// Table Name原始英文全名的欄位索引值
        /// </summary>
        [JsonPropertyOrder(8)]
        [Description("Table Name原始英文全名的欄位索引值")]
        public Int32? IndexOfTableOriginalName
        {
            get; set;
        }

        /// <summary>
        /// Table 說明的欄位索引值
        /// </summary>
        [JsonPropertyOrder(9)]
        [Description("Table 說明的欄位索引值")]
        public Int32? IndexOfTableDescription
        {
            get; set;
        }

        [JsonPropertyOrder(10)]
        [Description("Table 額外定義資料")]
        public List<ExtensionData> ExtensionData
        {
            get; set;
        }
    }

    public class SchemaConfiguration
    {
        /// <summary>
        /// 資料啟始欄位索引值
        /// </summary>
        [JsonPropertyOrder(99)]
        [Description("資料啟始欄位索引值")]
        public Int32 IndexOfStartRow
        {
            get; set;
        } = 0;

        /// <summary>
        /// Column Seq的欄位索引值
        /// </summary>
        [JsonPropertyOrder(0)]
        [Description("Column Seq的欄位索引值")]
        public Int32? IndexOfColumnSeq
        {
            get; set;
        }

        /// <summary>
        /// Area的欄位索引值
        /// </summary>
        [JsonPropertyOrder(1)]
        [Description("Area的欄位索引值")]
        public Int32? IndexOfArea
        {
            get; set;
        }

        /// <summary>
        /// SubjectArea的欄位索引值
        /// </summary>
        [JsonPropertyOrder(2)]
        [Description("SubjectArea的欄位索引值")]
        public Int32? IndexOfSubjectArea
        {
            get; set;
        }

        /// <summary>
        /// DataBase的欄位索引值
        /// </summary>
        [JsonPropertyOrder(3)]
        [Description("DataBase的欄位索引值")]
        public Int32? IndexOfDataBase
        {
            get; set;
        }

        /// <summary>
        /// Schema的欄位索引值
        /// </summary>
        [JsonPropertyOrder(4)]
        [Description("Schema的欄位索引值")]
        public Int32? IndexOfSchema
        {
            get; set;
        }

        /// <summary>
        /// Table Name的欄位索引值
        /// </summary>
        [JsonPropertyOrder(5)]
        [Description("Table Name的欄位索引值")]
        public Int32? IndexOfTableName
        {
            get; set;
        }

        /// <summary>
        /// 欄位名稱的欄位索引值
        /// </summary>
        [JsonPropertyOrder(6)]
        [Description("欄位名稱的欄位索引值")]
        public Int32? IndexOfColumnName
        {
            get; set;
        }

        /// <summary>
        /// Data Type - 1的欄位索引值
        /// </summary>
        [JsonPropertyOrder(7)]
        [Description("Data Type - 1的欄位索引值")]
        public Int32? IndexOfDatatype01
        {
            get; set;
        }

        /// <summary>
        /// Data Type - 2的欄位索引值
        /// </summary>
        [JsonPropertyOrder(8)]
        [Description("Data Type - 2的欄位索引值")]
        public Int32? IndexOfDatatype02
        {
            get; set;
        }

        /// <summary>
        /// 欄位說明的欄位索引值
        /// </summary>
        [JsonPropertyOrder(9)]
        [Description("欄位說明的欄位索引值")]
        public Int32? IndexOfColumnDescription
        {
            get; set;
        }


        /// <summary>
        /// Primary Key 的欄位索引值
        /// </summary>
        [JsonPropertyOrder(10)]
        [Description("Primary Key 的欄位索引值")]
        public Int32? IndexOfPK
        {
            get; set;
        }

        /// <summary>
        ///  Foreign Key的欄位索引值
        /// </summary>
        [JsonPropertyOrder(11)]
        [Description("Foreign Key的欄位索引值")]
        public Int32? IndexOfFK
        {
            get; set;
        }

        /// <summary>
        ///   是否可Null的欄位索引值
        /// </summary>
        [JsonPropertyOrder(12)]
        [Description("是否可Null的欄位索引值")]
        public Int32? IndexOfNullable
        {
            get; set;
        }

        /// <summary>
        ///   是否 Not Null的欄位索引值
        /// </summary>
        [JsonPropertyOrder(12)]
        [Description("是否 Not Null的欄位索引值")]
        public Int32? IndexOfNotNull
        {
            get; set;
        }
        /// <summary>
        ///  欄位英文原始全名的欄位索引值
        /// </summary>
        
        
        [JsonPropertyOrder(13)]
        [Description("欄位英文原始全名的欄位索引值")]
        public Int32? IndexOfColumnOriginalName
        {
            get; set;
        }

        [JsonPropertyOrder(10)]
        [Description("Table 額外定義資料")]
        public List<ExtensionData> ExtensionData
        {
            get; set;
        }
    }
}
