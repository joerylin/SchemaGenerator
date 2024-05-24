using CommonLibrary;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchemaGenerator.BaseFactory.Services
{
    public class ExcelService
    {
        protected readonly string _filePath;
        protected IWorkbook _book;


        public ExcelService(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// 取得Excel Workbook
        /// </summary>
        /// <param name="filePath">檔案路徑，若無參數值預設取*.json設定檔ExcelPath路徑</param>       
        public IWorkbook GetWorkBook()
        {
            if (_book == null)
            {
                using (FileStream fStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    if (_filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                        _book = new XSSFWorkbook(fStream);
                    else
                        _book = new HSSFWorkbook(fStream);
                }
            }
            return _book;
        }

        public Boolean GetNullable(string? nullable, string? notNull)
        {
            //Nullable or Not Null 優化
            Boolean flag = true;

            if (nullable != null)
            {
                // Nullable 欄位值可能被設(Y、*、V、空白....)都視為可Nullable -> true，只有 (N、X)為Not Null  ->false
                nullable = DataTrans.CUpperString(nullable);
                flag = !( nullable == "N" || nullable == "X" );
            }
            else if (notNull != null)
            {
                // Not Nullable 欄位值可能被設(Y、*、V)都視為  Not Null -> false，只有 (N、X、"")為  非 Not Null  -> true
                notNull = DataTrans.CUpperString(notNull);
                flag = ( notNull == "N" || notNull == "X"  || notNull =="");
            }
            return flag;
        }

        #region For NPOI function

        public string? GetCellValue(IRow row, Int32? index)
        {
            if (index != null)
                return this.GetCellValue(row.GetCell(index.Value));
            else
                return null;
        }

        public string GetCellValue(ICell cell)
        {
            /*  ===== Specific Case =====
             *  1. if column has fuction like =A1+B1 , A1 is 735 ; B1 is 225 that get result is 735+225  -OK
             *  2. Can't provider to process *.xlsx -OK 
             *  3. Excel cell date type trans to DateTime string -OK use DataUtil class
             */
            if (cell == null)
                return null;
            CellType type = cell.CellType;
            if (type == CellType.Formula)
                type = cell.CachedFormulaResultType;

            switch (type)
            {
                case CellType.Numeric:
                    return p_get_cellNumerice_value(cell);
                case CellType.String:
                case CellType.Blank:
                    return p_Instead_NewLine(DataTrans.CString(cell.RichStringCellValue));
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:

                case CellType.Error:
                case CellType.Unknown:
                default:
                    return DataTrans.CString(cell);
            }
        }

        public Int32 GetLastCellNum(ISheet sheet)
        {
            Int32 lastCellNum = 0;
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                try
                {
                    if (sheet.GetRow(i).LastCellNum > lastCellNum)
                        lastCellNum = DataTrans.CInt32(sheet.GetRow(i).LastCellNum);
                }
                catch
                {
                    continue;
                }

            }
            return lastCellNum;
        }

        protected string p_get_cellNumerice_value(ICell cell)
        {
            string cellValue = string.Empty;

            try
            {
                if (!DateUtil.IsCellDateFormatted(cell))
                    cellValue = DataTrans.CString(DataTrans.CDecimal(cell.NumericCellValue));
                else
                    cellValue = DataTrans.CString(cell.DateCellValue);
            }
            catch
            {
                cellValue = DataTrans.CString(DataTrans.CDecimal(cell.NumericCellValue));
            }
            return cellValue;
        }

        protected string p_Instead_NewLine(string str)
        {
            try
            {
                str = str.Replace("\n", "");
            }
            catch
            {
                str = null;
            }
            return str;
        }


        #endregion
    }

}
