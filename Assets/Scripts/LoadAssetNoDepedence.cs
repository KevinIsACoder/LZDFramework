using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : 
public class LoadAssetNoDepedence : MonoBehaviour {

    private Dictionary<string, int> assetBundleRef = new Dictionary<string, int>();
	// Use this for initialization
	void Start () {
		//要加载一个含有依赖的assetbundle,  比如 ui_login 依赖 circle, 需要先加载circle这个依赖，ui_login 才能正常显示
		string manifestUrl = Application.streamingAssetsPath + "/" + "StreamingAssets";
		AssetBundle assetBundle = AssetBundle.LoadFromFile(manifestUrl);
		AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
		string[] dependences = manifest.GetAllDependencies("ui_login.assetbundle");
		for(int i = 0; i < dependences.Length; ++i)
		{
			string refBundleName = dependences[i];
		    AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + refBundleName);
			//asset.Unload(false);  //这边如果释放了，图片显示不出
			
			if(assetBundleRef.ContainsKey(refBundleName))
			{
				assetBundleRef[refBundleName] += 1;
			}
		}
        //加载正式资源
		string bundlePath = Application.streamingAssetsPath + "/" + "ui_login.assetbundle";
		AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
		GameObject bundleObj = bundle.LoadAsset<GameObject>("Panel_test");
		//Resources.UnloadAsset(bundleObj);   //Resource.UnLoadAsse（）不能卸载prefab和
	    GameObject obj = GameObject.Instantiate(bundleObj);
        
		obj.transform.SetParent(transform, false);
		obj.transform.localScale = Vector3.one;
		obj.transform.localRotation = Quaternion.identity;
		bundle.Unload(false);
		Resources.UnloadUnusedAssets();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
