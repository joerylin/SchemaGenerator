using CommonLibrary;
using NPOI.SS.UserModel;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.BaseFactory.Services;

namespace SourceReader.SDM
{
    public class SDMSourceReader : SourceReaderBase
    {
        public ExcelService ExcelService => base._excelService;
        public AppConfiguration AppConfigure => base._appConfiguration;

        private IWorkbook _book;


        public SDMSourceReader(AppConfiguration appConfigure, ExcelService excelService) : base(appConfigure, excelService)
        {
            this.TableSummaries = new List<TableSummary>();
            this.TableSchemas = new List<TableSchema>();
            this._book = _excelService.GetWorkBook();
        }

        /// <summary>
        /// 取得資料表欄位Schema 資料
        /// </summary>
        /// <param name="searchTblName">選取取得的資料表集合</param>
        /// <returns></returns>
        public override List<TableSchema> GetTableSchema(List<string> searchTblName)
        {
            searchTblName ??= new List<string>();

            if (this.TableSchemas.Count == 0)
            {
                ISheet sheet = _book.GetSheet(this.AppConfigure.TableSchemaWorksheet);
                TableSchema item;
                // 讀取行和列數據
                for (int i = this.AppConfigure.SchemaConfiguration.IndexOfStartRow; i <= sheet.LastRowNum; i++)
                {
                    IRow currentRow = sheet.GetRow(i);
                    if (currentRow != null)
                    {
                        item = new TableSchema();
                        item.ColumnSeq = DataTrans.CInt32Nullable(_excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfColumnSeq));
                        item.Area = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfArea);
                        item.SubjectArea = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfSubjectArea);
                        item.DataBase = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfDataBase);
                        item.Schema = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfSchema);
                        item.TableName = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfTableName);
                        item.ColumnName = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfColumnName);
                        if (String.IsNullOrEmpty(item.ColumnName))
                            continue;
                        item.DataType = _excelService.GetCellValue(currentRow, this.AppConfigure.SchemaConfiguration.IndexOfDatatype01);
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
                        this.TableSchemas.Add(item);
                    }
                }
            }
            if (searchTblName.Count > 0)
                return this.TableSchemas.Where(x => searchTblName.Contains(x.TableName)).ToList();
            else
                return this.TableSchemas;
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
                        if (string.IsNullOrEmpty(DataTrans.CString(item.TableName)))       //如果資料表名稱都沒有就不加入資料集中
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


    }
}
