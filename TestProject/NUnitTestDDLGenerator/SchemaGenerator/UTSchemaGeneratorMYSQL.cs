using NUnitTestDDLGenerator.SchemaGenerator;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.MYSQL;

namespace TestProject1.SchemaGenerator
{


    [TestFixture("MYSQL")]
    public class UTSchemaGeneratorMYSQL : MYSQLSchemaGenerator
    {
        private readonly string _filePath = "";
        private readonly string _docType = "SDM";
        private readonly string _dbType;

        private readonly List<TableSummary> _tables = DataService.GetSourceTableSummaryData();
        private readonly List<TableSchema> _schemas = DataService.GetSourceTableSchemaData();

        public UTSchemaGeneratorMYSQL(string dBType) : base(dBType)
        {
            this._dbType = dBType;
        }



        [TestCase(false, false, 4)]
        [TestCase(false, true, 4)]
        [TestCase(true, false, 1)]
        [TestCase(true, true, 1)]
        public void GeneratorCreateTableDDL_��J�ɮץت���T_�O�_���T����DDL(Boolean isCombineFile, Boolean isTblHasDbOwnerName, int expCnt)
        {
            //Arrange:��l��
            string path = "C:\\Users\\Joery\\Downloads\\SQLString\\My_SQL\\";
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

    }
}