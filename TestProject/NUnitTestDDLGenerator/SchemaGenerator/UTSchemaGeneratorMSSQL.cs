using NUnitTestDDLGenerator.SchemaGenerator;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.MSSQL;

namespace TestProject1.SchemaGenerator
{


    [TestFixture("MSSQL")]
    public class UTSchemaGeneratorMSSQL : MSSQLSchemaGenerator
    {
        private readonly string _filePath = "";
        private readonly string _docType = "SDM";
        private readonly string _dbType;

        private readonly List<TableSummary> _tables = DataService.GetSourceTableSummaryData();
        private readonly List<TableSchema> _schemas = DataService.GetSourceTableSchemaData();

        public UTSchemaGeneratorMSSQL(string dBType) : base(dBType)
        {
            this._dbType = dBType;
        }


        [Test]
        public void SetData_Input2Objects_CheckCnt()
        {
            //Arrange:初始化                            
            int tableCnt = _tables.Count;
            int schemaCnt = _schemas.Count;

            //Act: 執行方法、行為、操作並取得結果
            base.SetData(_tables, _schemas, true, false);

            //Assert: 驗證
            Assert.AreEqual(tableCnt, base._tableSummaries.Count);
            Assert.AreEqual(schemaCnt, base._tableSchemas.Count);
        }

        [TestCase(true, "")]
        [TestCase(false, "NOT NULL")]
        public void GetNullableTag_InputBool_NullableDDL(Boolean flag, string ansStr)
        {
            //Arrange:初始化
            string SQLStr;

            //Act: 執行方法、行為、操作並取得結果
            SQLStr = base.GetNullableTag(flag);

            //Assert: 驗證
            Assert.AreEqual(ansStr, SQLStr);
        }

        [TestCase("mis_catg", "bus_no,catg_cd")]
        [TestCase("mis_item", "bus_no,catg_cd,item_cd")]
        [TestCase("news", "bus_no,news_no")]
        public void GetPKcolumn_InputTblNm_PKColumnDDL(string tblName, string ansStr)
        {
            //Arrange:初始化
            string SQLStr = "";

            //Act: 執行方法、行為、操作並取得結果
            SQLStr = base.GetPKcolumn(_schemas.FindAll(x => x.TableName == tblName));

            //Assert: 驗證
            Assert.AreEqual(ansStr, SQLStr);
        }

        [TestCase(false, false, 4)]
        [TestCase(false, true,4)]
        [TestCase(true, false, 1)]
        [TestCase(true, true, 1)]
        public void GeneratorCreateTableDDL_輸入檔案目的資訊_是否正確產生DDL(Boolean isCombineFile, Boolean isTblHasDbOwnerName, int expCnt)
        {
            //Arrange:初始化
            string path = "C:\\Users\\Joery\\Downloads\\SQLString\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach (string f in Directory.GetFiles(path, "*.sql"))
                File.Delete(f);
            SchemaGeneratorBase schemaGenerator = new SchemaGeneratorFactory(this._dbType).CreateSchemaGeneratorInstance();
            schemaGenerator.SetData(this._tables, this._schemas, isCombineFile, isTblHasDbOwnerName);

            //Act: 執行方法、行為、操作並取得結果          
            schemaGenerator.GeneratorCreateTableDDL(path);

            //Assert: 驗證
            int fileCnt = Directory.GetFiles(path, "*.sql").Length;
            Assert.AreEqual(expCnt, fileCnt);
        }

    }
}