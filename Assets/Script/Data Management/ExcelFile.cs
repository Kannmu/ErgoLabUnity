using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System;
using OfficeOpenXml.Style;
using Unity.VisualScripting;

public static class ExcelFile
{
    public static string ExperimentName = "Default";
    
    // Create New Excel File, Within Subject Experiment Type
    public static void CreateWithinExcelFile(string ExpName)
    {
        ExperimentName = ExpName;
        try
        {
            // Get File Path
            string ExcelFilePath = Application.dataPath+ "/Data/Experiments Data/" + ExpName + "/" + ExpName + ".xlsx";
            FileInfo fileInfo = new(ExcelFilePath);

            // Create Target Package
            ExcelPackage package = new(fileInfo);

            // Read Setting Data
            Dictionary<string, int> Settintg = JsonFile.ReadSetting(ExpName);

            // Add a Main Data Sheet
            ExcelWorksheet WS_MainData = package.Workbook.Worksheets.Add("Main Data");
            ExcelWorksheet WS_Info = package.Workbook.Worksheets.Add("Information");

            

            // Get Current Date And Time
            DateTime CurrentTime = DateTime.Now;

            // Write Information Data
            // Write Experiment Time Info
            WS_Info.Cells[1, 1].Value = "Experiment Date and Time";
            WS_Info.Cells[1, 1].AutoFitColumns();
            WS_Info.Cells[1, 2].Value = CurrentTime.ToString();
            WS_Info.Cells[1, 2].AutoFitColumns();

            // Init Write Main Data
            // Init Colomun Head
            WS_MainData.Cells[1,1].Value = "Index";
            WS_MainData.Cells[1,2].Value = "Subject";
            WS_MainData.Cells[1,3].Value = "Round";
            WS_MainData.Cells[1, 4 + Settintg["IndependentVarNum"]].Value = "X";
            WS_MainData.Cells[1, 4 + Settintg["IndependentVarNum"]].Style.Fill.PatternType = ExcelFillStyle.Solid;
            WS_MainData.Cells[1, 4 + Settintg["IndependentVarNum"]].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);

            // Fill Indexes
            for (int i = 0; i < Settintg["SubjectNum"]* Settintg["RoundsPerSubject"]; i++)
            {
                // Set Index
                WS_MainData.Cells[i + 2, 1].Value = i;
                // Set Subject Index
                WS_MainData.Cells[i + 2, 2].Value = i % Settintg["SubjectNum"];
                // Set Rounds Index
                WS_MainData.Cells[i + 2, 3].Value = i % Settintg["RoundsPerSubject"];
                // Set Depart Line Between Independent and Dependent Variables
                WS_MainData.Cells[i + 2, 4 + Settintg["IndependentVarNum"]].Value = "X";
                WS_MainData.Cells[i + 2, 4+ Settintg["IndependentVarNum"]].Style.Fill.PatternType = ExcelFillStyle.Solid;
                WS_MainData.Cells[i + 2, 4 + Settintg["IndependentVarNum"]].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
            }

            // Save Excel File
            package.Save();
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR: Failed to Create Excel File, Check If The .xlsx File Already Exist");
            Debug.LogError(ex.Message);
            FlowControl.Exit();
        }

    }

    // Create New Excel File, Between Subject Experiment Type
    public static void CreateBetweenExcelFile(string ExpName)
    {
        ExperimentName = ExpName;
        try
        {
            // Get File Path
            string ExcelFilePath = Application.dataPath + "/Data/Experiments Data/" + ExpName + "/" + ExpName + ".xlsx";
            FileInfo fileInfo = new(ExcelFilePath);

            // Create Target Package
            ExcelPackage package = new(fileInfo);

            // Read Setting Data
            Dictionary<string, int> Settintg = JsonFile.ReadSetting(ExpName);

            // Create Worksheets Array Of Group Data
            ExcelWorksheet[] WS_GroupData = new ExcelWorksheet[Settintg["GroupNum"]];

            // Add a Main Data Sheet
            for (int i = 0; i < Settintg["GroupNum"]; i++)
            {
                ExcelWorksheet WS_MainData = package.Workbook.Worksheets.Add("Group "+i.ToString()+" Data");
                
                // Init Write Main Data
                WS_MainData.Cells[1, 1].Value = "Index";
                WS_MainData.Cells[1, 2].Value = "Subject";

                // Fill Indexes
                
                for (int j = 0; j < (int)(Settintg["SubjectNum"]); j++)
                {
                    // Set Index
                    WS_MainData.Cells[j + 2, 1].Value = j;
                    // Set Subject Index
                    WS_MainData.Cells[j + 2, 2].Value = j % Settintg["SubjectNum"];
                }
                WS_GroupData[i] = WS_MainData;
            }

            ExcelWorksheet WS_Info = package.Workbook.Worksheets.Add("Information");

            // Get Current Date And Time
            DateTime CurrentTime = DateTime.Now;

            // Write Information Data
            WS_Info.Cells[1, 1].Value = "Experiment Date and Time";
            WS_Info.Cells[1, 1].AutoFitColumns();
            WS_Info.Cells[1, 2].Value = CurrentTime.ToString();
            WS_Info.Cells[1, 2].AutoFitColumns();

            // Save Excel File
            package.Save();
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR: Failed to Create Excel File, Check If The .xlsx File Already Exist");
            Debug.LogError(ex.Message);
            FlowControl.Exit();
        }
    }


}
