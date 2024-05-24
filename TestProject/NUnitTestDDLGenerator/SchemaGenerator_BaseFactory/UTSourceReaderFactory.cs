using CommonLibrary;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;


namespace TestProject1.SchemaGenerator_BaseFactory
{

    [TestFixture]
    public class UTSourceReaderFactory : NUnitBase
    {

        [SetUp]
        public void Setup()
        {

        }


        [TestCase("SDM", "SDM_MIS.xlsx")]
        [TestCase("SDM", "SDM_Team.xlsx")]
        // [TestCase("TableSchema", null)]
        // [TestCase("TableSchema", "")]
        public void Constructor_in2paras_Config(string doctype, string filePath)
        {
            //Arrange:��l��
            SourceReaderBase sourceReader;
            filePath = $"{base.BaseTemplatePath}{filePath}";

            //Act: �����k�B�欰�B�ާ@�è��o���G
            sourceReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(filePath);

            //Assert: ����            
            Assert.NotNull(sourceReader);
            Console.WriteLine($"Instance >{sourceReader.GetType().FullName}");
        }

        [TestCase("SDM")]
        [TestCase("TableSchema")]
        public void GetAppConfiguration_iSrcType_AppConfig(string doctype)
        {
            //Arrange:��l��
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: �����k�B�欰�B�ާ@�è��o���G
            appConfiguration = srcFactory.GetAppConfiguration(doctype);

            //Assert: ����            
            Assert.NotNull(appConfiguration);
            Console.WriteLine($"appConfiguration DataSourceType >{appConfiguration.DataSourceType}");
        }


        [TestCase("SDM", "SourceReader.SDM.json")]
        [TestCase("SDM", "SourceReader.SDM_iSAT.json")]
        public void GetAppConfiguration_��J�]�w�ɦW_AppConfig(string doctype, string srcAppPath)
        {
            //Arrange:��l��
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: �����k�B�欰�B�ާ@�è��o���G
            appConfiguration = srcFactory.GetAppConfiguration(doctype, srcAppPath);

            //Assert: ����            
            Assert.NotNull(appConfiguration);
            Console.WriteLine($"appConfiguration DataSourceType >{appConfiguration.DataSourceType}");
        }

        [TestCase("SDM", "SourceReader.SDM.json", "mis_catg", "IndexOfTableDescription", null)]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "mis_catg", "IndexOfTableDescription", "Index Of Table Description")]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "func_info", "IndexOfTablePPIName", "Primary Index  Name")]
        public void GetAppConfiguration_��JKey_�T�{TableAppConfig��ƬO�_���T(string doctype, string srcAppPath, string tblName, string key, string expStrValue)
        {
            //Arrange:��l��
            ExtensionData data = new ExtensionData();
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: �����k�B�欰�B�ާ@�è��o���G
            appConfiguration = srcFactory.GetAppConfiguration(doctype, srcAppPath);
            if (appConfiguration.TableConfiguration.ExtensionData != null)
                data = appConfiguration.TableConfiguration.ExtensionData.Find(x => x.Key == key);
            Console.WriteLine($"expect ans:{expStrValue}");
            Console.WriteLine($"Key:{data.Key}\nName:{data.Name}\nIndexOf:{data.IndexOfColumn}\nvalue:{data.Value}");

            //Assert: ����            
            Assert.AreEqual(expStrValue, data.Name);
        }

        [TestCase("SDM", "SourceReader.SDM.json", "mis_catg", "catg_cd", "ColumnExtion", null)]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_cd", "ColumnExtion", "Primary Index  Name")]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "func_info", "func_url", "ColumnExtion", "Primary Index  Name")]
        public void GetAppConfiguration_��JKey_�T�{ColumnAppConfig��ƬO�_���T(string doctype, string srcAppPath, string tblName, string colName, string key, string expStrValue)
        {
            //Arrange:��l��
            ExtensionData data = new ExtensionData();
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: �����k�B�欰�B�ާ@�è��o���G
            appConfiguration = srcFactory.GetAppConfiguration(doctype, srcAppPath);
            if (appConfiguration.SchemaConfiguration.ExtensionData != null)
                data = appConfiguration.SchemaConfiguration.ExtensionData.Find(x => x.Key == key);
            Console.WriteLine($"expect ans:{expStrValue}");
            Console.WriteLine($"Key:{data.Key}\nName:{data.Name}\nIndexOf:{data.IndexOfColumn}\nvalue:{data.Value}");

            //Assert: ����            
            Assert.AreEqual(expStrValue, data.Name);
        }
    }
}