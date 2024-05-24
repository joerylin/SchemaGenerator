using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.BaseFactory.Services;

namespace SchemaGenerator.BaseFactory
{
    public abstract class SourceReaderBase : BaseClass
    {
        public readonly AppConfiguration _appConfiguration;
        protected readonly ExcelService _excelService;
        public List<TableSummary> TableSummaries;
        public List<TableSchema> TableSchemas;

        public SourceReaderBase(AppConfiguration appconfig, ExcelService excelService = null)
        {
            this._appConfiguration = appconfig;
            this._excelService = excelService;
        }


        /// <summary>
        /// 取得Table Summary資料
        /// </summary>
        /// <returns>Table Summary資料</returns>
        public abstract List<TableSummary> GetTableSummaries();

        /// <summary>
        /// 取得Table Schema資料
        /// </summary>
        /// <param name="searchTblName">選取取得的資料表集合</param>
        /// <returns>Table Schema資料</returns>

        public abstract List<TableSchema> GetTableSchema(List<string> searchTblName = null);

    }

}
