using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.BaseFactory.Services;
using SourceReader.SDM;

namespace TestProject1.SourceReader
{

    [TestFixture]
    public class UTSourceReaderSDM : NUnitBase
    {
        private readonly string _filePath = "";
        private readonly string _docType = "SDM";


        [TestCase("SDM", "SDM_MIS.xlsx")]
        [Order(0)]
        public void Constructor_in2paras_InstanceNotNull(string doctype, string fileFullPath)
        {
            //Arrange:初始化                            
            SDMSourceReader documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SDMSourceReader(new SourceReaderFactory(doctype).GetAppConfiguration(doctype), new ExcelService(fileFullPath));

            //Assert: 驗證
            Assert.NotNull(documentReader);
            Assert.NotNull(documentReader.AppConfigure);
            Assert.NotNull(documentReader.ExcelService);
        }

        [TestCase("SDM", "SDM_MIS.xlsx", null)]
        [TestCase("SDM", "SDM_MIS.xlsx", "SourceReader.SDM_Defalut.json")]
        [TestCase("SDM", "台灣大哥大111年CRM汰換專案_P2_SDM_20230920.xlsx", "SourceReader.SDM_TWM_CRM-P2.json")]
        [Order(0)]
        public void Constructor_in2paras_InstanceNotNull(string doctype, string fileFullPath, string srcAppPath)
        {
            //Arrange:初始化                            
            SDMSourceReader documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SDMSourceReader(new SourceReaderFactory(doctype).GetAppConfiguration(doctype, srcAppPath), new ExcelService(fileFullPath));

            //Assert: 驗證
            Assert.NotNull(documentReader);
            Assert.NotNull(documentReader.AppConfigure);
            Assert.NotNull(documentReader.ExcelService);
        }

        [TestCase("SDM", "SDM_MIS.xlsx")]
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

        [TestCase("SDM", "SDM_MIS.xlsx", null, 14)]
        [TestCase("SDM", "SDM_MIS.xlsx", "SourceReader.SDM.json", 14)]
        [TestCase("SDM", "SDM_MIS.xlsx", "SourceReader.SDM_Defalut.json", 14)]
        [TestCase("SDM", "SDM_Team.xlsx", null, 3)]
        [TestCase("SDM", "SDM_Team.xlsx", "SourceReader.SDM.json", 3)]
        [TestCase("SDM", "台灣大哥大111年CRM汰換專案_P2_SDM_20230920.xlsx", "SourceReader.SDM_TWM_CRM-P2.json", 95)]
        public void GetTableSummaries_GetWorkSheet_RowCount(string doctype, string fileFullPath, string srcAppPath, Int32 exRecordCnt)
        {
            //Arrange:初始化
            Int32 recordCnt;
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype, srcAppPath).CreateDocumentReaderInstance(fileFullPath);
            recordCnt = documentReader.GetTableSummaries().Count();

            //Assert: 驗證
            Assert.AreEqual(exRecordCnt, recordCnt);
        }

        [TestCase("SDM", "SDM_MIS.xlsx", 3, "dept", "Department")]
        [TestCase("SDM", "SDM_MIS.xlsx", 5, "user_contact", "User Contact")]
        [TestCase("SDM", "SDM_MIS.xlsx", 9, "func_info", "Function Information")]
        [TestCase("SDM", "SDM_MIS.xlsx", 11, "sys_prvl_func", "System Privilege-Function")]
        public void GetTableSummaries_GetWorkSheet_CheckData(string doctype, string fileFullPath, Int32 rowIndex, string expTableName, string expTableOriginalName)
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

            Console.WriteLine($"expect expTableOriginalName ={expTableOriginalName}，acture Table Name={table.TableOriginalName}");
            Assert.AreEqual(expTableOriginalName, table.TableOriginalName);
        }


        [TestCase("SDM", "SDM_MIS.xlsx", 105)]
        [TestCase("SDM", "SDM_Team.xlsx", 14)]
        public void GetTableSchema_GetWorkSheet_RowCount(string doctype, string fileFullPath, Int32 exRecordCnt)
        {
            //Arrange:初始化
            Int32 recordCnt;
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            recordCnt = documentReader.GetTableSchema(new List<string>()).Count();

            //Assert: 驗證
            Assert.AreEqual(exRecordCnt, recordCnt);
        }

        [TestCase("SDM", null, "SDM_MIS.xlsx", 3, "in_usr_id", "Insert User ID", "VARCHAR(20)", true)]
        [TestCase("SDM", null, "SDM_MIS.xlsx", 5, "mod_usr_id", "Modify User ID", "VARCHAR(20)", true)]
        [TestCase("SDM", null, "SDM_MIS.xlsx", 8, "bus_id", "Bussiness ID", "VARCHAR(20)", false)]
        [TestCase("SDM", null, "SDM_MIS.xlsx", 11, "tel", "TEL", "VARCHAR(12)", true)]
        [TestCase("SDM", "SourceReader.SDM_MIS-ALL.json", "SDM-MIS-ALL.xlsx", 220, "apprl_map_name", "Approval Map Name", "VARCHAR(100)", false)]
        public void GetTableSchema_GetWorkSheet_CheckData(string doctype, string jsonPath, string fileFullPath, Int32 rowIndex, string expColName, string expColOriginalName, string expDataType, Boolean expNullable)
        {
            //Arrange:初始化        
            SourceReaderBase documentReader;
            TableSchema table;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";
            

            //Act: 執行方法、行為、操作並取得結果
            documentReader = new SourceReaderFactory(doctype, jsonPath).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSchema(new List<string>())[rowIndex];

            //Assert: 驗證
            Assert.NotNull(documentReader);
            Console.WriteLine($"documentReader Not Null >{documentReader.ToString()}");

            Console.WriteLine($"expect Column Name={expColName}，acture Column Name={table.ColumnName}");
            Assert.AreEqual(expColName, table.ColumnName);

            Console.WriteLine($"expect expColumnOriginalName ={expColOriginalName}，acture Column Name={table.ColumnOriginalName}");
            Assert.AreEqual(expColOriginalName, table.ColumnOriginalName);

            Console.WriteLine($"expect expColumn DataType ={expDataType}，acture Column DataType={table.DataType}");
            Assert.AreEqual(expDataType, table.DataType);

            Console.WriteLine($"expect expColumn Nullable ={expNullable}，acture Column Nullable={table.Nullable}");
            Assert.AreEqual(expNullable, table.Nullable);
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

        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_cd", "ColumnExtion", ",catg_cd  VARCHAR(10) PRIMARY KEY NOT NULL")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_desc", "ColumnExtion", ",catg_desc  VARCHAR(50)")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "bus_eng_name", "ColumnExtion", ",bus_eng_name  NVARCHAR(150)")]
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