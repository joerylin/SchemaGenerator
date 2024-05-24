using NUnitTestDDLGenerator.SchemaGenerator;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.Oracle;

namespace TestProject1.SchemaGenerator
{


    [TestFixture("Oracle")]
    public class UTSchemaGeneratorOracle : OracleSchemaGenerator
    {
        private readonly string _filePath = "";
        private readonly string _docType = "SDM";
        private readonly string _dbType;

        private readonly List<TableSummary> _tables = DataService.GetSourceTableSummaryData();
        private readonly List<TableSchema> _schemas = DataService.GetSourceTableSchemaData();

        public UTSchemaGeneratorOracle(string dBType) : base(dBType)
        {
            this._dbType = dBType;
        }



        [TestCase("mis_catg", "COMMENTONTABLEmis_catgIS'代碼資料-(分類主檔)';")]
        [TestCase("mis_item", "COMMENTONTABLEmis_itemIS'代碼資料-(項目明細)';")]
        [TestCase("news", "COMMENTONTABLEnewsIS'訊息檔';")]
        [TestCase("todolist", "")]
        public void GetTableCommentDDL_是否TableComment(string tableName, string expAns)
        {
            //Arrange:初始化
            string tblCommSql;

            //Act: 執行方法、行為、操作並取得結果          
            tblCommSql = base.GetTableCommentDDL(this._tables.First(x => x.TableName == tableName));

            Console.WriteLine($"Table Comment SQL>\n{tblCommSql}");
            tblCommSql = tblCommSql.Replace(" ", "").Replace("\r\n","");
            Console.WriteLine($"Table Comment Replace SQL>\n{tblCommSql}");

            //Assert: 驗證           
            Assert.AreEqual(expAns, tblCommSql);
        }

        [TestCase("mis_catg", "bus_no", "COMMENTONCOLUMNmis_catg.bus_noIS'企業編號';")]
        [TestCase("mis_catg", "catg_cd", "COMMENTONCOLUMNmis_catg.catg_cdIS'大類代碼';")]
        [TestCase("mis_catg", "enbl_ind", "COMMENTONCOLUMNmis_catg.enbl_indIS'是否啟用';")]
        [TestCase("todolist", "msg", "")]
        public void GetColumnCommentDDL_是否TableComment(string tableName, string colName, string expAns)
        {
            //Arrange:初始化
            string colCommSql;

            //Act: 執行方法、行為、操作並取得結果          
            colCommSql = base.GetColumnCommentDDL(this._schemas.FindAll(x => x.TableName == tableName && x.ColumnName == colName));
            colCommSql = colCommSql.Replace(" ", "");
            Console.WriteLine($"Column Comment SQL>\n{colCommSql}");
            colCommSql = colCommSql.Replace(" ", "").Replace("\r\n","");
            Console.WriteLine($"Column Comment Replace SQL>\n{colCommSql}");

            //Assert: 驗證
            Assert.AreEqual(expAns, colCommSql);
        }


        [TestCase(false, false, 4)]
        [TestCase(false, true, 4)]
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