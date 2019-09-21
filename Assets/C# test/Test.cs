using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// AUTHOR: 梁振东
// DATETIME: 06/17/2019 18:53:32
// DESC: ****
public class Test : MonoBehaviour {
	// private Dictionary<string, string> dic = new Dictionary<string>;

	// Use this for initialization
	void Start () {
		IDictionary<string, string> Idic = new Dictionary<string, string>();
		Dictionary<string, string> dic = new Dictionary<string, string>();
		Idic.Add("lzd","1234");
		dic.Add("lzd", "lzd");
		TestDic(Idic);
		TestDic(dic);
		Debug.Log(Idic);
		Debug.Log(dic);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void TestDic(IDictionary<string, string> testIdic)
	{  
		testIdic = new Dictionary<string, string>();
        //testIdic["lzd"] = "5678"; //会改变实参
		testIdic["LZD"] = "1314"; //	不会会改变实参
	}
}
