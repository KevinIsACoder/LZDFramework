using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
public abstract class MyScriptObject : ScriptableObject {

    public static string MyScriptObjectPath = "Assets/ScriptObjectPath/";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static T CreateScriptObject<T>(string assetName,T Obj = null) where T:ScriptableObject
    {
        if (Obj == null) Obj = ScriptableObject.CreateInstance<T>();
        if (MyScriptObjectPath == null) Directory.CreateDirectory(MyScriptObjectPath);
        AssetDatabase.CreateAsset(Obj, GetPath(assetName));
        return Obj;
    }

    public static T GetScriptObject<T>(string assetName,bool createDefault = true) where T:ScriptableObject
    {
        T obj = AssetDatabase.LoadAssetAtPath<T>(GetPath(assetName));
        if (obj == null && createDefault) return CreateScriptObject<T>(assetName);
        return obj;
    }

    static string GetPath(string assetName)
    {
        return MyScriptObjectPath + assetName + ".asset";
    }
}