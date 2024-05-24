using SchemaGenerator.BaseFactory.Models;

namespace NUnitTestDDLGenerator.SchemaGenerator
{
    internal class DataService
    {
        public static List<TableSummary> GetSourceTableSummaryData()
        {
            List<TableSummary> tables = new List<TableSummary>();
            tables.Add(new TableSummary() { TableName = "mis_catg", TableComment = "代碼資料-(分類主檔)", TableDescription = "Code Table 共用代碼主檔-企業若自訂資料，則抓企業自訂的資料" , DataBase="MyTest"});
            tables.Add(new TableSummary() { TableName = "mis_item", TableComment = "代碼資料-(項目明細)", TableDescription = "Code Table 共用代碼明細檔企業若自訂資料，則抓企業自訂的資料" , DataBase = "MyTest" , Schema="sys"});
            tables.Add(new TableSummary() { TableName = "news", TableComment = "訊息檔" });
            tables.Add(new TableSummary() { TableName = "todolist" });
            return tables;
        }
        public static List<TableSchema> GetSourceTableSchemaData()
        {
            List<TableSchema> schemas = new List<TableSchema>();
            schemas.Add(new TableSchema() { TableName = "mis_catg", ColumnName = "bus_no", ColumnDescription = "企業編號", ColumnSeq = 1, PK = true, DataType = "VARCHAR(20)" });
            schemas.Add(new TableSchema() { TableName = "mis_catg", ColumnName = "catg_cd", ColumnDescription = "大類代碼", ColumnSeq = 2, PK = true, DataType = "VARCHAR(5)" });
            schemas.Add(new TableSchema() { TableName = "mis_catg", ColumnName = "catg_desc", ColumnDescription = "大類說明", ColumnSeq = 3, DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "mis_catg", ColumnName = "enbl_ind", ColumnDescription = "是否啟用", ColumnSeq = 4, DataType = "CHAR(1)" });

            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "bus_no", ColumnDescription = "企業編號", ColumnSeq = 1, PK = true, DataType = "VARCHAR(20)" });
            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "catg_cd", ColumnDescription = "大類代碼", ColumnSeq = 2, PK = true, DataType = "VARCHAR(5)" });
            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "item_cd", ColumnDescription = "項目代碼", ColumnSeq = 3, PK = true, DataType = "VARCHAR(5)" });
            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "item_desc", ColumnDescription = "項目說明", ColumnSeq = 4, DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "item_desc_o1", ColumnDescription = "項目說明1", ColumnSeq = 5, DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "item_desc_o2", ColumnDescription = "項目說明2", ColumnSeq = 6, DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "mis_item", ColumnName = "enbl_ind", ColumnDescription = "是否啟用", ColumnSeq = 7, DataType = "CHAR(1)" });

            schemas.Add(new TableSchema() { TableName = "news", ColumnName = "bus_no", ColumnDescription = "企業編號", ColumnSeq = 1, PK = true, DataType = "VARCHAR(20)" });
            schemas.Add(new TableSchema() { TableName = "news", ColumnName = "news_no", ColumnDescription = "訊息編號", ColumnSeq = 2, PK = true, DataType = "VARCHAR(20)" });
            schemas.Add(new TableSchema() { TableName = "news", ColumnName = "title", ColumnDescription = "標題", ColumnSeq = 3, DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "news", ColumnName = "msg", ColumnDescription = "內容", ColumnSeq = 4, DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "news", ColumnName = "msg_catg", ColumnDescription = "訊息類別", ColumnSeq = 5, DataType = "VARCHAR(5)" });
            schemas.Add(new TableSchema() { TableName = "news", ColumnName = "enbl_ind", ColumnDescription = "是否啟用", ColumnSeq = 6, DataType = "CHAR(1)" });

            schemas.Add(new TableSchema() { TableName = "todolist", ColumnName = "msg", DataType = "VARCHAR(50)" });
            schemas.Add(new TableSchema() { TableName = "todolist", ColumnName = "enbl_ind", ColumnDescription = "是否啟用",  DataType = "CHAR(1)" });
            schemas.Add(new TableSchema() { TableName = "todolist", ColumnName = "msg_catg", DataType = "VARCHAR(5)" });
           
            return schemas;
        }
    }
}
