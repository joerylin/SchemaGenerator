using SchemaGenerator.BaseFactory;


namespace TestProject1.SchemaGenerator_BaseFactory
{

    [TestFixture]
    public class UTSchemaGeneratorFactory
    {

        [TestCase("MSSQL")]
        public void CreateSchemaGeneratorInstance(string dbtype)
        {
            //Arrange:初始化
            SchemaGeneratorBase schemaGntr;

            //Act: 執行方法、行為、操作並取得結果
            schemaGntr = new SchemaGeneratorFactory(dbtype).CreateSchemaGeneratorInstance();

            //Assert: 驗證            
            Assert.NotNull(schemaGntr);
            Console.WriteLine($"Instance >{schemaGntr.GetType().FullName}");
        }

    }
}