using CommonLibrary;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using System.Text;

namespace SchemaGenerator.POCOModel
{
    public class POCOModelSchemaGenerator(string dBType) : SchemaGeneratorBase(dBType)
    {

        public override void GeneratorCreateTableDDL(string? path = null, string fileName = "POCOModel.cs")
        {
            if (this.IsCombineFile)
                this.GeneratorSingleFileCrtTableDDL(path, fileName);
            else
                this.GeneratorMutilFileCrtTableDDL(path);
        }

        protected override void GeneratorMutilFileCrtTableDDL(string? path)
        {
            StringBuilder ddlBuilder = new StringBuilder();
            foreach (TableSummary item in this._tableSummaries)
            {
                ddlBuilder.Clear();
                ddlBuilder.AppendLine("namespace YourProjectNameSpace;");
                ddlBuilder.AppendLine();
                ddlBuilder.AppendLine(this.GetCreateTableDDLSQLString(item.TableName));
                ddlBuilder.AppendLine();
                ddlBuilder.AppendLine();
                this.SaveAs(path, $"{item.TableName}.cs", ddlBuilder.ToString());
            }
        }

        protected override void GeneratorSingleFileCrtTableDDL(string? path = null, string? fileName = "CrtTableDDL.sql")
        {
            StringBuilder ddlBuilder = new StringBuilder();
            ddlBuilder.AppendLine("namespace YourProjectNameSpace;");
            ddlBuilder.AppendLine();
            foreach (TableSummary item in this._tableSummaries)
            {
                ddlBuilder.AppendLine(this.GetCreateTableDDLSQLString(item.TableName));
                ddlBuilder.AppendLine();
                ddlBuilder.AppendLine();
            }
            this.SaveAs(path, fileName!, ddlBuilder.ToString());
        }


        protected override string GetCreateTableDDLSQLString(string tableName)
        {
            TableSummary table = base._tableSummaries.Find(x => x.TableName == tableName)!;
            List<TableSchema> schemas = base._tableSchemas.FindAll(x => x.TableName == tableName)!.OrderBy(x => x.ColumnSeq).ToList();

            string textScript = base.GetTemplateScript("POCOTemplate.cs")
                .Replace("#Namespace#", "YourProjectNameSpace")
                .Replace("#TableName#", base.getTableName(table));

            //Process column 
            StringBuilder builder = new StringBuilder();
            schemas = schemas.OrderBy(x => x.ColumnSeq).ToList();
            foreach (TableSchema item in schemas)
            {
                builder.AppendLine($"\t/// <summary>");
                builder.AppendLine($"\t/// {item.ColumnDescription}");
                if (!string.IsNullOrEmpty(item.ColumnOriginalName))
                    builder.AppendLine($"\t/// {item.ColumnOriginalName}");
                builder.AppendLine($"\t/// </summary>");
                string dbtype = DataTrans.ConvertToCSharpType(item.DataType, DataTrans.enmDbTypeMapCatg.MSSQL);
                builder.AppendLine($"\tpublic {dbtype} {item.ColumnName} {{ get; set; }}\n");
            }

            textScript = textScript.Replace("#ColumnList#", builder.ToString());

            return textScript;
        }



        protected override string GetTableCommentDDL(TableSummary table) => String.Empty;   //POCOModel 不需要此方法

        protected override string GetColumnCommentDDL(List<TableSchema> schemas) => String.Empty;   //POCOModel 不需要此方法

    }
}
