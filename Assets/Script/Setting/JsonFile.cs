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
    public static string ProjectSettingJsonPath = "/Settings/Program Setting/Program Setting.json";
    public static string ExperimentSettingJsonPath = "/Settings/Experiment Setting/Experiment Setting.json";
    public static string ReadJsonStrData(string FilePath)
    {
        //string���͵����ݳ���
        string StringData;
        //��ȡ��·��
        string fileUrl = Application.dataPath + FilePath;
        //��ȡ�ļ�

        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //���ݱ���
            StringData = sr.ReadToEnd();
            sr.Close();
        }
        //��������
        return StringData;
    }
    public static void WriteJsonStrDate(string FilePath, string JsonStr)
    {
        //��ȡ��·��
        string fileUrl = Application.dataPath + FilePath;
        using (StreamWriter sw = new(fileUrl, false, Encoding.Default))
        {
            //���ݱ���
            sw.Write(JsonStr);
            sw.Flush();
            sw.Close();
        }
    }
    public static Dictionary<string, int> ReadProgramSetting()
    {
        return ReadAndConvertJsonToStr(ProjectSettingJsonPath);
    }
    public static Dictionary<string, int> ReadExperimentSetting()
    {
        return ReadAndConvertJsonToStr(ExperimentSettingJsonPath);
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
            Debug.LogError("ERROR����ȡJson�����ļ���ʧ�����ݴ�������"+ "/Asserts/"+ filepath +"�ļ��Ƿ�ʧ�����ݴ���");
            FlowControl.Exit();
            return new Dictionary<string, int> { };
        }
    }
    public static void WriteProgramSetting(ProgramSetting PS)
    {
        // Debug Test Use
        // ProgramSetting PS = new();
        // PS.ResolutionX = 1920;
        // PS.ResolutionY = 1080;
        // PS.FPS = 60;
        // PS.IsFullScreen = 0;
        string JsonStr = JsonConvert.SerializeObject(PS, Newtonsoft.Json.Formatting.Indented);
        WriteJsonStrDate(ProjectSettingJsonPath, JsonStr);
    }
    public static void WriteExperimentSetting(ExperimentSetting ES)
    {
        // Debug Test Use 
        //ES = new();
        //ES.SubjectNum = 20;
        //ES.RoundPerSubject = 3;
        string JsonStr = JsonConvert.SerializeObject(ES, Newtonsoft.Json.Formatting.Indented);
        WriteJsonStrDate(ExperimentSettingJsonPath, JsonStr);
    }
    public class ProgramSetting
    {
        public int ResolutionX;
        public int ResolutionY;
        public int IsFullScreen;
        public int FPS;
    }
    public class ExperimentSetting
    {
        public int SubjectNum;
        public int RoundPerSubject;
    }
}
