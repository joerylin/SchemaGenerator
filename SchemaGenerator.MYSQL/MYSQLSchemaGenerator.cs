using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using System.Text;

namespace SchemaGenerator.MYSQL
{
    public class MYSQLSchemaGenerator(string dBType) : SchemaGeneratorBase(dBType)
    {
        protected override string GetCreateTableDDLSQLString(string tableName)
        {
            TableSummary table = base._tableSummaries.Find(x => x.TableName == tableName)!;
            List<TableSchema> schemas = base._tableSchemas.FindAll(x => x.TableName == tableName)!.OrderBy(x => x.ColumnSeq).ToList();

            string SQLstring = base._SQLTemplate!
                .Replace("#CreateTable#", "CREATE TABLE")
                .Replace("#ExecutionSign#", ";")      //SQL 段落執行指令
                .Replace("#TableName#", base.getTableName(table))
                .Replace("#TableDescription#", base.getTableDescriptionDDL(table));

            //Process column 
            schemas = schemas.OrderBy(x => x.ColumnSeq).ToList();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < schemas.Count; i++)
            {
                if (i == 0)
                    builder.Append($"\t\t {schemas[i].ColumnName.PadRight(30)}\t{schemas[i].DataType.PadRight(30)}\t{base.GetNullableTag(schemas[i].Nullable).PadRight(10)}\t{this.getColumnDescriptionDDL(schemas[i])}");
                else
                    builder.Append($"\r\n\t\t,{schemas[i].ColumnName.PadRight(30)}\t{schemas[i].DataType.PadRight(30)}\t{base.GetNullableTag(schemas[i].Nullable).PadRight(10)}\t{this.getColumnDescriptionDDL(schemas[i])}");
            }
            //Process column - PK
            if (schemas.FindAll(x => x.PK).Count > 0)
                builder.Append($"\r\n\t\t,PRIMARY KEY({base.GetPKcolumn(schemas.FindAll(x => x.PK))})");

            SQLstring = SQLstring.Replace("#ColumnList#", builder.ToString());

            //Process TableOption
            SQLstring = SQLstring.Replace("#TableOption#", "");    //目前無Table Option

            //Process Table Comment
            SQLstring = SQLstring.Replace("#TableComment#", this.GetTableCommentDDL(table));

            //Process Column Comment
            SQLstring = SQLstring.Replace("#ColumnComment#", this.GetColumnCommentDDL(schemas));

            return SQLstring;
        }

        protected override string GetTableCommentDDL(TableSummary table)
        {
            if (!string.IsNullOrEmpty(table.TableComment))
                return $"\r\n\r\nCOMMENT ON TABLE {table.TableName} IS '{table.TableComment}';";
            else
                return String.Empty;
        }

        protected override string GetColumnCommentDDL(List<TableSchema> schemas)
        {
            //My SQL Database type, has not use this function !
            //因My SQL Column comment 已在 產 Column時處理，故在此替換成空白即可
            return "";
        }

        private string getColumnDescriptionDDL(TableSchema table)
        {
            string ddlString = string.Empty;
            if (!String.IsNullOrEmpty(table.ColumnDescription))
                ddlString = $"COMMENT\t'{table.ColumnDescription}'";
            return ddlString;
        }
   
    }
}
