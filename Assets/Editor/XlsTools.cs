
//using Excel;
//using System;
//using System.Data;
//using System.IO;
//using System.Text;
//using System.Xml;
//using UnityEditor;
//using UnityEngine;
//public class XlsTools
//{
//    [MenuItem("AssetBundle/ReadBuffXls &F2", false)]
//    public static void ReadBuffXls()
//    {
//        FileInfo info = new FileInfo(Application.dataPath + "/../../../../cspublic/ExcelTool/T_Buff.xls");
//        Logger.Log(info.DirectoryName);
//        FileStream stream = File.Open(info.DirectoryName + "\\T_Buff.xls", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
//        IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
//        DataSet result = excelReader.AsDataSet();
//        Logger.Log(111);
//        int columns = result.Tables[0].Columns.Count;
//        int rows = result.Tables[0].Rows.Count;
//        string str = "";
//        for (int i = 0; i < rows; i++)
//        {
//            for (int j = 0; j < columns; j++)
//            {
//                string nvalue = result.Tables[0].Rows[i][j].ToString();
//                if (i > 0)
//                {
//                    str += nvalue + "\t";
//                }
//                else
//                {
//                    str += nvalue + "\t";
//                }
//            }
//            str += "\n";
//        }
//        CopyToClipboard(str);
//        stream.Close();
//    }
//    public static void CopyToClipboard(string data)
//    {
//        TextEditor te = new TextEditor();
//        te.text = data;
//        te.SelectAll();
//        te.Copy();
//    }

//    public void WriteExcel(DataSet ds, string path)
//    {

//        StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));//打开写文件流
//        StringBuilder sb = new StringBuilder();

//        for (int k = 0; k < ds.Tables.Count; k++)
//        {
//            DataTable tables = ds.Tables[k];
//            long totalCount = tables.Rows.Count;
//            for (int i = 0; i < tables.Rows.Count; i++)//遍历每行内容
//            {
//                for (int j = 0; j < tables.Columns.Count; j++)//一行中的每列
//                {
//                    sb.Append(tables.Rows[i][j].ToString() + "\t");//每个单元格内容，加到StringBuilder中
//                }
//                sb.Append(Environment.NewLine);
//            }
//            sb.Append(Environment.WorkingSet);
//        }
//        sw.Write(sb);//文件流写入内容
//        sw.Flush();
//        sw.Close();
//    }
//}
