using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.Text;

public static class JsonFile
{
    public static string ReadJsonStrData(string FilePath)
    {
        //string 
        string StringData;
        
        // Get Json File Path
        string fileUrl = Application.dataPath + FilePath;
        
        // Read File
        using (StreamReader sr = File.OpenText(fileUrl))
        {
            StringData = sr.ReadToEnd();
            sr.Close();
        }
        return StringData;
    }
    public static void WriteJsonStrDate(string FilePath, string JsonStr)
    {
        // Get Json File Path
        string fileUrl = Application.dataPath + FilePath;
        using (StreamWriter sw = new(fileUrl, false, Encoding.Default))
        {
            sw.Write(JsonStr);
            sw.Flush();
            sw.Close();
        }
    }
    public static Dictionary<string, int> ReadAndConvertJsonToStr(string filepath)
    {
        try
        {
            string JsonStr = ReadJsonStrData(filepath);
            Dictionary<string, int> Dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonStr);
            return Dict;
        }
        catch
        {
            Debug.LogError("ERROR: Failed to read JSON, file missing or content incorrect. Please check if the " + "/Asserts/" + filepath + " file is missing or has incorrect content.");
            FlowControl.Exit();
            return new Dictionary<string, int> { };
        }
    }
    public static Dictionary<string, int> ReadSetting(string ExperimentName)
    {
        return ReadAndConvertJsonToStr("/Data/Experiments Data/" + ExperimentName + "/" + "Setting.json");
    }
    public static Dictionary<string, int> ReadProgress(string ExperimentName)
    {
        return ReadAndConvertJsonToStr("/Data/Experiments Data/" + ExperimentName + "/" + "Progress.json");
    }
    public static void WriteSetting(string ExperimentName,Setting ST)
    {
        // Debug Test Use
        //Setting PS = new()
        //{
        //    ResolutionX = 1920,
        //    ResolutionY = 1080,
        //    FPS = 60,
        //    IsFullScreen = 0,
        //    ExperimentType = 0,
        //    SubjectNum = 20,
        //    RoundsPerSubject = 3
        //};
        string JsonStr = JsonConvert.SerializeObject(ST, Newtonsoft.Json.Formatting.Indented);
        WriteJsonStrDate("/Data/Experiments Data/" + ExperimentName + "/" + "Setting.json", JsonStr);
     
    }
    public static Dictionary<string, int> WriteProgress(string ExperimentName,int TotalPresentedRounds, int SubjectIndex, int RoundIndex, int GroupIndex)
    {
        Progress PG = new();
        PG.TotalPresentedRounds = TotalPresentedRounds;
        PG.CurrentSubjectIndex = SubjectIndex;
        PG.CurrentRoundIndex = RoundIndex;
        PG.CurrentGroupIndex = GroupIndex;
        string JsonStr = JsonConvert.SerializeObject(PG, Newtonsoft.Json.Formatting.Indented);
        WriteJsonStrDate("/Data/Experiments Data/" + ExperimentName + "/" + "Progress.json", JsonStr);
        Dictionary<string, int> Dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonStr);
        return Dict;
    }
    
    //public static void SetDefaultSetting(string ExperimentName)
    //{
    //    Setting ST = new()
    //    {
    //        ResolutionX = 1920,
    //        ResolutionY = 1080,
    //        FPS = 60,
    //        IsFullScreen = 0,
    //        ExperimentType = 0,
    //        SubjectNum = 20,
    //        RoundsPerSubject = 3,
    //        IndependentVarNum = 2,
    //        DependentVarNum = 4,
    //        GropNum = 3
    //    };
    //    WriteSetting(ExperimentName,ST);
    //}

    public static void SetDefaultProgress(string ExperimentName)
    {
        _ = WriteProgress(ExperimentName,0, 0, 0, 0);
    }
    public class Setting
    {
        // Define Setting Param Here, Int Type Only
        public int ResolutionX;
        public int ResolutionY;
        public int IsFullScreen;
        public int FPS;
        public int ExperimentType;
        public int SubjectNum;
        public int RoundsPerSubject;
        public int IndependentVarNum;
        public int DependentVarNum;
        public int GroupNum;
    }
    public class Progress
    {
        // Define Progress Param Here, Int Type Only
        public int TotalPresentedRounds;
        public int CurrentSubjectIndex;
        public int CurrentRoundIndex;
        public int CurrentGroupIndex;
    }
}
