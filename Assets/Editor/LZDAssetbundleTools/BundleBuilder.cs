using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using UnityEngineInternal;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
public class BundleBuilder{

    private static string SPLIGHTLINE = "|";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void BuildBundle(BundleObject bundleobj)
    {
        if (bundleobj.forceRebuild && Directory.Exists(bundleobj.outPath)) Directory.Delete(bundleobj.outPath, true); //判断要不要重新生成StreamingAssets
        Directory.CreateDirectory(bundleobj.outPath);
        AssetDatabase.Refresh();
        CopyFiles(bundleobj);
        AssetDatabase.Refresh();
        BuildPipeline.BuildAssetBundles(bundleobj.outPath, GetBundleList(bundleobj).ToArray(), BuildAssetBundleOptions.DeterministicAssetBundle, bundleobj.target);
        if (bundleobj.filetxtName != null) CreateFileList(bundleobj.outPath,bundleobj.filetxtName);
        AssetDatabase.Refresh();
        Debug.Log("Build Complete");
    }
    private static void CopyFiles(BundleObject bundleobj)
    {
        foreach(BundleObject.copyInfo info in bundleobj.copyinfo)
        {
            string sourthPath = info.sourthPath;
            string destPath = info.destPath;
            if (sourthPath == string.Empty || destPath == string.Empty) continue;
            if (sourthPath == null) return;
            string[] files = Directory.GetFiles(sourthPath,info.filePattern,info.searchOption);
            foreach(string filePath in files)
            {
                if (filePath.EndsWith(".meta")) continue;
                Directory.CreateDirectory(destPath);
                string newpath = destPath + filePath.Replace(sourthPath, "");
                Directory.CreateDirectory(Path.GetDirectoryName(newpath));
                File.Copy(sourthPath, newpath, true);
            }
        }
    }
    private static List<AssetBundleBuild> GetBundleList(BundleObject bundleobj)
    {
        List<AssetBundleBuild> buildList = new List<AssetBundleBuild>();
        foreach(BundleObject.bundleInfo info in bundleobj.bundleinfo)
        {
            if (string.IsNullOrEmpty(info.bundleName)) { continue; Debug.LogWarning("bundleName is Empty"); }
            if (info.filePattern == "") info.filePattern = "*";
            string[] files = Directory.GetFiles("Assets/" + info.searchPath,info.filePattern,info.searchOption);
            for(int i = 0;i < files.Length;++i)
            {
                files[i] = files[i].Replace("\\", "/");
            }
            AssetBundleBuild abb = new AssetBundleBuild();
            abb.assetBundleName = info.bundleName;
            abb.assetNames = files;
            buildList.Add(abb);
        }
        Debug.LogError(buildList.Count);
        return buildList;
    }
    private static void CreateFileList(string fileListpath,string filename)
    {
        string filetxtPath = Path.Combine(fileListpath, filename);
        if (File.Exists(filetxtPath)) File.Delete(filetxtPath); //如果file.txt存在了已经，就删除存在的，重新生成
        List<string> fileList = new List<string>();
        TraverseFile(fileListpath, fileList);
        FileStream fs = new FileStream(filetxtPath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        foreach (string file in fileList)
        {
            if (file.EndsWith(".meta") || file.EndsWith(".manifest")) continue;
            string md5Value = utility.Md5File(file);
            string relativePath = file.Replace(fileListpath, string.Empty);
            string size = (new FileInfo(file).Length >> 10).ToString();
            sw.WriteLine(relativePath + SPLIGHTLINE + md5Value + SPLIGHTLINE + size);
        }
        sw.Close();
        fs.Close();
    }

    private static void TraverseFile(string filePath,List<string> fileList) //遍历文件
    {
        if (!Directory.Exists(filePath)) return;
        string[] files = Directory.GetFiles(filePath);
        string[] paths = Directory.GetDirectories(filePath);
        foreach(string file in files)
        {
            fileList.Add(file.Replace("\\","/"));
        }
        foreach(string path in paths)
        {
            TraverseFile(path, fileList);
        }
    }
}