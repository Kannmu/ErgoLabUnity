using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System;

public static class ExcelFile
{
    // Create New Excel File
    public static void CreateExcelFile(string filePath)
    {
        try
        {
            // Get File Path
            string ExcelFilePath = Application.dataPath+ "/Data/Experiments Data/" + filePath + ".xlsx";
            FileInfo fileInfo = new(ExcelFilePath);

            // Target File
            ExcelPackage package = new(fileInfo);

            // Add a Main Data Sheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Main Data");


            Debug.Log("Write Experiment: " + filePath);

            // Write Experiment Title
            worksheet.Cells["A1"].Value = filePath;
            
            // Save Excel File
            package.Save(); 
        }
        catch
        {
            Debug.LogError("ERROR: Failed to Create Excel File, Check If The .xlsx File Already Exist");
            FlowControl.Exit();
        }

    }
    
    
    
    
    public static string MidStrEx(string sourse, string startstr, string endstr)
    {
        // Extract Strings Bettwn startstr and endstr in sourse
        string result = string.Empty;
        int startindex, endindex;
        try
        {
            startindex = sourse.IndexOf(startstr);
            if (startindex == -1)
                return result;
            string tmpstr = sourse.Substring(startindex + startstr.Length);
            endindex = tmpstr.IndexOf(endstr);           
            if (endindex == -1)
                return result;
            result = tmpstr.Remove(endindex);
        }
        catch (Exception ex)
        {
                Debug.Log("MidStrEx Err:" + ex.Message);
        }
        return result;
    }    
}
