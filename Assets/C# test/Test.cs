using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// AUTHOR: 梁振东
// DATETIME: 06/17/2019 18:53:32
// DESC: ****
// dictionary是引用类型，改变dic的值会改变原值
public class Test : MonoBehaviour {
	// private Dictionary<string, string> dic = new Dictionary<string>;
	public AnimationCurve animationCurve;
	public MeshRenderer render;
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
		Debug.Log(string.Format("{0:C}", money));  //货币格式化
        
		Keyframe[] keyframes = new Keyframe[]{new Keyframe(0, 0), new Keyframe(1, 5), new Keyframe(2, 0), new Keyframe(3, 5), new Keyframe(4, 0)};
		keyframes[0].outTangent = 5.0f;
		keyframes[1].outTangent = 0;
		keyframes[2].outTangent = -5.0f;
		animationCurve = new AnimationCurve(keyframes);
		//animationCurve = AnimationCurve.EaseInOut(0, 1, 1, 10);
		

		animationCurve.preWrapMode = WrapMode.Loop;
		animationCurve.postWrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(Time.time, animationCurve.Evaluate(Time.time), 0);
		Debug.DrawLine(transform.localPosition,transform.localPosition, Color.red);
	}
	void TestDic(IDictionary<string, string> testIdic)
	{
        testIdic["lzd"] = "5678";
	}
}
