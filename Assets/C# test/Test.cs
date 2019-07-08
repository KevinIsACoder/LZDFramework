using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// AUTHOR: 梁振东
// DATETIME: 06/17/2019 18:53:32
// DESC: ****
// dictionary是引用类型，改变dic的值会改变原值
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
		long scienceNum = 100000000000000000;
        Debug.Log(scienceNum.ToString("###,###")); //千分位
        int money = 100;
		Debug.Log(string.Format("{0:C}", money));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void TestDic(IDictionary<string, string> testIdic)
	{
        testIdic["lzd"] = "5678";
	}
}
