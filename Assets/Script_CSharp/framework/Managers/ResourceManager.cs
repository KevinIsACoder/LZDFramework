using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UObject = UnityEngine.Object;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
public class ResourceManager : Mysingleton<ResourceManager> {

    private Dictionary<string, AssetBundle> m_Bundles; //记录已经加载过的bundle
    private Dictionary<string, string> m_AssetPath; //记录资源在编辑器下的路径
    private string bundlePath;
    private const string m_bundleExtension = ".unity";
    private AssetBundleManifest m_manifest;
    public AssetBundleManifest Manifest  //Bundle的Manifest
    {
        get
        {
            if (m_manifest == null)
            {
                AssetBundle manifestBundle = AssetBundle.LoadFromFile(bundlePath + "StreamingAssets");
                if (manifestBundle != null)
                    Manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
            return m_manifest;
        }
        private set{ }
    }
    void Awake()
    {

    }
	// Use this for initialization
	void Start () {
        m_Bundles = new Dictionary<string, AssetBundle>();
        m_AssetPath = new Dictionary<string, string>();
        bundlePath = Application.streamingAssetsPath;
	}

    //获取资源文件路径
    private void GetAssetPathFromBundle(AssetBundle bundle)
    {
        foreach(string assetdir in bundle.GetAllAssetNames())
        {
            string assetName = Path.GetFileNameWithoutExtension(assetdir);
            m_AssetPath[GetAssetkey(bundle.name, assetName)] = assetdir;
        }
    }
    //获取key值
    private string GetAssetkey(string bundlename,string assetName)
    {
        bundlename = bundlename.ToLower();
        assetName = assetName.ToLower();
        return bundlename + "-" + assetName;
    }
    //同步加载资源
    public UObject LoadAsset(string bundleName,string assetName)
    {
#if UNITY_EDITOR
        string assetkey = GetAssetkey(bundleName,assetName);
        string assetpath;
        if (!m_AssetPath.TryGetValue(assetkey,out assetpath))
        {
            Debug.LogWarning("assetkey not find" + assetkey);
            return null;
        }
        else
        {
            return  UnityEditor.AssetDatabase.LoadAssetAtPath(assetpath,typeof(UObject));
        }
#else
        bundleName = bundleName.ToLower();
        AssetBundle bundle = LoadBundle(bundleName);
        if (bundle != null) return bundle.LoadAsset(assetName);
        return null;
#endif
    }
    //泛型加载资源
	public T LoadAsset<T>(string bundleName,string assetName) where T:UObject
    {
        bundleName = bundleName.ToLower();
        AssetBundle bundle = LoadBundle(bundleName);
        if (bundle != null) return bundle.LoadAsset<T>(assetName);
        return null;
    }
    //加载Bundle
    public AssetBundle LoadBundle(string bundleName)
    {
        AssetBundle ab;
        LoadDepedences(bundleName); //处理依赖关系，有依赖，先加载依赖，在加载bundle
        if (m_Bundles.ContainsKey(bundleName)) m_Bundles.TryGetValue(bundleName,out ab);
        else
        {
            string bundleDir = bundlePath + bundleName;
            Debug.Log("Load Bundle From File: " + bundleDir);
            ab = AssetBundle.LoadFromFile(bundleDir);
            GetAssetPathFromBundle(ab);
            m_Bundles[bundleName] = ab;
        }
        return ab; 
    }

    //加载依赖
    private void LoadDepedences(string bundleName)
    {
        if (Manifest == null) { Debug.LogError("Manifest Is Null!"); return; }
        string[] manifests = Manifest.GetAllDependencies(bundleName);
        foreach(string depedence in manifests)
        {
            LoadBundle(depedence);
        }
    }

    //异步加载资源
    public void LoadAssetAsync<T>(string bundleName,string assetName,System.Action<T> callback) where T:UObject
    {
        StartCoroutine(cor_LoadAsset<T>(bundleName, assetName, callback));
    }
    IEnumerator cor_LoadAsset<T>(string bundleName,string assetName,System.Action<T> callback) where T:UObject
    {
        AssetBundle ab;
        yield return null;
        bundleName = bundleName.ToLower();
        if (!bundleName.EndsWith(m_bundleExtension)) bundleName += m_bundleExtension;
        yield return cor_LoadBundle(bundleName);
        if (m_Bundles.TryGetValue(bundleName,out ab))
        {
            AssetBundleRequest abr = ab.LoadAssetAsync<T>(assetName);
            if (abr.isDone) callback(abr.asset as T);
        }
    }
    IEnumerator cor_LoadBundle(string bundleName)
    {
        string abPath = bundlePath + bundleName;
        yield return null;
        yield return cor_LoadDepedences(bundleName); //加载依赖
        if (!m_Bundles.ContainsKey(bundleName))
        {
            AssetBundleCreateRequest abr = AssetBundle.LoadFromFileAsync(abPath);
            if (abr.isDone)
            {
                if (abr.assetBundle == null)
                {
                    Debug.LogError("Bundle is not Exist!" + bundleName);
                }
                m_Bundles.Add(bundleName, abr.assetBundle);
            }
        }
    }
    IEnumerator cor_LoadDepedences(string bundleName)
    {
        yield return null;
        string[] bundledepedences = Manifest.GetAllDependencies(bundleName);
        foreach (string dep in bundledepedences)
        {
            yield return cor_LoadBundle(dep);
        }
    }

    public void UnLoadAssetBundle(){
       // AssetBundle
    }

    public void UnLoadUnusedAssets(){
        Resources.UnloadUnusedAssets();
    }
}