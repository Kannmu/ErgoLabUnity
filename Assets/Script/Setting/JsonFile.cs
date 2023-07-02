using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Unity.VisualScripting;

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


}
