using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using System.Text;

namespace SchemaGenerator.MSSQL
{
    public class MSSQLSchemaGenerator(string dBType) : SchemaGeneratorBase(dBType)
    {
        protected override string GetCreateTableDDLSQLString(string tableName)
        {
            TableSummary table = base._tableSummaries.Find(x => x.TableName == tableName)!;
            List<TableSchema> schemas = base._tableSchemas.FindAll(x => x.TableName == tableName)!.OrderBy(x => x.ColumnSeq).ToList();

            string SQLstring = base._SQLTemplate!
                .Replace("#CreateTable#", "CREATE TABLE")
                .Replace("#ExecutionSign#", "GO")      //SQL 段落執行指令
                .Replace("#TableName#", base.getTableName(table))
                .Replace("#TableDescription#", base.getTableDescriptionDDL(table));

            //Process column 
            schemas = schemas.OrderBy(x => x.ColumnSeq).ToList();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < schemas.Count; i++)
            {
                if (i == 0)
                    builder.Append($"\t\t {schemas[i].ColumnName.PadRight(30)}\t{schemas[i].DataType.PadRight(30)}\t{base.GetNullableTag(schemas[i].Nullable)}");
                else
                    builder.Append($"\r\n\t\t,{schemas[i].ColumnName.PadRight(30)}\t{schemas[i].DataType.PadRight(30)}\t{base.GetNullableTag(schemas[i].Nullable)}");
            }

            //Process column - PK
            if (schemas.FindAll(x => x.PK).Count > 0)
                builder.Append($"\r\n\t\t,CONSTRAINT PK_{table.TableName} PRIMARY KEY({base.GetPKcolumn(schemas.FindAll(x => x.PK))})");
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
            {
                //因配合Sql template及產出格式要求故多空二行開始
                return @$"

EXEC sys.sp_addextendedproperty 
    @name=N'TableDescription', 
    @value=N'{table.TableComment}',
    @level0type=N'SCHEMA',
    @level0name=N'dbo', 
    @level1type=N'TABLE',
    @level1name=N'{table.TableName}'
GO";
            }
            else
                return String.Empty;

        }

        protected override string GetColumnCommentDDL(List<TableSchema> schemas)
        {
            StringBuilder builder = new StringBuilder();
            foreach (TableSchema item in schemas)
            {
                if (!String.IsNullOrEmpty(item.ColumnDescription))
                {
                    builder.Append(@$"
EXEC sys.sp_addextendedproperty 
    @name=N'MS_Description', 
    @value=N'{item.ColumnDescription}',
    @level0type=N'SCHEMA',
    @level0name=N'dbo', 
    @level1type=N'TABLE',
    @level1name=N'{item.TableName}',
    @level2type=N'COLUMN',
    @level2name=N'{item.ColumnName}'
GO");
                }
            }
            if (builder.Length > 0)
                builder.Insert(0, "\r\n");
            return builder.ToString();
        }
    }
}
