using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
public class BundleObject : MyScriptObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public string assetName = "Bundle";
    public BuildTarget target = BuildTarget.Android;
    public string outPath = "Assets/StreamingAssets";
    public string filetxtName = "file.txt";
    public bool forceRebuild = false;
    [Serializable]
    public class bundleInfo
    {
        public string filePattern;
        public string bundleName;
        public string searchPath;
        public SearchOption searchOption = SearchOption.AllDirectories;
    }
    [Serializable]
    public class copyInfo
    {
        public string sourthPath;
        public string filePattern;
        public string destPath;
        public SearchOption searchOption = SearchOption.AllDirectories;
    }
    public List<bundleInfo> bundleinfo = new List<bundleInfo>();
    public List<copyInfo> copyinfo = new List<copyInfo>();

    public bool bundleFoldOut = false;
    public bool copyFoldOut = false;    
}