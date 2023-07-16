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
            WS_MainData.Cells[1, 4 + JsonFile.Setting["IndependentVarNum"]].Value = "X";
            WS_MainData.Cells[1, 4 + JsonFile.Setting["IndependentVarNum"]].Style.Fill.PatternType = ExcelFillStyle.Solid;
            WS_MainData.Cells[1, 4 + JsonFile.Setting["IndependentVarNum"]].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);

            // Fill Indexes
            for (int i = 0; i < JsonFile.Setting["SubjectNum"]* JsonFile.Setting["RoundsPerSubject"]; i++)
            {
                // Set Index
                WS_MainData.Cells[i + 2, 1].Value = i;
                // Set Subject Index
                WS_MainData.Cells[i + 2, 2].Value = i % JsonFile.Setting["SubjectNum"];
                // Set Rounds Index
                WS_MainData.Cells[i + 2, 3].Value = i % JsonFile.Setting["RoundsPerSubject"];
                // Set Depart Line Between Independent and Dependent Variables
                WS_MainData.Cells[i + 2, 4 + JsonFile.Setting["IndependentVarNum"]].Value = "X";
                WS_MainData.Cells[i + 2, 4+ JsonFile.Setting["IndependentVarNum"]].Style.Fill.PatternType = ExcelFillStyle.Solid;
                WS_MainData.Cells[i + 2, 4 + JsonFile.Setting["IndependentVarNum"]].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
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

            // Create Worksheets Array Of Group Data
            ExcelWorksheet[] WS_GroupData = new ExcelWorksheet[JsonFile.Setting["GroupNum"]];

            // Add a Main Data Sheet
            for (int i = 0; i < JsonFile.Setting["GroupNum"]; i++)
            {
                ExcelWorksheet WS_MainData = package.Workbook.Worksheets.Add("Group "+i.ToString()+" Data");
                
                // Init Write Main Data
                WS_MainData.Cells[1, 1].Value = "Index";
                WS_MainData.Cells[1, 2].Value = "Subject";

                // Fill Indexes
                
                for (int j = 0; j < (int)(JsonFile.Setting["SubjectNum"]); j++)
                {
                    // Set Index
                    WS_MainData.Cells[j + 2, 1].Value = j;
                    // Set Subject Index
                    WS_MainData.Cells[j + 2, 2].Value = j % JsonFile.Setting["SubjectNum"];
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

    public static void WriteRoundData(int SubjectInex, int RoundIndex)
    {

    }


}
