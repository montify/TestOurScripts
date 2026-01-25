//using NanoXLSX;

//namespace ExcelReader
//{
//    public class ExcelReader
//    {
//        public ExcelReader()
//        {
//            //Workbook wb;
//            //try
//            //{
//            //    CopyWorkBook();
//            //    wb = Workbook.Load("D:/FurnDebug/BLABLA_1.xlsx");
//            //}
//            //catch (Exception e)
//            //{
//            //    Console.WriteLine(e.Message);
//            //    return;
//            //}

//            //int startRow = 5;
//            //int endRow = 5;
//            //int startCol = 0;
//            //int endCol = 10;
//            //for (int r = startRow; r <= startRow; r++)
//            //for (int c = startCol; c <= endCol; c++)
//            //{
//            //    string address = NumberToColumn(c) + r;

//            //    if (wb.CurrentWorksheet.Cells.TryGetValue(address, out var cell))
//            //    {
//            //        Console.WriteLine($"{address} = {cell.Value}");
//            //    }
//            //}
//            //string workBookPath = "D:/FurnDebug/BLABLA_1.xlsx";

//            //var workBook = ReadWorkbook(workBookPath);
//        }

//        public void GetWorkSheedContent(string condition, string path)
//        {
//            var wb = ReadWorkbook(path);
//            var ws = wb.CurrentWorksheet;

//            // Get all used rows
//            var maxRowNumber = ws.GetLastColumnNumber();
//            var resultRows = new List<string>();

//            for (int i = 0; i < maxRowNumber; i++)
//            {
//                var row = ws.GetRow(i);

//                var firstCellValue = row[i].Value?.ToString();

//                if (!string.IsNullOrEmpty(firstCellValue) && firstCellValue.Contains(condition))
//                {
//                    resultRows.Add(firstCellValue);
//                }
//            }
//        }

//        private Workbook ReadWorkbook(string path)
//        {
//            try
//            {
//                CopyWorkBook();
//                return Workbook.Load("D:/FurnDebug/BLABLA_1.xlsx");
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        private void CopyWorkBook()
//        {
//            if (File.Exists("D:/FurnDebug/BLABLA_1.xlsx"))
//                File.Delete("D:/FurnDebug/BLABLA_1.xlsx");

//            if (File.Exists("D:/FurnDebug/BLABLA.xlsx"))
//                ;

//            File.Copy("D:/FurnDebug/BLABLA.xlsx", "D:/FurnDebug/BLABLA_1.xlsx");
//        }

//        int ColumnToNumber(string column)
//        {
//            int result = 0;
//            foreach (char c in column)
//            {
//                result = result * 26 + (c - 'A' + 1);
//            }
//            return result;
//        }

//        string NumberToColumn(int number)
//        {
//            string result = "";
//            while (number > 0)
//            {
//                number--;
//                result = (char)('A' + (number % 26)) + result;
//                number /= 26;
//            }
//            return result;
//        }

//        (int row, int col) ParseAddress(string address)
//        {
//            var letters = new string(address.TakeWhile(char.IsLetter).ToArray());
//            var numbers = new string(address.SkipWhile(char.IsLetter).ToArray());

//            return (int.Parse(numbers), ColumnToNumber(letters));
//        }
//    }
//}
