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



        [TestCase("mis_catg", "COMMENTONTABLEmis_catgIS'�N�X���-(�����D��)';")]
        [TestCase("mis_item", "COMMENTONTABLEmis_itemIS'�N�X���-(���ة���)';")]
        [TestCase("news", "COMMENTONTABLEnewsIS'�T����';")]
        [TestCase("todolist", "")]
        public void GetTableCommentDDL_�O�_TableComment(string tableName, string expAns)
        {
            //Arrange:��l��
            string tblCommSql;

            //Act: �����k�B�欰�B�ާ@�è��o���G          
            tblCommSql = base.GetTableCommentDDL(this._tables.First(x => x.TableName == tableName));

            Console.WriteLine($"Table Comment SQL>\n{tblCommSql}");
            tblCommSql = tblCommSql.Replace(" ", "").Replace("\r\n","");
            Console.WriteLine($"Table Comment Replace SQL>\n{tblCommSql}");

            //Assert: ����           
            Assert.AreEqual(expAns, tblCommSql);
        }

        [TestCase("mis_catg", "bus_no", "COMMENTONCOLUMNmis_catg.bus_noIS'���~�s��';")]
        [TestCase("mis_catg", "catg_cd", "COMMENTONCOLUMNmis_catg.catg_cdIS'�j���N�X';")]
        [TestCase("mis_catg", "enbl_ind", "COMMENTONCOLUMNmis_catg.enbl_indIS'�O�_�ҥ�';")]
        [TestCase("todolist", "msg", "")]
        public void GetColumnCommentDDL_�O�_TableComment(string tableName, string colName, string expAns)
        {
            //Arrange:��l��
            string colCommSql;

            //Act: �����k�B�欰�B�ާ@�è��o���G          
            colCommSql = base.GetColumnCommentDDL(this._schemas.FindAll(x => x.TableName == tableName && x.ColumnName == colName));
            colCommSql = colCommSql.Replace(" ", "");
            Console.WriteLine($"Column Comment SQL>\n{colCommSql}");
            colCommSql = colCommSql.Replace(" ", "").Replace("\r\n","");
            Console.WriteLine($"Column Comment Replace SQL>\n{colCommSql}");

            //Assert: ����
            Assert.AreEqual(expAns, colCommSql);
        }


        [TestCase(false, false, 4)]
        [TestCase(false, true, 4)]
        [TestCase(true, false, 1)]
        [TestCase(true, true, 1)]
        public void GeneratorCreateTableDDL_��J�ɮץت���T_�O�_���T����DDL(Boolean isCombineFile, Boolean isTblHasDbOwnerName, int expCnt)
        {
            //Arrange:��l��
            string path = "C:\\Users\\Joery\\Downloads\\SQLString\\";
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