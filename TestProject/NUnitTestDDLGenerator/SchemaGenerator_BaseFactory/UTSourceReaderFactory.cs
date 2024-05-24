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
            //Arrange:初始化
            SourceReaderBase sourceReader;
            filePath = $"{base.BaseTemplatePath}{filePath}";

            //Act: 執行方法、行為、操作並取得結果
            sourceReader = new SourceReaderFactory(doctype).CreateDocumentReaderInstance(filePath);

            //Assert: 驗證            
            Assert.NotNull(sourceReader);
            Console.WriteLine($"Instance >{sourceReader.GetType().FullName}");
        }

        [TestCase("SDM")]
        [TestCase("TableSchema")]
        public void GetAppConfiguration_iSrcType_AppConfig(string doctype)
        {
            //Arrange:初始化
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: 執行方法、行為、操作並取得結果
            appConfiguration = srcFactory.GetAppConfiguration(doctype);

            //Assert: 驗證            
            Assert.NotNull(appConfiguration);
            Console.WriteLine($"appConfiguration DataSourceType >{appConfiguration.DataSourceType}");
        }


        [TestCase("SDM", "SourceReader.SDM.json")]
        [TestCase("SDM", "SourceReader.SDM_iSAT.json")]
        public void GetAppConfiguration_輸入設定檔名_AppConfig(string doctype, string srcAppPath)
        {
            //Arrange:初始化
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: 執行方法、行為、操作並取得結果
            appConfiguration = srcFactory.GetAppConfiguration(doctype, srcAppPath);

            //Assert: 驗證            
            Assert.NotNull(appConfiguration);
            Console.WriteLine($"appConfiguration DataSourceType >{appConfiguration.DataSourceType}");
        }

        [TestCase("SDM", "SourceReader.SDM.json", "mis_catg", "IndexOfTableDescription", null)]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "mis_catg", "IndexOfTableDescription", "Index Of Table Description")]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "func_info", "IndexOfTablePPIName", "Primary Index  Name")]
        public void GetAppConfiguration_輸入Key_確認TableAppConfig資料是否正確(string doctype, string srcAppPath, string tblName, string key, string expStrValue)
        {
            //Arrange:初始化
            ExtensionData data = new ExtensionData();
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: 執行方法、行為、操作並取得結果
            appConfiguration = srcFactory.GetAppConfiguration(doctype, srcAppPath);
            if (appConfiguration.TableConfiguration.ExtensionData != null)
                data = appConfiguration.TableConfiguration.ExtensionData.Find(x => x.Key == key);
            Console.WriteLine($"expect ans:{expStrValue}");
            Console.WriteLine($"Key:{data.Key}\nName:{data.Name}\nIndexOf:{data.IndexOfColumn}\nvalue:{data.Value}");

            //Assert: 驗證            
            Assert.AreEqual(expStrValue, data.Name);
        }

        [TestCase("SDM", "SourceReader.SDM.json", "mis_catg", "catg_cd", "ColumnExtion", null)]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "mis_catg", "catg_cd", "ColumnExtion", "Primary Index  Name")]
        [TestCase("SDM", "SourceReader.SDM_Defalut.json", "func_info", "func_url", "ColumnExtion", "Primary Index  Name")]
        public void GetAppConfiguration_輸入Key_確認ColumnAppConfig資料是否正確(string doctype, string srcAppPath, string tblName, string colName, string key, string expStrValue)
        {
            //Arrange:初始化
            ExtensionData data = new ExtensionData();
            AppConfiguration appConfiguration;
            SourceReaderFactory srcFactory = new SourceReaderFactory(doctype);

            //Act: 執行方法、行為、操作並取得結果
            appConfiguration = srcFactory.GetAppConfiguration(doctype, srcAppPath);
            if (appConfiguration.SchemaConfiguration.ExtensionData != null)
                data = appConfiguration.SchemaConfiguration.ExtensionData.Find(x => x.Key == key);
            Console.WriteLine($"expect ans:{expStrValue}");
            Console.WriteLine($"Key:{data.Key}\nName:{data.Name}\nIndexOf:{data.IndexOfColumn}\nvalue:{data.Value}");

            //Assert: 驗證            
            Assert.AreEqual(expStrValue, data.Name);
        }
    }
}