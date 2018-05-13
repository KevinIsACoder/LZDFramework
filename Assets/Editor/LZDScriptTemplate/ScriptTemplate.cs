/*#AUTORNAME#
#DATATIME#*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class ScriptTemplate : UnityEditor.AssetModificationProcessor {

    private const string csharpExtension = ".cs";
	private const string luaExtension = ".lua";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private static void OnWillCreateAsset(string path){

	    path = path.Replace(".meta",string.Empty);
        if (Path.GetExtension(path) == luaExtension || Path.GetExtension(path) == csharpExtension)
		{
			try
			{
                string content = File.ReadAllText(path);
                content = content.Replace("#AUTHORNAME#","梁振东");
                content = content.Replace("#DATATIME#", System.DateTime.Now.ToString());
                File.WriteAllText(path, content);
                AssetDatabase.Refresh(); //必须有这句才能刷新
			}
			catch (System.Exception e)
			{
                Debug.LogError(e);
			}
		}
	}
}
