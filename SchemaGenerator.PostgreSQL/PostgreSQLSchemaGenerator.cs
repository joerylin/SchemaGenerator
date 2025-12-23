using NPOI.SS.Formula.Functions;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using System.Text;

namespace SchemaGenerator.PostgreSQL
{
    public class PostgreSQLSchemaGenerator(string dBType) : SchemaGeneratorBase(dBType)
    {
        private readonly Dictionary<string, string> _typeMapping = new(StringComparer.OrdinalIgnoreCase)
        {
            { "string", "text" },
            { "varchar", "varchar" },
            { "nvarchar", "varchar" },
            { "int", "integer" },
            { "bigint", "bigint" },
            { "smallint", "smallint" },
            { "decimal", "numeric" },
            { "numeric", "numeric" },
            { "double", "double precision" },
            { "float", "double precision" },
            { "datetime", "timestamp without time zone" },
            { "timestamp", "timestamp without time zone" },
            { "bool", "boolean" },
            { "boolean", "boolean" },
            { "guid", "uuid" }
        };

        protected override string GetCreateTableDDLSQLString(string tableName)
        {
            TableSummary table = base._tableSummaries.Find(x => x.TableName == tableName)!;
            List<TableSchema> schemas = base._tableSchemas.FindAll(x => x.TableName == tableName)!.OrderBy(x => x.ColumnSeq).ToList();

            string SQLstring = base._SQLTemplate!
                .Replace("#CreateTable#", "CREATE TABLE")
                .Replace("#ExecutionSign#", ";")
                .Replace("#TableName#", base.getTableName(table))
                .Replace("#TableDescription#", base.getTableDescriptionDDL(table));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < schemas.Count; i++)
            {
                var col = schemas[i];
                var pgType = MapType(col.DataType);
                string nullTag = base.GetNullableTag(col.Nullable);
                string defaultTag = GetDefaultTag(col);
                string colDef = $"{col.ColumnName.PadRight(30)}\t{pgType.PadRight(30)}\t{nullTag.PadRight(10)}\t{defaultTag} {GetColumnInlineComment(col)}".Trim();

                if (i == 0)
                    builder.Append($"\t\t {colDef}");
                else
                    builder.Append($"\r\n\t\t,{colDef}");
            }

            if (schemas.FindAll(x => x.PK).Count > 0)
                builder.Append($"\r\n\t\t,PRIMARY KEY({base.GetPKcolumn(schemas.FindAll(x => x.PK))})");

            SQLstring = SQLstring.Replace("#ColumnList#", builder.ToString());
            SQLstring = SQLstring.Replace("#TableOption#", string.Empty);
            SQLstring = SQLstring.Replace("#TableComment#", this.GetTableCommentDDL(table));
            SQLstring = SQLstring.Replace("#ColumnComment#", this.GetColumnCommentDDL(schemas));

            return SQLstring;
        }

        private string MapType(string? source)
        {
            if (string.IsNullOrEmpty(source)) return "text";
            var key = source.Trim();
            // try exact match
            if (_typeMapping.TryGetValue(key, out var mapped)) return mapped;
            // try remove length e.g. varchar(50)
            var idx = key.IndexOf('(');
            if (idx > 0)
            {
                var baseType = key.Substring(0, idx);
                if (_typeMapping.TryGetValue(baseType, out var m2)) return m2 + key.Substring(idx);
            }
            return key; // fallback
        }

        private string GetDefaultTag(TableSchema col)
        {
            // look for ExtensionData named Default or DefaultValue
            var def = col.ExtensionData?.FirstOrDefault(x => string.Equals(x.Key, "Default", StringComparison.OrdinalIgnoreCase) || string.Equals(x.Key, "DefaultValue", StringComparison.OrdinalIgnoreCase))?.Value;
            if (!string.IsNullOrEmpty(def))
                return $"DEFAULT {def}";
            return string.Empty;
        }

        protected override string GetTableCommentDDL(TableSummary table)
        {
            if (!string.IsNullOrEmpty(table.TableComment))
                return $"\r\n\r\nCOMMENT ON TABLE {table.TableName} IS '{table.TableComment}';";
            return string.Empty;
        }

        protected override string GetColumnCommentDDL(List<TableSchema> schemas)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var col in schemas)
            {
                if (!string.IsNullOrEmpty(col.ColumnDescription))           
                    sb.Append($"\r\nCOMMENT ON COLUMN {col.TableName}.{col.ColumnName} IS '{col.ColumnDescription}';");            
            }
            if (sb.Length > 0)
                sb.Insert(0, "\r\n");
            return sb.ToString();
        }

        private string GetColumnInlineComment(TableSchema col)
        {
            if (!string.IsNullOrEmpty(col.ColumnDescription))
                return $"-- {col.ColumnDescription}";
            return string.Empty;
        }
    }
}
