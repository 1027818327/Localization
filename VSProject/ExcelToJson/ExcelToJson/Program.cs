using Aspose.Cells;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelToJson
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件 |*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string excelFileName = ofd.FileName;
                //Console.WriteLine(excelFileName);

                DirectoryInfo tempDi = new DirectoryInfo(excelFileName);
                //Console.WriteLine(tempDi.Parent.FullName);

                string tempOutPath = tempDi.Parent.FullName;

                Workbook workbook = new Workbook(excelFileName);
                Cells cells = workbook.Worksheets[0].Cells;
                /// 获取语言

                MultipleLanguageData[] tempChineseArray = new MultipleLanguageData[cells.MaxDataRow - 1];  // 中文
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string s = cells[1, j].StringValue.Trim();
                    if (s.Equals("Chinese"))
                    {
                        Console.WriteLine(s);

                        for (int i = 0; i < tempChineseArray.Length; i++)
                        {
                            string tempKey = cells[i + 2, j].StringValue.Trim();
                            tempChineseArray[i] = new MultipleLanguageData(tempKey, tempKey);

                            Console.WriteLine("key={0}, value={1}", tempChineseArray[i].key, tempChineseArray[i].value);
                        }

                        string[] tempJsonArray = GetJsonArray(tempChineseArray);
                        File.WriteAllLines(Path.Combine(tempOutPath, s + ".json"), tempJsonArray);
                        break;
                    }
                }

                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string s = cells[1, j].StringValue.Trim();
                    if (s.Equals("Chinese") == false)
                    {
                        Console.WriteLine(s);
                        MultipleLanguageData[] tempOtherArray = new MultipleLanguageData[cells.MaxDataRow - 1];  // 其他语言

                        for (int i = 0; i < tempOtherArray.Length; i++)
                        {
                            string tempValue = cells[i + 2, j].StringValue.Trim();
                            tempOtherArray[i] = new MultipleLanguageData(tempChineseArray[i].key, tempValue);

                            Console.WriteLine("key={0}, value={1}", tempOtherArray[i].key, tempOtherArray[i].value);
                        }

                        string[] tempJsonArray = GetJsonArray(tempOtherArray);
                        File.WriteAllLines(Path.Combine(tempOutPath, s + ".json"), tempJsonArray);

                        break;
                    }
                }
            }


        }

        static string[] GetJsonArray(MultipleLanguageData[] varArray)
        {
            string[] tempJsonArray = new string[4 * varArray.Length + 2];
            tempJsonArray[0] = "[";
            tempJsonArray[tempJsonArray.Length - 1] = "]";
            for (int i = 0; i < varArray.Length; i++)
            {
                tempJsonArray[i * 4 + 1] = "  {";
                tempJsonArray[i * 4 + 2] = "    \"key\":" + "\"" + varArray[i].key + "\",";
                tempJsonArray[i * 4 + 3] = "    \"value\":" + "\"" + varArray[i].value + "\"";
                if (i == varArray.Length - 1)
                {
                    tempJsonArray[i * 4 + 4] = "  }";
                }
                else
                {
                    tempJsonArray[i * 4 + 4] = "  },";
                }
            }
            return tempJsonArray;
        }
    }
}
