
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
// /// <summary>
// /// lua文件绘制方式
// /// </summary>
// [CustomAsset(".lua")]  //绘制方式绑定以.lua为后缀的资源文件
// public class LuaInspector : Editor
// {
//     private string content;
//     private bool show = false;
//     private string showKey = "LuaInspectorShown";
 
//     void OnEnable()
//     {
//         //Debug.Log(Application.dataPath +"   /   "+AssetDatabase.GetAssetPath(Selection.activeObject));
//         if (String.IsNullOrEmpty( AssetDatabase.GetAssetPath(Selection.activeObject)))
//         {
//             return;
//         }
//         string path = Application.dataPath + "/" + AssetDatabase.GetAssetPath(Selection.activeObject).Substring(7);
 
//         try
//         {
//             TextReader tr = new StreamReader(path);
//             content = tr.ReadToEnd(); //读取文件中所有文本
//             tr.Close();
//         }
//         catch (Exception e)
//         {
//             Debug.Log(e);
//         }
//     }
 
//     public override void OnInspectorGUI()
//     {
//         show = EditorPrefs.GetBool("showKey");
//         show = GUILayout.Toggle(show, "Show Lua Content");
//         EditorPrefs.SetBool("showKey", show);
//         if (show) //这里为了性能，加了开关
//         {
//             GUILayout.Label(content);
//         }
//     }
// }

[CanEditMultipleObjects, CustomAsset(".lua")]
public class LuaInspector : Editor
{
    private GUIStyle m_TextStyle;
 
    public override void OnInspectorGUI()
    {
        if (this.m_TextStyle == null)
        {
            this.m_TextStyle = "ScriptText";
        }
        bool enabled = GUI.enabled;
        GUI.enabled = true;
        string assetPath = AssetDatabase.GetAssetPath(target);
        if (assetPath.EndsWith(".lua"))
        {
            string luaFile = File.ReadAllText(assetPath);
            string text;
            if (base.targets.Length > 1)
            {
                text = Path.GetFileName(assetPath);
            }
            else
            {
                text = luaFile;
                if (text.Length > 7000)
                {
                    text = text.Substring(0, 7000) + "...\n\n<...etc...>";
                }
            }
            Rect rect = GUILayoutUtility.GetRect(new GUIContent(text), this.m_TextStyle);
            rect.x = 0f;
            rect.y -= 3f;
            rect.width = EditorGUIUtility.currentViewWidth + 1f;
            GUI.Box(rect, text, this.m_TextStyle);
        }
        GUI.enabled = enabled;
    }
}
