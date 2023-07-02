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
        try
        {
            string JsonStr = ReadJsonStrData("/Settings/Program Setting/Program Setting.json");
            Dictionary<string, int> Dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonStr);
            return Dict;

        }
        catch
        {
            Debug.LogError("ERROR����ȡProgramSetting�����ļ���ʧ�����ݴ�������/Asserts/Settings/Program Settings/Program Setting.json�ļ��Ƿ�ʧ�����ݴ���");
            FlowControl.Exit();
            return new Dictionary<string, int> { } ;
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
        WriteJsonStrDate("/Settings/Program Setting/Program Setting.json", JsonStr);
    }

}
public class ProgramSetting
{
    public int ResolutionX;
    public int ResolutionY;
    public int IsFullScreen;
    public int FPS;
    
}