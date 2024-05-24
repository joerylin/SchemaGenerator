using NUnitTestDDLGenerator.SchemaGenerator;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;

namespace TestProject1.SchemaGenerator_BaseFactory
{
    [TestFixture("MSSQL")]
    public class UTSchemaGeneratorBase : SchemaGeneratorBase
    {
        //private readonly string _filePath;
        private readonly string _docType = "SDM";
        private readonly List<TableSummary> _tables = DataService.GetSourceTableSummaryData();
        private readonly List<TableSchema> _schemas = DataService.GetSourceTableSchemaData();

        public UTSchemaGeneratorBase(string dBType) : base(dBType)
        {
        }

        protected override string GetColumnCommentDDL(List<TableSchema> schemas)
        {
            throw new NotImplementedException();
        }

        protected override string GetCreateTableDDLSQLString(string tableName)
        {
            throw new NotImplementedException();
        }

        protected override string GetTableCommentDDL(TableSummary table)
        {
            throw new NotImplementedException();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SetData_Input3Objects_CheckCnt()
        {
            //Arrange:初始化                            
            int tableCnt = _tables.Count;
            int schemaCnt = _schemas.Count;

            //Act: 執行方法、行為、操作並取得結果
            SetData(_tables, _schemas, true, false);

            //Assert: 驗證
            Assert.AreEqual(tableCnt, _tableSummaries.Count);
            Assert.AreEqual(schemaCnt, _tableSchemas.Count);
        }

        [TestCase(true, "")]
        [TestCase(false, "NOT NULL")]
        public void GetNullableTag_InputBool_NullableDDL(bool flag, string ansStr)
        {
            //Arrange:初始化
            string SQLStr ;

            //Act: 執行方法、行為、操作並取得結果
            SQLStr = GetNullableTag(flag);

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
            SQLStr =  GetPKcolumn(_schemas.FindAll(x => x.TableName == tblName));

            //Assert: 驗證
            Console.WriteLine(SQLStr);
            Assert.AreEqual(ansStr, SQLStr);
        }


    }
}