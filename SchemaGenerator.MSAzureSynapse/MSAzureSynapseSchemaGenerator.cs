using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using System.Text;

namespace SchemaGenerator.MSAzureSynapse
{
    public class MSAzureSynapseSchemaGenerator(string dBType) : SchemaGeneratorBase(dBType)
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
                    builder.Append($"\t\t {schemas[i].ColumnName.PadRight(30)}\t{schemas[i].DataType.PadRight(30)}\t{base.GetNullableTag(schemas[i].Nullable)}");
                else
                    builder.Append($"\r\n\t\t,{schemas[i].ColumnName.PadRight(30)}\t{schemas[i].DataType.PadRight(30)}\t{base.GetNullableTag(schemas[i].Nullable)}");
            }
            SQLstring = SQLstring.Replace("#ColumnList#", builder.ToString());

            //Process TableOption
            SQLstring = SQLstring.Replace("#TableOption#", this.getTableOption(schemas));

            //Process Table Comment
            SQLstring = SQLstring.Replace("#TableComment#", this.GetTableCommentDDL(table));

            //Process Column Comment
            SQLstring = SQLstring.Replace("#ColumnComment#", this.GetColumnCommentDDL(schemas));

            return SQLstring;
        }

        protected override string GetTableCommentDDL(TableSummary table)
        {
            // MS Synapse SQL Database type, dose not support Table comment ! 
            return "";
        }

        protected override string GetColumnCommentDDL(List<TableSchema> schemas)
        {
            //MS Synapse SQL Database type, dose not support Column comment！
            return "";
        }

        protected string getTableOption(List<TableSchema> schemas)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("WITH(");
            sb.AppendLine("\t\t CLUSTERED COLUMNSTORE INDEX");
            if (schemas.FindAll(x => x.PK).Count() > 0)
                sb.AppendLine($"\t\t,DISTRIBUTION = HASH({base.GetPKcolumn(schemas.FindAll(x => x.PK))})");
            else
                sb.AppendLine("\t\t,DISTRIBUTION = ROUND_ROBIN");
            sb.Append(")");

            return sb.ToString();
        }
    }
}
