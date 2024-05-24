using NUnitTestDDLGenerator.SchemaGenerator;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.MSAzureSynapse;

namespace TestProject1.SchemaGenerator
{


    [TestFixture("MSAzureSynapse")]
    public class UTSchemaGeneratorAzureSynapse : MSAzureSynapseSchemaGenerator
    {
        private readonly string _filePath = "";
        private readonly string _docType = "SDM";
        private readonly string _dbType;

        private readonly List<TableSummary> _tables = DataService.GetSourceTableSummaryData();
        private readonly List<TableSchema> _schemas = DataService.GetSourceTableSchemaData();

        public UTSchemaGeneratorAzureSynapse(string dBType) : base(dBType)
        {
            this._dbType = dBType;
        }



        [TestCase(false, false, 4)]
        [TestCase(false, true, 4)]
        [TestCase(true, false, 1)]
        [TestCase(true, true, 1)]
        [Order(2)]
        public void GeneratorCreateTableDDL_��J�ɮץت���T_�O�_���T����DDL(Boolean isCombineFile, Boolean isTblHasDbOwnerName, int expCnt)
        {
            //Arrange:��l��
            string path = "C:\\Users\\Joery\\Downloads\\SQLString\\MS_Azure_Synapse_SQL\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach (string f in Directory.GetFiles(path, "*.sql"))
                File.Delete(f);
            SchemaGeneratorBase schemaGenerator = new SchemaGeneratorFactory(this._dbType).CreateSchemaGeneratorInstance();
            schemaGenerator.SetData(this._tables, this._schemas, isCombineFile, isTblHasDbOwnerName);

            //Act: �����k�B�欰�B�ާ@�è��o���G          
            schemaGenerator.GeneratorCreateTableDDL(path);

            //Assert: ����
            int fileCnt = Directory.GetFiles(path, "*.sql").Length;
            Assert.AreEqual(expCnt, fileCnt);
        }

        [TestCase("mis_catg", true,     "WITH(CLUSTEREDCOLUMNSTOREINDEX,DISTRIBUTION=HASH(bus_no,catg_cd))")]
        [TestCase("mis_item", true, "WITH(CLUSTEREDCOLUMNSTOREINDEX,DISTRIBUTION=HASH(bus_no,catg_cd,item_cd))")]
        [TestCase("todolist", true, "WITH(CLUSTEREDCOLUMNSTOREINDEX,DISTRIBUTION=ROUND_ROBIN)")]             
        [TestCase("mis_catg", false, "WITH(CLUSTEREDCOLUMNSTOREINDEX,DISTRIBUTION=ROUND_ROBIN)")]
        [TestCase("mis_item", false,    "WITH(CLUSTEREDCOLUMNSTOREINDEX,DISTRIBUTION=ROUND_ROBIN)")]
        [TestCase("todolist", false, "WITH(CLUSTEREDCOLUMNSTOREINDEX,DISTRIBUTION=ROUND_ROBIN)")]
        [Order(1)]
        public void getTableOption_��JisPK_�T�{TableOption�O�_���T(string tblName, Boolean isNeedPK, string expTblOptSql)
        {
            //Arrange:��l��
            string tblOptSql;

            //Act: �����k�B�欰�B�ާ@�è��o���G
            if (isNeedPK)
                tblOptSql = base.getTableOption(this._schemas.FindAll(x => x.TableName == tblName));
            else
                tblOptSql = base.getTableOption(new List<TableSchema>());

            Console.WriteLine($"Table Option SQL>\n{tblOptSql}");
            tblOptSql = tblOptSql.Replace(" ", "").Replace("\t", "").ReplaceLineEndings("");
            Console.WriteLine($"Table Option Replace SQL>\n{tblOptSql}");
            //Assert: ����
            Assert.AreEqual(expTblOptSql, tblOptSql);

        }
    }
}