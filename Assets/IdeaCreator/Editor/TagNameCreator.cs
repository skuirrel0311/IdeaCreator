﻿using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditorInternal;
using System.Collections.Generic;
using System;

public class TagNameCreator
{
    private const string TAGNAME_HASH_KEY = "TagName_Hash";

    [MenuItem("Assets/TagNameCreator")]
    public static void _TagNameCreator()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
            return;

        BuildTagName();
    }

    static TagNameCreator()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
            return;

        BuildTagName();
    }

    static void BuildTagName()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        builder = WriteTagManagerClass(builder);


        string text = builder.ToString().Replace(",}", "}");
        string assetPath = currentFolderPath + "TagName.cs";

        //if (AssetDatabase.LoadAssetAtPath(assetPath.Replace("/Editor/..", ""), typeof(UnityEngine.Object)) != null && EditorPrefs.GetInt(TAGNAME_HASH_KEY, 0) == text.GetHashCode())
        //    return;

        System.IO.File.WriteAllText(assetPath, text);
        EditorPrefs.SetInt(TAGNAME_HASH_KEY, text.GetHashCode());
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    static System.Text.StringBuilder WriteTagManagerClass(System.Text.StringBuilder builder)
    {
        List<string> tagNames = InternalEditorUtility.tags.ToList();

        builder.AppendLine("public class TagName");
        builder.AppendLine("{");

        {
            WriteTagNameFunction(builder, tagNames);
        }

        {
            WriteTagNameArray(builder, tagNames);
        }

        builder.AppendLine("}");
        return builder;
    }

    static void WriteTagNameFunction(System.Text.StringBuilder builder, List<string> tagNames)
    {
        tagNames.ToList().ForEach(tagName =>
        {
            builder.Append("\t").AppendLine("/// <summary>");
            builder.Append("\t").AppendFormat("/// return \"{0}\"", tagName).AppendLine();
            builder.Append("\t").AppendLine("/// </summary>");
            builder.Append("\t").AppendFormat(@"public static string @{0} = ""{1}"";", Replace(tagName), tagName).AppendLine();
        });
    }

    static void WriteTagNameArray(System.Text.StringBuilder builder, List<string> tagNames)
    {
        builder.Append("\t").Append("public static readonly string[] TAGS = new string[]{");
        tagNames.ForEach(tagName => builder.AppendFormat(@"""{0}"",", tagName));
        builder.AppendLine("};");
    }

    static string Replace(string name)
    {
        string[] invalidChar = new string[] { " ", "!", "\"", "#", "$", "%", "&", "\'", "(", ")", "-", "=", "^", "~", "¥", "|", "[", "{", "@", "`", "]", "}", ":", "*", ";", "+", "/", "?", ".", ">", ",", "<" };
        invalidChar.ToList().ForEach(s => name = name.Replace(s, string.Empty));
        return name;
    }

    static string currentFolderPath
    {
        get
        {
            return "Assets/KanekoUtilities/Scripts/Utilities/";
        }
    }
}