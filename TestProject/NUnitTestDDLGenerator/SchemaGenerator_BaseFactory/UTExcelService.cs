using NPOI.SS.UserModel;
using SchemaGenerator.BaseFactory.Services;

namespace TestProject1.SchemaGenerator_BaseFactory
{

    [TestFixture]
    public class UTExcelService : NUnitBase
    {
        private readonly string _filePath;
        private ExcelService instance;

        public UTExcelService()
        {
            this._filePath = base.BaseTemplatePath;
        }

        [TestCase("SDM_MIS.xlsx")]
        [TestCase("TableSchema.xls")]
        [Order(0)]
        public void GetWorkBook_InputFilePath_ReturnWorkbook(string filename)
        {
            //Arrange:��l��
            ExcelService excelService = new ExcelService($"{_filePath}{filename}");

            //Act: �����k�B�欰�B�ާ@�è��o���G
            IWorkbook workbook = excelService.GetWorkBook();

            //Assert: ����
            Console.WriteLine($"Workbook HashCode:{workbook.GetHashCode()}");
            Assert.NotNull(workbook);
        }

        [TestCase("SDM_MIS.xlsx")]
        [TestCase("TableSchema.xls")]
        [Order(1)]
        public void GetWorkSheet_InputFilePath_ReturnWorkSheetCnt(string filename)
        {
            //Arrange:��l��
            ExcelService excelService = new ExcelService($"{_filePath}{filename}");

            //Act: �����k�B�欰�B�ާ@�è��o���G
            IWorkbook workbook = excelService.GetWorkBook();

            //Assert: ����
            Console.WriteLine($"Work sheet  Cnt:{workbook.NumberOfSheets}");
            Assert.Greater(workbook.NumberOfSheets, 0);
        }

        [TestCase("SDM_MIS.xlsx", "ReadMe", 3)]
        [TestCase("SDM_MIS.xlsx", "SDM-Summary", 16)]
        [TestCase("SDM_MIS.xlsx", "SDM-Detail", 11)]
        [TestCase("TableSchema.xls", "Change_List", 4)]
        [TestCase("TableSchema.xls", "MART_Table�`��", 17)]
        [Order(2)]
        public void GetLastCellNum_ExcelInfo_ReturnLastCellNumber(string filename, string sheetName, int ansLastNumber)
        {
            //Arrange:��l��
            int expectValue = ansLastNumber;
            int lastNumber = 0;

            ExcelService excelService = new ExcelService($"{_filePath}{filename}");

            //Act: �����k�B�欰�B�ާ@�è��o���G
            IWorkbook workbook = excelService.GetWorkBook();
            lastNumber = excelService.GetLastCellNum(workbook.GetSheet(sheetName));

            //Assert: ����
            Console.WriteLine($"Expect last cell is {expectValue}�Athe last number of this sheet is{workbook.NumberOfSheets}");
            Assert.GreaterOrEqual(lastNumber, expectValue);
        }


        [TestCase("SDM_MIS.xlsx", "ReadMe", 0, 0, "\"SDM-Summary\" SheetColumn List")]
        [TestCase("SDM_MIS.xlsx", "ReadMe", 1, 0, "PDM Target Table Area:")]
        [TestCase("SDM_MIS.xlsx", "ReadMe", 1, 1, null)]
        [TestCase("SDM_MIS.xlsx", "SDM-Detail", 0, 4, "")]
        [TestCase("SDM_MIS.xlsx", "SDM-Detail", 4, 0, "80")]
        [TestCase("SDM_MIS.xlsx", "SDM-Detail", 5, 12, ",in_usr_id  VARCHAR(20)")]
        [Order(3)]
        public void GetCellValue_ExcelInfo_ReturnCellValue(string filename, string sheetName, int rowIndex, int colIndex, object value)
        {
            //Arrange:��l��
            object expectValue = value;
            object result;

            ExcelService excelService = new ExcelService($"{_filePath}{filename}");

            //Act: �����k�B�欰�B�ާ@�è��o���G
            IWorkbook workbook = excelService.GetWorkBook();
            result = excelService.GetCellValue(workbook.GetSheet(sheetName).GetRow(rowIndex).GetCell(colIndex));

            //Assert: ����
            Console.WriteLine($"Expect  is {expectValue}�Athe cell value is{result}");
            Assert.AreEqual(result, expectValue);
        }

        [TestCase("y", null, true)]
        [TestCase("Y", null, true)]
        [TestCase("v", null, true)]
        [TestCase("V", null, true)]
        [TestCase("*", null, true)]
        [TestCase("", null, true)]
        [TestCase("x", null, false)]        
        [TestCase("X", null, false)]
        [TestCase(null, null, true)]
        [TestCase(null, "y", false)]
        [TestCase(null, "Y", false)]
        [TestCase(null, "v", false)]
        [TestCase(null, "V", false)]
        [TestCase(null, "", true)]
        [TestCase(null, "*", false)]
        [TestCase(null, "x", true)]
        [TestCase(null, "X", true)]
        public void GetNullable_��JNullorNotNull_�P�_�O�_��Nullable(string? strNull, string? strNotNull, Boolean expFlag)
        {
            //Arrange:��l��
            Boolean flag;
            ExcelService excelService = new ExcelService("");

            //Act: �����k�B�欰�B�ާ@�è��o���G
            flag = excelService.GetNullable(strNull, strNotNull);

            //Assert: ����
            Console.WriteLine($"Expect  is {expFlag}�Athe cell value is{flag}");
            Assert.AreEqual(expFlag, flag);
        }
    }
}