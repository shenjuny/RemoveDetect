using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveDetect
{
    public class DBSet
    {
        public class CsvRow : List<string>
        {
            public string LineText { get; set; }
        }
        public class CsvFileReader : StreamReader
        {
            public CsvFileReader(Stream stream)
                : base(stream)
            {
            }

            public CsvFileReader(string filename)
                : base(filename)
            {
            }

            /// <summary>  
            /// Reads a row of data from a CSV file  
            /// </summary>  
            /// <param name="row"></param>  
            /// <returns></returns>  
            public bool ReadRow(CsvRow row)
            {
                row.LineText = ReadLine();
                if (String.IsNullOrEmpty(row.LineText))
                    return false;

                int pos = 0;
                int rows = 0;

                while (pos < row.LineText.Length)
                {
                    string value;

                    // Special handling for quoted field  
                    if (row.LineText[pos] == '"')
                    {
                        // Skip initial quote  
                        pos++;

                        // Parse quoted value  
                        int start = pos;
                        while (pos < row.LineText.Length)
                        {
                            // Test for quote character  
                            if (row.LineText[pos] == '"')
                            {
                                // Found one  
                                pos++;

                                // If two quotes together, keep one  
                                // Otherwise, indicates end of value  
                                if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }
                        value = row.LineText.Substring(start, pos - start);
                        value = value.Replace("\"\"", "\"");
                    }
                    else
                    {
                        // Parse unquoted value  
                        int start = pos;
                        while (pos < row.LineText.Length && row.LineText[pos] != ',')
                            pos++;
                        value = row.LineText.Substring(start, pos - start);
                    }

                    // Add field to list  
                    if (rows < row.Count)
                        row[rows] = value;
                    else
                        row.Add(value);
                    rows++;

                    // Eat up to and including next comma  
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    if (pos < row.LineText.Length)
                        pos++;
                }
                // Delete any unused items  
                while (row.Count > rows)
                    row.RemoveAt(rows);

                // Return true if any columns read  
                return (row.Count > 0);
            }
        }
        /// <summary>
        /// 读取CSV文件到DataTable中
        /// </summary>
        /// <param name="filePath">CSV的文件路径</param>
        /// <returns></returns>
        public static DataTable ReadCSV(string filePath)
        {
            DataTable dt = new DataTable();
            int lineNumber = 0;
            using (CsvFileReader reader = new CsvFileReader(filePath))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {

                    if (0 == lineNumber)
                    {
                        foreach (string s in row)
                        {
                            dt.Columns.Add(s.Replace("\"", ""));
                        }
                    }
                    else
                    {
                        int index = 0;
                        DataRow dr = dt.NewRow();
                        foreach (string s in row)
                        {
                            dr[index] = s.Replace("\"", "");
                            index++;
                        }
                        dt.Rows.Add(dr);
                    }
                    lineNumber++;
                }
                reader.Close();
            }

            return dt;
        }
        ////导入CSV文件
        //public static System.Data.DataTable CSV2DataTable(string fileName)
        //{

        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //    StreamReader sr = new StreamReader(fs, new System.Text.UnicodeEncoding());
        //    //记录每次读取的一行记录
        //    string strLine = "";
        //    //记录每行记录中的各字段内容
        //    string[] aryLine;
        //    //标示列数
        //    int columnCount = 0;
        //    //标示是否是读取的第一行
        //    bool IsFirst = true;

        //    //逐行读取CSV中的数据
        //    while ((strLine = sr.ReadLine()) != null)
        //    {
        //        aryLine = strLine.Split(',');
        //        if (IsFirst == true)
        //        {
        //            IsFirst = false;
        //            columnCount = aryLine.Length;
        //            //创建列
        //            for (int i = 0; i < columnCount; i++)
        //            {
        //                DataColumn dc = new DataColumn(aryLine[i]);
        //                dt.Columns.Add(dc);
        //            }
        //        }
        //        else
        //        {
        //            DataRow dr = dt.NewRow();
        //            for (int j = 0; j < columnCount; j++)
        //            {
        //                dr[j] = aryLine[j];
        //            }
        //            dt.Rows.Add(dr);
        //        }
        //    }

        //    sr.Close();
        //    fs.Close();
        //    return dt;
        //}
        ///// <summary>
        ///// 读取CSV
        ///// </summary>
        ///// <param name="filePath">CSV路径</param>
        ///// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        ///// <returns></returns>
        //public static System.Data.DataTable CsvToDataTable(string filePath, int n)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default, false);
        //    int m = 0;
        //    while (!reader.EndOfStream)
        //    {
        //        m = m + 1;
        //        string str = reader.ReadLine();
        //        string[] split = str.Split(',');
        //        if (m == n)
        //        {
        //            System.Data.DataColumn column; //列名
        //            for (int c = 0; c < split.Length; c++)
        //            {
        //                column = new System.Data.DataColumn();
        //                column.DataType = System.Type.GetType("System.String");
        //                column.ColumnName = split[c];
        //                if (dt.Columns.Contains(split[c]))                 //重复列名处理
        //                    column.ColumnName = split[c] + c;
        //                dt.Columns.Add(column);
        //            }
        //        }
        //        if (m >= n + 1)
        //        {
        //            System.Data.DataRow dr = dt.NewRow();
        //            for (int i = 0; i < split.Length; i++)
        //            {
        //                dr[i] = split[i];
        //            }
        //            dt.Rows.Add(dr);
        //        }
        //    }
        //    reader.Close();
        //    return dt;
        //}
        /// <summary>
        /// DataTable导出为CSV
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">路径</param>
        public static void dataTableToCsvT(System.Data.DataTable dt, string strFilePath)
        {
            if (dt == null || dt.Rows.Count == 0)   //确保DataTable中有数据
            {
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);//System.Text.Encoding.Default
                strmWriterObj.WriteLine("");
                strmWriterObj.Close();
            }
            else
            {
                string strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);//System.Text.Encoding.Default
                                                                                                             //写入列头
                foreach (System.Data.DataColumn col in dt.Columns)
                    strBufferLine += col.ColumnName + ",";
                strBufferLine = strBufferLine.Substring(0, strBufferLine.Length - 1);
                strmWriterObj.WriteLine(strBufferLine);
                //写入记录
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += dt.Rows[i][j].ToString().Replace(",", "");   //因为CSV文件以逗号分割，在这里替换为空
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
            }
        }
    }
}
