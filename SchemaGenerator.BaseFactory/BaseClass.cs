using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaGenerator.BaseFactory
{
    public class BaseClass
    {
        protected readonly string _basePath;
        protected readonly string _appDataPath;
        protected readonly string _extensionPath;
        protected readonly string _templatePath;
        public BaseClass()
        {
            this._basePath = AppDomain.CurrentDomain.BaseDirectory;
            this._appDataPath = $@"{this._basePath}App_Data\";
            this._extensionPath = $@"{this._basePath}Extension\";
            this._templatePath = $@"{this._basePath}Template\";
        }
    }
}
