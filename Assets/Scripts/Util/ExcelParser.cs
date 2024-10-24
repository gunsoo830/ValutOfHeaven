using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using ExcelDataReader;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace VOHUtil
{
    public class ExcelParser
    {
        public enum ExcelDimension
        {
            None = -1,
            Row,
            Column,
        }
        
        
        private static ExcelParser instance = null;
        public static ExcelParser getInstance()
        {
            if (instance != null)
                return instance;

            instance = new ExcelParser();
            return instance;
        }
        public static T getDataWithType<T>(ref List<List<string>> excelData, int row, int col)
        {
            return (T)Convert.ChangeType(excelData[row][col], typeof(T));
        }


        private ExcelParser()
        {
            RootPath = Directory.GetCurrentDirectory();
        }

        private string RootPath = "";
        public List<List<string>> ParseExcel(string path, int sheetIndex, int startRow, int endRow, int startCol, int endCol)
        {
            return this._parseExcel(path, sheetIndex, startRow, endRow, startCol, endCol);
        }
        public List<List<string>> ParseExcel(string path, int sheetIndex, int row, int col)
        {
            return this._parseExcel(path, sheetIndex, row, row + 1, col, col+1);
        }
        
        public List<List<string>> ParseExcel(string path, int sheetIndex)
        {
            return this._parseExcel(path, sheetIndex);
        }
        
        public int GetExcelCount(string path, int sheetIndex, ExcelDimension dimensionType) => _getExcelCount(path, sheetIndex,dimensionType);
        private int _getExcelCount(string path, int sheetIndex, ExcelDimension dimensionType)
        {
            string realPath = RootPath + path;

            try
            {
                using (FileStream stream = File.Open(realPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        DataSet result = reader.AsDataSet();
                        if (sheetIndex <= result.Tables.Count)
                        {
                            DataTable Sheet = result.Tables[sheetIndex];
                            return dimensionType switch
                            {
                                ExcelDimension.Row => Sheet.Rows.Count,
                                ExcelDimension.Column => Sheet.Columns.Count,
                                _ => 0
                            };
                        }
                        else
                        {
                            throw new ArgumentException($"Sheet index {sheetIndex} , Total sheets: {result.Tables.Count}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to get excel row count: {ex.Message}");
                return 0;
            }
        }
        private List<List<string>> _parseExcel(string path, int sheetIndex, int startRow, int endRow, int startCol, int endCol)
        {
            List<List<string>> retVal = new List<List<string>>();

            string realPath = RootPath + path;

            try
            {
                using (FileStream stream = File.Open(realPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        DataSet result = reader.AsDataSet();
                        DataTable Sheet = result.Tables[sheetIndex];
                        for (int rowCount = startRow; rowCount < endRow; rowCount++)
                        {
                            List<string> rowData = new List<string>();
                            for (int colCount = startCol; colCount < endCol; colCount++)
                            {
                                rowData.Add(Sheet.Rows[rowCount][colCount].ToString());
                            }

                            retVal.Add(rowData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return retVal;
        }

        private List<List<string>> _parseExcel(string path, int sheetIndex)
        {
            List<List<string>> retVal = new List<List<string>>();

            string realPath = RootPath + path;

            try
            {
                using (FileStream stream = File.Open(realPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        DataSet result = reader.AsDataSet();
                        DataTable Sheet = result.Tables[sheetIndex];
                        for (int rowCount = 0; rowCount < Sheet.Rows.Count; rowCount++)
                        {
                            List<string> rowData = new List<string>();
                            for (int colCount = 0; colCount < Sheet.Columns.Count; colCount++)
                            {
                                rowData.Add(Sheet.Rows[rowCount][colCount].ToString());
                            }

                            retVal.Add(rowData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return retVal;
        }

        
        
    }
}
