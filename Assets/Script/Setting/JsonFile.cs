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
        //string类型的数据常量
        string StringData;
        //获取到路径
        string fileUrl = Application.dataPath + FilePath;
        //读取文件

        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //数据保存
            StringData = sr.ReadToEnd();
            sr.Close();
        }
        //返回数据
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
            Debug.LogError("ERROR：读取ProgramSetting错误，文件丢失或内容错误。请检查/Asserts/Settings/Program Settings/Program Setting.json文件是否丢失或内容错误。");
            FlowControl.Exit();
            return new Dictionary<string, int> { } ;
        }

    }


}
