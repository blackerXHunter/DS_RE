using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Unity Editor 下右键创建文本类文件
/// </summary>
public class CreateFileEditor : Editor
{
    [MenuItem("Assets/Create/Lua File")]
    static void CreateLuaFile()
    {
        CreateFile("lua");
    }

    [MenuItem("Assets/Create/Text File")]
    static void CreateTextFile()
    {
        CreateFile("txt");
    }

    [MenuItem("Assets/Create/Json File")]
    static void CreateJsonFile()
    {
        CreateFile("json");
    }

    /// <summary>
    /// 创建文件类的文件
    /// </summary>
    /// <param name="fileEx"></param>
    static void CreateFile(string fileEx)
    {
        //获取当前所选择的目录（相对于Assets的路径）
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var path = Application.dataPath.Replace("Assets", "") + "/";
        var newFileName = NewFileName(fileEx);
        var newFilePath = selectPath + "/" + newFileName;
        var fullPath = path + newFilePath;
        int fileIndex = 1;
        //简单的重名处理
        while (File.Exists(fullPath))
        {
            fileIndex++;
            var newName = NewFileName(fileEx, fileIndex);
            newFilePath = selectPath + "/" + newName;
            fullPath = path + newFilePath;
        }
        if (fileEx == "lua")
        {
            //如果是空白文件，编码并没有设成UTF-8
            File.WriteAllText(fullPath, "-- Created in " + DateTime.Now, Encoding.UTF8);
        }
        else
        {
            File.WriteAllText(fullPath, "", Encoding.UTF8);
        }

        AssetDatabase.Refresh();

        //选中新创建的文件
        var asset = AssetDatabase.LoadAssetAtPath(newFilePath, typeof(UnityEngine.Object));
        Selection.activeObject = asset;
    }

    static string NewFileName(string fileEx, int index = 1)
    {
        if (index > 1)
        {
            return "new_" + fileEx + index.ToString() + "." + fileEx;
        }
        else
        {
            return "new_" + fileEx + "." + fileEx;
        }
    }
}