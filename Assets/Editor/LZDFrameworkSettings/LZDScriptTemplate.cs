using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
public class LZDScriptTemplate : UnityEditor.AssetModificationProcessor
{

    private const string CSharpExtension = ".cs";
    private const string LuaExtension = ".lua";
    private static string filepath = "";

    static void OnWillCreateAsset(string assetName)
    {
        filepath = assetName.Replace(".meta", "");
        if (filepath.EndsWith(".cs") || filepath.EndsWith(".lua"))
        {
            //FileStream fs = new FileStream(filepath, FileMode.Open);
            try
            {
                string contents = File.ReadAllText(filepath);
                contents = contents.Replace("#author#", "梁振东").Replace("#dateTime#", System.DateTime.Now.ToString())
                .Replace("#DESC#", "****");
                File.WriteAllText(filepath, contents);
                AssetDatabase.Refresh();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}
