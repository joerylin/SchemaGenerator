using NUnitTestDDLGenerator.SchemaGenerator;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.MSSQL;
using SchemaGenerator.POCOModel;

namespace TestProject1.SchemaGenerator
{


    [TestFixture("POCOModel")]
    public class UTSchemaGeneratorPOCOModel : POCOModelSchemaGenerator

    {
        private readonly string _filePath = "";
        private readonly string _docType = "SDM";
        private readonly string _dbType;

        private readonly List<TableSummary> _tables = DataService.GetSourceTableSummaryData();
        private readonly List<TableSchema> _schemas = DataService.GetSourceTableSchemaData();

        public UTSchemaGeneratorPOCOModel(string dBType) : base(dBType)
        {
            this._dbType = dBType;
        }


        [Test]
        public void SetData_Input2Objects_CheckCnt()
        {
            //Arrange:��l��                            
            int tableCnt = _tables.Count;
            int schemaCnt = _schemas.Count;

            //Act: �����k�B�欰�B�ާ@�è��o���G
            base.SetData(_tables, _schemas, true, false);

            //Assert: ����
            Assert.AreEqual(tableCnt, base._tableSummaries.Count);
            Assert.AreEqual(schemaCnt, base._tableSchemas.Count);
        }


        [TestCase(false, false, 4)]
        [TestCase(false, true, 4)]
        [TestCase(true, false, 1)]
        [TestCase(true, true, 1)]
        public void GeneratorCreateTableDDL_��J�ɮץت���T_�O�_���T����DDL(Boolean isCombineFile, Boolean isTblHasDbOwnerName, int expCnt)
        {
            //Arrange:��l��
            string path = "C:\\Users\\Joery\\Downloads\\POCO\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach (string f in Directory.GetFiles(path, "*.cs"))
                File.Delete(f);
            SchemaGeneratorBase schemaGenerator = new SchemaGeneratorFactory(this._dbType).CreateSchemaGeneratorInstance();
            schemaGenerator.SetData(this._tables, this._schemas, isCombineFile, isTblHasDbOwnerName);

            //Act: �����k�B�欰�B�ާ@�è��o���G            
            schemaGenerator.GeneratorCreateTableDDL(path, "POCOModel.cs");

            //Assert: ����
            int fileCnt = Directory.GetFiles(path, "*.cs").Length;
            Assert.AreEqual(expCnt, fileCnt);
        }

    }
}