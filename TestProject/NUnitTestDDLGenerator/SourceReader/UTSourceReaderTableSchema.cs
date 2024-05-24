using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.BaseFactory.Services;
using SourceReader.TableSchema;

namespace TestProject1.SourceReader
{

    [TestFixture]
    public class UTSourceReaderTableSchema        :NUnitBase
    {
        private readonly string _filePath = "";
        private readonly string _docType = "TableSchema";


        [TestCase("SDM", "TableSchema.xls"), Description("測試建立 Instance 是否成功")]
        [Order(0)]
        public void Constructor_in2paras_InstanceNotNull(string doctype, string fileFullPath)
        {
            //Arrange:初始化                            
            TableSchemaSourceReader documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new TableSchemaSourceReader(new SourceReaderFactory(doctype).GetAppConfiguration(doctype), new ExcelService(fileFullPath));

            //Assert: 驗證
            Assert.NotNull(documentReader);
            Assert.NotNull(documentReader.AppConfigure);
            Assert.NotNull(documentReader.ExcelService);
        }

        [TestCase("TableSchema", "TableSchema.xls")]
        public void Constructor_in2parasByFactory_InstanceNotNull(string doctype, string fileFullPath)
        {
            //Arrange:初始化                            
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);

            //Assert: 驗證
            Assert.NotNull(documentReader);
        }

        [TestCase("TableSchema", "TableSchema.xls", 11)]
        public void GetTableSummaries_GetWorkSheet_RowCount(string doctype, string fileFullPath, Int32 exRecordCnt)
        {
            //Arrange:初始化
            Int32 recordCnt;
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            recordCnt = documentReader.GetTableSummaries().Count();

            //Assert: 驗證
            Assert.AreEqual(exRecordCnt, recordCnt);
        }

        [TestCase("TableSchema", "TableSchema.xls", 3, "STA_VAT_ACCT_DTL_DE", "帳戶明細-折溢攤")]
        [TestCase("TableSchema", "TableSchema.xls", 5, "STA_VAT_TXN_CL", "交易資訊-CL模組")]
        [TestCase("TableSchema", "TableSchema.xls", 9, "STA_VAT_DETB_TELLER", "TELLER交易資訊")]
        [TestCase("TableSchema", "TableSchema.xls", 10, "STA_VAT_SRC_ACCT", "SRC 資料表")]
        public void GetTableSummaries_GetWorkSheet_CheckData(string doctype, string fileFullPath, Int32 rowIndex, string expTableName, string expTableDesc)
        {
            //Arrange:初始化        
            SourceReaderBase documentReader;
            TableSummary table;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSummaries()[rowIndex];

            //Assert: 驗證
            Assert.NotNull(documentReader);
            Console.WriteLine($"documentReader Not Null >{documentReader.ToString()}");
        
            Console.WriteLine($"expect Table Name={expTableName}，acture Table Name={table.TableName}");
            Assert.AreEqual(expTableName, table.TableName);
                
            Console.WriteLine($"expect expTableOriginalName ={expTableDesc}，acture Table Name={table.TableComment}");
            Assert.AreEqual(expTableDesc, table.TableComment);
        }


        [TestCase("TableSchema", "TableSchema.xls", "STA_VAT_SRC_ACCT", 10)]
        [TestCase("TableSchema", "TableSchema.xls", "STA_VAT_ACCT_DTL_CR", 20)]
        [TestCase("TableSchema", "TableSchema.xls", "STA_VAT_ISTB_CONTRACT", 6)]
        public void GetTableSchema_GetWorkSheet_RowCount(string doctype, string fileFullPath, string tableName, Int32 exRecordCnt)
        {
            //Arrange:初始化
            Int32 recordCnt;
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";
            List<string> tableList = new List<string>();
            tableList.Add(tableName);

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            recordCnt = documentReader.GetTableSchema(tableList).FindAll(x=>x.TableName==tableName).Count();

            //Assert: 驗證
            Assert.AreEqual(exRecordCnt, recordCnt);
        }

