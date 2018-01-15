using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
namespace LZDEditorTools{
    public class BuildBundle:LZDScriptObject{
        public const string assetName = "BundleObject";
        public string bundleExtension = ".unity";
        public string fileListName = "file.txt";
        public BuildTarget target = BuildTarget.Android;
        public bool RemoveOldPath = false;
        [Serializable]
        public class bundleInfo{
        public string bundleName = "";
        public string assetpath = "";
        public string filepattern = "*.*";
        public SearchOption searchoption = SearchOption.AllDirectories;
        }
        [Serializable]
        public class copyInfo{
            public string sourthPath = "";
            public string destPath = "";
            public SearchOption searchoption = SearchOption.AllDirectories;
            public string filepattern = "*.*";
        }
        public List<bundleInfo> bundleList = new List<bundleInfo>();
        public List<copyInfo> copyList = new List<copyInfo>();
    }
}