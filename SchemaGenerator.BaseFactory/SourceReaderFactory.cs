using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.BaseFactory.Services;
using System.Reflection;
using System.Text.Json;

namespace SchemaGenerator.BaseFactory
{

    public class SourceReaderFactory      :BaseClass
    {
        private SourceReaderBase _srcReader;    
        private readonly AppConfiguration _appConfiguration;


        public SourceReaderFactory(string srcType, string srcAppconfigPath=null)
        { 
            this._appConfiguration =this.GetAppConfiguration(srcType, srcAppconfigPath);
        }

        public AppConfiguration GetAppConfiguration(string srcType, string srcAppconfigPath = null)
        {
            string jsonStr ;
            if(string.IsNullOrEmpty(srcAppconfigPath))
                jsonStr = File.ReadAllText($@"{base._appDataPath}SourceReader.{srcType}.json");
            else
                jsonStr = File.ReadAllText($@"{base._appDataPath}{srcAppconfigPath}");

            return JsonSerializer.Deserialize<AppConfiguration>(jsonStr);
        }

        /// <summary>
        /// 建立 Source Reader Instance
        /// </summary>
        /// <param name="filepath">file(Excel) 路徑</param>
        /// <returns>Source Reader Instance></returns>
        public SourceReaderBase CreateDocumentReaderInstance(string filepath)
        {
            if (this._srcReader == null)
            {         
                string filename = $"{base._extensionPath}SourceReader.{this._appConfiguration.DataSourceType}.dll";
                Assembly assembly = Assembly.LoadFrom(filename);
                Type type = assembly.GetType($"SourceReader.{this._appConfiguration.DataSourceType}.{this._appConfiguration.DataSourceType}SourceReader")!;
                var classInstance = Activator.CreateInstance(type, this._appConfiguration, new ExcelService(filepath));       
                if (classInstance is SourceReaderBase)
                    this._srcReader = classInstance as SourceReaderBase;
            }
            return this._srcReader!;
        }
    }
}
