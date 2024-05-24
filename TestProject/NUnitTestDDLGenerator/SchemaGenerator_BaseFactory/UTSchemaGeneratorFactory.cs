using SchemaGenerator.BaseFactory;


namespace TestProject1.SchemaGenerator_BaseFactory
{

    [TestFixture]
    public class UTSchemaGeneratorFactory
    {

        [TestCase("MSSQL")]
        public void CreateSchemaGeneratorInstance(string dbtype)
        {
            //Arrange:��l��
            SchemaGeneratorBase schemaGntr;

            //Act: �����k�B�欰�B�ާ@�è��o���G
            schemaGntr = new SchemaGeneratorFactory(dbtype).CreateSchemaGeneratorInstance();

            //Assert: ����            
            Assert.NotNull(schemaGntr);
            Console.WriteLine($"Instance >{schemaGntr.GetType().FullName}");
        }

    }
}