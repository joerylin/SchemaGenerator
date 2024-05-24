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


        [TestCase("SDM", "TableSchema.xls"), Description("���իإ� Instance �O�_���\")]
        [Order(0)]
        public void Constructor_in2paras_InstanceNotNull(string doctype, string fileFullPath)
        {
            //Arrange:��l��                            
            TableSchemaSourceReader documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new TableSchemaSourceReader(new SourceReaderFactory(doctype).GetAppConfiguration(doctype), new ExcelService(fileFullPath));

            //Assert: ����
            Assert.NotNull(documentReader);
            Assert.NotNull(documentReader.AppConfigure);
            Assert.NotNull(documentReader.ExcelService);
        }

        [TestCase("TableSchema", "TableSchema.xls")]
        public void Constructor_in2parasByFactory_InstanceNotNull(string doctype, string fileFullPath)
        {
            //Arrange:��l��                            
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);

            //Assert: ����
            Assert.NotNull(documentReader);
        }

        [TestCase("TableSchema", "TableSchema.xls", 11)]
        public void GetTableSummaries_GetWorkSheet_RowCount(string doctype, string fileFullPath, Int32 exRecordCnt)
        {
            //Arrange:��l��
            Int32 recordCnt;
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            recordCnt = documentReader.GetTableSummaries().Count();

            //Assert: ����
            Assert.AreEqual(exRecordCnt, recordCnt);
        }

        [TestCase("TableSchema", "TableSchema.xls", 3, "STA_VAT_ACCT_DTL_DE", "�b�����-�鷸�u")]
        [TestCase("TableSchema", "TableSchema.xls", 5, "STA_VAT_TXN_CL", "�����T-CL�Ҳ�")]
        [TestCase("TableSchema", "TableSchema.xls", 9, "STA_VAT_DETB_TELLER", "TELLER�����T")]
        [TestCase("TableSchema", "TableSchema.xls", 10, "STA_VAT_SRC_ACCT", "SRC ��ƪ�")]
        public void GetTableSummaries_GetWorkSheet_CheckData(string doctype, string fileFullPath, Int32 rowIndex, string expTableName, string expTableDesc)
        {
            //Arrange:��l��        
            SourceReaderBase documentReader;
            TableSummary table;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSummaries()[rowIndex];

            //Assert: ����
            Assert.NotNull(documentReader);
            Console.WriteLine($"documentReader Not Null >{documentReader.ToString()}");
        
            Console.WriteLine($"expect Table Name={expTableName}�Aacture Table Name={table.TableName}");
            Assert.AreEqual(expTableName, table.TableName);
                
            Console.WriteLine($"expect expTableOriginalName ={expTableDesc}�Aacture Table Name={table.TableComment}");
            Assert.AreEqual(expTableDesc, table.TableComment);
        }


        [TestCase("TableSchema", "TableSchema.xls", "STA_VAT_SRC_ACCT", 10)]
        [TestCase("TableSchema", "TableSchema.xls", "STA_VAT_ACCT_DTL_CR", 20)]
        [TestCase("TableSchema", "TableSchema.xls", "STA_VAT_ISTB_CONTRACT", 6)]
        public void GetTableSchema_GetWorkSheet_RowCount(string doctype, string fileFullPath, string tableName, Int32 exRecordCnt)
        {
            //Arrange:��l��
            Int32 recordCnt;
            SourceReaderBase documentReader;
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";
            List<string> tableList = new List<string>();
            tableList.Add(tableName);

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(fileFullPath);
            recordCnt = documentReader.GetTableSchema(tableList).FindAll(x=>x.TableName==tableName).Count();

            //Assert: ����
            Assert.AreEqual(exRecordCnt, recordCnt);
        }

        [TestCase( "TableSchema.xls", "STA_VAT_SRC_ACCT" ,1 , "SAP_CO", "���q�O", "VARCHAR(4)")]
        [TestCase( "TableSchema.xls", "STA_VAT_SRC_ACCT",  5, "SAP_PRD", "", "VARCHAR(4)")]
        [TestCase( "TableSchema.xls", "STA_VAT_SRC_ACCT", 9, "CATEGORY", "�Ҳ�����/��ƨӷ�", "VARCHAR(30)")]
        [TestCase( "TableSchema.xls", "STA_VAT_ACCT_DTL_CR", 11, "EXCH_RATE", "�ײv", "DECIMAL(24,12)")]
        public void GetTableSchema_GetWorkSheet_CheckData(string fileFullPath, string tableName,  int rowIndex, string expColName, string expColDesc, string expDataType)
        {
            //Arrange:��l��        
            SourceReaderBase documentReader;
            TableSchema table;
            List<string> tableList = new List<string>();
            tableList.Add(tableName);
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(this._docType).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSchema(tableList).FindAll(x => x.TableName == tableName)[rowIndex];

            //Assert: ����
            Assert.NotNull(documentReader);
            Console.WriteLine($"documentReader Not Null >{documentReader.ToString()}");

            Console.WriteLine($"expect Column Name={expColName}�Aacture Column Name={table.ColumnName}");
            Assert.AreEqual(expColName, table.ColumnName);

            Console.WriteLine($"expect expColumnOriginalName ={expColDesc}�Aacture Column Name={table.ColumnDescription}");
            Assert.AreEqual(expColDesc, table.ColumnDescription);

            Console.WriteLine($"expect expColumn DataType ={expDataType}�Aacture Column DataType={table.DataType}");
            Assert.AreEqual(expDataType, table.DataType);
        }


        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "IndexOfTableDescription", "Code Table �@�ΥN�X�D��-���~�Y�ۭq��ơA�h����~�ۭq�����")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "IndexOfTablePPIName", "MIS_CATG_PPI")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "IndexOfTableDescription", null)]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "IndexOfTablePPIName", "BUSINESS_PPI")]

        public void GetTableSummaries_�ˬd�B�~�����(string doctype, string fileFullPath, string appConfigFile, string tblName, string key, string expValue)
        {
            //Arrange:��l��        
            SourceReaderBase documentReader;
            TableSummary table;
            string value = "";
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(doctype, appConfigFile).CreateDocumentReaderInstance(fileFullPath);
            table = documentReader.GetTableSummaries().First(x => x.TableName == tblName);
            if (table.ExtensionData != null)
                value = table.ExtensionData.First(x => x.Key == key).Value;
            Console.WriteLine($"expect expValue={expValue}�Aacture Table Name={value}");

            //Assert: ����
            Assert.AreEqual(expValue, value);
        }

        //TODO ���� TableSchema �B�~���]�w
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_cd", "ColumnExtion", ",catg_cd  VARCHAR(10) PRIMARY KEY NOT NULL")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_desc", "ColumnExtion", ",catg_desc  VARCHAR(50)")]
        [TestCase("SDM", "SDM_MIS2.xlsx", "SourceReader.SDM_Defalut.json", "business", "bus_eng_name", "ColumnExtion", ",bus_eng_name  NVARCHAR(150)")]
        public void GetTableSchema_�ˬd�B�~�����(string doctype, string fileFullPath, string appConfigFile, string tblName, string colName, string key, string expValue)
        {
            //Arrange:��l��        
            SourceReaderBase documentReader;
            TableSchema schema;
            string value = "";
            fileFullPath = $"{base.BaseTemplatePath}{fileFullPath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            documentReader = new SourceReaderFactory(doctype, appConfigFile).CreateDocumentReaderInstance(fileFullPath);
            schema = documentReader.GetTableSchema().First(x => x.TableName == tblName && x.ColumnName == colName);
            if (schema.ExtensionData != null)
                value = schema.ExtensionData.First(x => x.Key == key).Value;
            Console.WriteLine($"expect expValue={expValue}�Aacture Table Name={value}");

            //Assert: ����
            Assert.AreEqual(expValue, value);
        }
    }
}