        [TestCase( "TableSchema.xls", "STA_VAT_SRC_ACCT" ,1 , "SAP_CO", "公司別", "VARCHAR(4)")]
        [TestCase( "TableSchema.xls", "STA_VAT_SRC_ACCT",  5, "SAP_PRD", "", "VARCHAR(4)")]
        [TestCase( "TableSchema.xls", "STA_VAT_SRC_ACCT", 9, "CATEGORY", "模組類型/資料來源", "VARCHAR(30)")]
        [TestCase( "TableSchema.xls", "STA_VAT_ACCT_DTL_CR", 11, "EXCH_RATE", "匯率", "DECIMAL(24,12)")]
        public void GetTableSchema_GetWorkSheet_CheckData(string fileFullPath, string tableName,  int rowIndex, string expColName, string expColDesc, string expDataType)
        {
            //Arrange:初始化        
            SourceReaderBase documentReader;
            TableSchema table;
            List<string> tableList = new List<string>();
            tableList.Add(tableName);
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(this._docType).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSchema(tableList).FindAll(x => x.TableName == tableName)[rowIndex];

            //Assert: 驗證
            Assert.NotNull(documentReader);
            Console.WriteLine($"documentReader Not Null >{documentReader.ToString()}");

            Console.WriteLine($"expect Column Name={expColName}，acture Column Name={table.ColumnName}");
            Assert.AreEqual(expColName, table.ColumnName);

            Console.WriteLine($"expect expColumnOriginalName ={expColDesc}，acture Column Name={table.ColumnDescription}");
            Assert.AreEqual(expColDesc, table.ColumnDescription);

            Console.WriteLine($"expect expColumn DataType ={expDataType}，acture Column DataType={table.DataType}");
            Assert.AreEqual(expDataType, table.DataType);
        }


        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "IndexOfTableDescription", "Code Table 共用代碼主檔-企業若自訂資料，則抓企業自訂的資料")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "IndexOfTablePPIName", "MIS_CATG_PPI")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "IndexOfTableDescription", null)]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "IndexOfTablePPIName", "BUSINESS_PPI")]

        public void GetTableSummaries_檢查額外欄位資料(string doctype, string fileFullPath, string appConfigFile, string tblName, string key, string expValue)
        {
            //Arrange:初始化        
            SourceReaderBase documentReader;
            TableSummary table;
            string value = "";
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype, appConfigFile).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSummaries().First(x => x.TableName == tblName);
            if (table.ExtensionData != null)
                value = table.ExtensionData.First(x => x.Key == key).Value;
            Console.WriteLine($"expect expValue={expValue}，acture Table Name={value}");

            //Assert: 驗證
            Assert.AreEqual(expValue, value);
        }

        //TODO 測試 TableSchema 額外欄位設定
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_cd", "ColumnExtion", ",catg_cd  VARCHAR(10) PRIMARY KEY NOT NULL")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_desc", "ColumnExtion", ",catg_desc  VARCHAR(50)")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "bus_eng_name", "ColumnExtion", ",bus_eng_name  NVARCHAR(150)")]
        public void GetTableSchema_檢查額外欄位資料(string doctype, string fileFullPath, string appConfigFile, string tblName, string colName, string key, string expValue)
        {
            //Arrange:初始化        
            SourceReaderBase documentReader;
            TableSchema schema;
            string value = "";
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype, appConfigFile).CreateDocumentReaderInstance(fileFullPath);
            schema = documentReader.GetTableSchema().First(x => x.TableName == tblName && x.ColumnName == colName);
            if (schema.ExtensionData != null)
                value = schema.ExtensionData.First(x => x.Key == key).Value;
            Console.WriteLine($"expect expValue={expValue}，acture Table Name={value}");

            //Assert: 驗證
            Assert.AreEqual(expValue, value);
        }
    }
}