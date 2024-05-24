using SchemaGenerator.BaseFactory.Models;
using System.Text;

namespace SchemaGenerator.BaseFactory
{
    public abstract class SchemaGeneratorBase : BaseClass
    {
        protected string? _SQLTemplate;
        public Boolean IsCombineFile;
        public Boolean IsTblHasDbOwnerName;
        public readonly string DBType;
        protected List<TableSummary> _tableSummaries = new();
        protected List<TableSchema> _tableSchemas = new();

        public SchemaGeneratorBase(string dBType)
        {
            this._SQLTemplate = File.ReadAllText($@"{base._templatePath}SQLTemplate.sql");
            this.DBType = dBType;
        }

        public void GeneratorCreateTableDDL(string? path = null, string fileName = "CrtTableDDL.sql")
        {
            if (this.IsCombineFile)
                this.GeneratorSingleFileCrtTableDDL(path, fileName);
            else
                this.GeneratorMutilFileCrtTableDDL(path);
        }

        public void SaveAs(string? path = null, string fileName = "CrtTableDDL.sql", string? sqlTemplate = null)
        {
            if (string.IsNullOrEmpty(path))
                path = $@"{base._basePath}SQLScript\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = $"{path}{fileName}";
            File.WriteAllText(path, sqlTemplate ?? this._SQLTemplate);
        }

         /// <summary>
        /// 設定要產生DDL的table summary 及 detail schema資料集合
        /// </summary>
        /// <param name="tables">產生DDL的table summary資料集合</param>
        /// <param name="schemas">產生DDL的detail schema資料集合</param>
        public void SetData(List<TableSummary> tables, List<TableSchema> schemas, Boolean isCombineFile, Boolean isTblHasDbOwnerName )
        {
            this.IsCombineFile = isCombineFile;
            this.IsTblHasDbOwnerName = isTblHasDbOwnerName;
            this._tableSummaries = tables;
            this._tableSchemas = schemas;
        }

        protected void GeneratorMutilFileCrtTableDDL(string? path = null)
        {
            StringBuilder ddlBuilder = new StringBuilder();
            foreach (TableSummary item in this._tableSummaries)
            {
                ddlBuilder.Clear();
                ddlBuilder.AppendLine(this.GetCreateTableDDLSQLString(item.TableName));
                ddlBuilder.AppendLine();
                ddlBuilder.AppendLine();
                this.SaveAs(path, $"CrtTbl_{item.TableName}.sql", ddlBuilder.ToString());
            }
        }

        protected void GeneratorSingleFileCrtTableDDL(string? path = null, string? fileName = "CrtTableDDL.sql")
        {
            StringBuilder ddlBuilder = new StringBuilder();
            foreach (TableSummary item in this._tableSummaries)
            {
                ddlBuilder.AppendLine(this.GetCreateTableDDLSQLString(item.TableName));
                ddlBuilder.AppendLine();
                ddlBuilder.AppendLine();
            }
            this.SaveAs(path, fileName!, ddlBuilder.ToString());
        }

        protected string GetNullableTag(Boolean flag)
        {
            if (flag)
                return "";
            else
                return "NOT NULL";
        }

        /// <summary>
        /// 取得PK column 
        /// ex: ColA,ColB
        /// </summary>
        /// <param name="schemas">欄位集合</param>
        /// <returns>ex: ColA,ColB</returns>
        protected string GetPKcolumn(List<TableSchema> schemas)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TableSchema item in schemas.FindAll(x => x.PK))
                sb.Append($",{item.ColumnName}");
            if (sb.Length > 0)
                sb.Remove(0, 1);
            return sb.ToString();
        }

        protected string getTableDescriptionDDL(TableSummary table)
        {
            if (!string.IsNullOrEmpty(table.TableDescription))
                return $"{table.TableComment} [{table.TableDescription}]";
            else
                return $"{table.TableComment}";
        }

        protected string getTableName(TableSummary table)
        {
            if (this.IsTblHasDbOwnerName && ( !string.IsNullOrEmpty(table.DataBase) || !string.IsNullOrEmpty(table.Schema) ))
                return $"{table.Schema ?? table.DataBase}.{table.TableName}";
            else
                return table.TableName;
        }

        #region abstract method
        protected abstract string GetCreateTableDDLSQLString(string tableName);

        protected abstract string GetTableCommentDDL(TableSummary table);

        protected abstract string GetColumnCommentDDL(List<TableSchema> schemas);

        #endregion

    }

}
