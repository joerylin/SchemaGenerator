using CommonLibrary;
using NPOI.SS.UserModel;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.BaseFactory.Services;

namespace SourceReader.TableSchema
{
    public class TableSchemaSourceReader : SourceReaderBase
    {
        public ExcelService ExcelService => base._excelService;
        public AppConfiguration AppConfigure => base._appConfiguration;

        private IWorkbook _book;
        public TableSchemaSourceReader(AppConfiguration appconfig, ExcelService excelService = null) : base(appconfig, excelService)
        {
            this.TableSummaries = new List<SchemaGenerator.BaseFactory.Models.TableSummary>();
            this.TableSchemas = new List<SchemaGenerator.BaseFactory.Models.TableSchema>();
            this._book = _excelService.GetWorkBook();
        }

        public override List<SchemaGenerator.BaseFactory.Models.TableSchema> GetTableSchema(List<string> searchTblName = null)
        {
            searchTblName ??= new List<string>();
            base.TableSchemas.Clear();
            TableSummary table;
            foreach (string tblName in searchTblName)
            {
                table = base.TableSummaries.Find(x => x.TableName == tblName)!;
                base.TableSchemas.AddRange(this.GetSingleTableSchema(tblName, table));
            }

            return base.TableSchemas;
        }

        public override List<TableSummary> GetTableSummaries()
        {
            if (TableSummaries.Count == 0)
            {
                ISheet sheet = _book.GetSheet(this.AppConfigure.TableSummaryWorksheet);
                TableSummary item;
                // 讀取行和列數據
                for (int i = this.AppConfigure.TableConfiguration.IndexOfStartRow; i <= sheet.LastRowNum; i++)
                {
                    IRow currentRow = sheet.GetRow(i);
                    if (currentRow != null)
                    {
                        item = new TableSummary();
                        item.Area = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfArea);
                        item.SubjectArea = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfSubjectArea);
                        item.DataBase = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfDataBase);
                        item.Schema = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfSchema);
                        item.TableName = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfTableName);
                        if (string.IsNullOrEmpty(DataTrans.CString(item.TableName)))    //如果資料表名稱都沒有就不加入資料集中
                            continue;
                        item.TableComment = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfTableComment);
                        item.TableOriginalName = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfTableOriginalName);
                        item.TableDescription = _excelService.GetCellValue(currentRow, this.AppConfigure.TableConfiguration.IndexOfTableDescription);

                        //處理其他額外的資料 ExtionData
                        if (this.AppConfigure.TableConfiguration.ExtensionData != null)
                        {
                            item.ExtensionData = new List<ExtensionData>();
                            foreach (ExtensionData extConfig in this.AppConfigure.TableConfiguration.ExtensionData)
                            {
                                item.ExtensionData.Add(new ExtensionData
                                {
                                    Key = extConfig.Key,
                                    Name = extConfig.Name,
                                    IndexOfColumn = extConfig.IndexOfColumn,
                                    Value = _excelService.GetCellValue(currentRow, extConfig.IndexOfColumn)
                                });
                            }
                        }
                        this.TableSummaries.Add(item);
                    }
                }
            }
            return this.TableSummaries;
        }


        private List<SchemaGenerator.BaseFactory.Models.TableSchema> GetSingleTableSchema(string tblWorksheetName, TableSummary table)
        {
            List<SchemaGenerator.BaseFactory.Models.TableSchema> schemas = new List<SchemaGenerator.BaseFactory.Models.TableSchema>();

            ISheet sheet = _book.GetSheet(tblWorksheetName);
            SchemaGenerator.BaseFactory.Models.TableSchema item;
            // 讀取行和列數據
            for (int i = this.AppConfigure.SchemaConfiguration.IndexOfStartRow; i <= sheet.LastRowNum; i++)
            {
                IRow currentRow = sheet.GetRow(i);
                if (currentRow != null)
                {
                    item = new SchemaGenerator.BaseFactory.Models.TableSchema();
                    item.ColumnSeq = DataTrans.CInt32Nullable(_excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfColumnSeq));
                    item.Area = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfArea);
                    item.SubjectArea = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfSubjectArea);
                    item.DataBase = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfDataBase);
                    item.Schema = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfSchema);
                    item.TableName = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfTableName);
                    // TableName 必須有值，會用來搜尋對應的Table Schema，所以若原本來源沒有Table 帶入參數資料表名
                    item.TableName = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfTableName) ?? tblWorksheetName;
                    item.ColumnName = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfColumnName);
                    if (String.IsNullOrEmpty(item.ColumnName))
                        continue;
                    item.DataType = this.getDataType(
                         _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfDatatype01)
                        , _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfDatatype02));
                    item.ColumnDescription = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfColumnDescription);
                    item.ColumnOriginalName = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfColumnOriginalName);
                    item.PK = !string.IsNullOrEmpty(_excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfPK));
                    item.FK = !string.IsNullOrEmpty(_excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfFK));
                    item.Nullable = _excelService.GetNullable(
                            _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfNullable)
                            , _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfNotNull));

                    //處理其他額外的資料 ExtionData
                    if (this.AppConfigure.SchemaConfiguration.ExtensionData != null)
                    {
                        item.ExtensionData = new List<ExtensionData>();
                        foreach (ExtensionData extConfig in this.AppConfigure.SchemaConfiguration.ExtensionData)
                        {
                            item.ExtensionData.Add(new ExtensionData
                            {
                                Key = extConfig.Key,
                                Name = extConfig.Name,
                                IndexOfColumn = extConfig.IndexOfColumn,
                                Value = _excelService.GetCellValue(currentRow, extConfig.IndexOfColumn)
                            });
                        }
                    }
                    schemas.Add(item);
                }
            }
            return schemas;
        }

        private string getDataType(string dataType1, string datatype2)
        {
            string dataType = dataType1;
            if (!String.IsNullOrEmpty(datatype2))
            {
                switch (DataTrans.CUpperString(dataType1))
                {
                    case "NUMBER":
                    case "DECIMAL":
                    case "CHAR":
                    case "VARCHAR":
                    case "VARCHAR2":
                    case "NVARCHAR":
                    case "TIMESTAMP":
                        dataType = $"{DataTrans.CUpperString(dataType1)}({datatype2})";
                        break;
                }
            }
            return dataType;
        }
    }
}
