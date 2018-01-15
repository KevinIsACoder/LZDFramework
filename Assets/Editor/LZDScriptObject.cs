using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
namespace LZDEditorTools{
public class LZDScriptObject : ScriptableObject{

    public const string assetdirPath = "Assets/LZDScriptableObject";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static T Load<T>(string assetname) where T:ScriptableObject{
		string resPath = assetdirPath + "/" + assetname;
		if(!Directory.Exists(assetname)) Directory.CreateDirectory(assetdirPath);
		T obj = Resources.Load<T>(assetdirPath);
		if(obj == null){
			obj = Create<T>(assetname);
		}
		return obj;
	}
    public static T Create<T>(string assetname,T obj = null) where T:ScriptableObject {
	   if(!Directory.Exists(assetdirPath)) Directory.CreateDirectory(assetdirPath);
	   if(obj == null) obj = ScriptableObject.CreateInstance<T>();
	   string resPath = assetdirPath + "/" + assetname + ".asset";
	   AssetDatabase.CreateAsset(obj,resPath);
	   return obj;
	}
}
}