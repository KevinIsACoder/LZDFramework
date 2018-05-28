
using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
// 1、Application.persistentDataPath:沙盒路径，热更新路径 2、Application.dataPath:数据存放路径 3、Application.StreammingAssets:资源存放目录
public class utility
{

    public string datapath { get { return Application.dataPath; } }
    public string streammingAssetPath { get { return Application.streamingAssetsPath; } }
    public string persistDataPath { get {

            if (Application.isEditor) return Application.dataPath + "/" + Application.productName + "/";
            return Application.persistentDataPath;
    } }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //MD5文件
    public static string Md5File(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(fs);
            fs.Close();
            StringBuilder sb = new StringBuilder();
            foreach (byte bt in data)
            {
                sb.Append(bt.ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            Debug.Log("MD5 File Error:" + ex.ToString());
            return ex.ToString();
        }
    }
    //MD字符串
    public static string Md5String(string str)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
        string md5Str = "";
        for (int i = 0; i < data.Length; ++i)
        {
            md5Str += System.Convert.ToString(data[i], 16).PadLeft(2,'0');
        }
        md5Str = md5Str.ToString().PadLeft(32, '0');
        return md5Str;
    }
    //应用程序所在目录，热更新所在目录,资源解压的目录
    public static string DataPath
    {
        get
        {
            string game = "lzdGame";
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/" + game + "/";
            }
            if (Appconst.DebugMode)
            {
                return Application.dataPath + "/" + Appconst.streammingAssets + "/";
            }
            return "C:/" + Appconst.gameName + "/";
        }
    }
    public static string StreamAssetsDir
    {
        get
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                return "jar:file://" + Application.dataPath + "!/assets/";
            }
            else if(Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return Application.dataPath + "/Raw/";
            }
            else if (Application.isEditor)
            {
                return Application.dataPath + "/" + Appconst.streammingAssets + "/";
            }
            return null;
        }
    }
}