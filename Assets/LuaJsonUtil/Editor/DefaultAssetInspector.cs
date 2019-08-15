using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefaultAsset))]
public class DefaultAssetInspector : Editor
{
    private Editor editor;
    private static Type[] customAssetTypes;

    [InitializeOnLoadMethod]
    static void Init()
    {
        customAssetTypes = GetCustomAssetTypes();
    }

    /// <summary>
    /// CustomAsset 属性のついたクラスを取得する
    /// </summary>
    private static Type[] GetCustomAssetTypes()
    {
        // ユーザーの作成した DLL 内から取得する
        var assemblyPaths = Directory.GetFiles("Library/ScriptAssemblies", "*.dll");
        var types = new List<Type>();
        var customAssetTypes = new List<Type>();

        foreach (var assembly in assemblyPaths
            .Select(assemblyPath => Assembly.LoadFile(assemblyPath)))
        {
            types.AddRange(assembly.GetTypes());
        }

        foreach (var type in types)
        {
            var customAttributes =
                type.GetCustomAttributes(typeof(CustomAssetAttribute), false)
                                                      as CustomAssetAttribute[];

            if (0 < customAttributes.Length)
                customAssetTypes.Add(type);
        }
        return customAssetTypes.ToArray();
    }

    /// <summary>
    /// 拡張子に対応した CustomAsset 属性のついたクラスを取得する
    /// </summary>
    /// <param name="extension">拡張子（例: .zip）</param>
    private Type GetCustomAssetEditorType(string extension)
    {
        foreach (var type in customAssetTypes)
        {
            var customAttributes =
              type.GetCustomAttributes(typeof(CustomAssetAttribute), false)
                                                      as CustomAssetAttribute[];

            foreach (var customAttribute in customAttributes)
            {
                if (customAttribute.extensions.Contains(extension))
                    return type;
            }
        }
        return typeof(DefaultAsset);
    }

    private void OnEnable()
    {
        var assetPath = AssetDatabase.GetAssetPath(target);

        var extension = Path.GetExtension(assetPath);
        var customAssetEditorType = GetCustomAssetEditorType(extension);
        //editor = CreateEditor(target, customAssetEditorType);
    }

    public override void OnInspectorGUI()
    {
        if (editor != null)
        {
            GUI.enabled = true;
            editor.OnInspectorGUI();
        }
    }

    public override bool HasPreviewGUI()
    {
        return editor != null ? editor.HasPreviewGUI() : base.HasPreviewGUI();
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        if (editor != null)
            editor.OnPreviewGUI(r, background);
    }

    public override void OnPreviewSettings()
    {
        if (editor != null)
            editor.OnPreviewSettings();
    }

    public override string GetInfoString()
    {
        return editor != null ? editor.GetInfoString() : base.GetInfoString();
    }

    //以下、任意で扱いたい Editor クラスの拡張を行う
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CustomAssetAttribute : Attribute {

public string[] extensions; //不同属性对应不同需求，同一需求可能有几种类型文件
 
    public CustomAssetAttribute(params string[] extention)
    {
        this.extensions = extention;
    }

}