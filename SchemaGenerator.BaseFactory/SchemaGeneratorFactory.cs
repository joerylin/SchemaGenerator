using System.Reflection;

namespace SchemaGenerator.BaseFactory
{

    public class SchemaGeneratorFactory(string dbType) : BaseClass
    {
        private SchemaGeneratorBase _schemaGenertor;
        private string _dbType = dbType;


        /// <summary>
        /// 建立 Schema Generator Instance
        /// </summary>
        /// <returns> Schema Generator  Instance></returns>
        public SchemaGeneratorBase CreateSchemaGeneratorInstance()
        {
            if (this._schemaGenertor == null)
            {
                string filename = $"{base._extensionPath}SchemaGenerator.{this._dbType}.dll";
                Assembly assembly = Assembly.LoadFrom(filename);
                Type type = assembly.GetType($"SchemaGenerator.{this._dbType}.{this._dbType}SchemaGenerator")!;
              
                var classInstance = Activator.CreateInstance(type, this._dbType);         
                if (classInstance is SchemaGeneratorBase)
                    this._schemaGenertor = classInstance as SchemaGeneratorBase;
            }
            return this._schemaGenertor!;
        }

    }
}
