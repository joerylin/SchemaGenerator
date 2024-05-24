using SchemaGenerator.BaseFactory.Models;
using SchemaGenerator.MSAzureSynapse;

namespace SchemaGenerator.MSAzureSynapseForSDI
{
    public class MSAzureSynapseForSDISchemaGenerator(string dBType) : MSAzureSynapseSchemaGenerator(dBType)
    {
        protected override string GetCreateTableDDLSQLString(string tableName)
        {
            string SQLString = base.GetCreateTableDDLSQLString(tableName);
            TableSummary table = base._tableSummaries.Find(x => x.TableName == tableName)!;
            if (table.ExtensionData != null && base._tableSchemas.FindAll(x=>x.TableName==tableName && x.PK).Count==0)
            {
                ExtensionData extData = table.ExtensionData.Find(x => x.Key == "TableIndex");                
                if (extData is not null)
                    SQLString = SQLString.Replace("DISTRIBUTION = ROUND_ROBIN", $"DISTRIBUTION = HASH{extData.Value.Replace("UNIQUE PRIMARY INDEX", "").Replace("PRIMARY INDEX", "").Trim()}");
            }
            return SQLString;
        }
    }
}
