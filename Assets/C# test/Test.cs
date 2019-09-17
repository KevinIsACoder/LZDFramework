using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// AUTHOR: 梁振东
// DATETIME: 06/17/2019 18:53:32
// DESC: ****
// dictionary是引用类型，改变dic的值会改变原值
// ref 和 out 参数的区别

public class Test : MonoBehaviour {
	// private Dictionary<string, string> dic = new Dictionary<string>;
	public AnimationCurve animationCurve;
	public MeshRenderer render;
	// Use this for initialization
	void Start () {
		// IDictionary<string, string> Idic = new Dictionary<string, string>();
		// Dictionary<string, string> dic = new Dictionary<string, string>();
		// Idic.Add("lzd","1234");
		// dic.Add("lzd", "lzd");
		// TestDic(Idic);
		// TestDic(dic);
		// long scienceNum = 100000000000000000;
        // Debug.Log(scienceNum.ToString("###,###")); //千分位
        // int money = 100;
		// Debug.Log(string.Format("{0:C}", money));  //货币格式化
        
		// Keyframe[] keyframes = new Keyframe[]{new Keyframe(0, 0), new Keyframe(1, 5), new Keyframe(2, 0), new Keyframe(3, 5), new Keyframe(4, 0)};
		// keyframes[0].outTangent = 5.0f;
		// keyframes[1].outTangent = 0;
		// keyframes[2].outTangent = -5.0f;
		// animationCurve = new AnimationCurve(keyframes);
		// //animationCurve = AnimationCurve.EaseInOut(0, 1, 1, 10);
		

		// animationCurve.preWrapMode = WrapMode.Loop;
		// animationCurve.postWrapMode = WrapMode.Loop;

		// float a = 1, b = 2;
		// try
		// {
        //     if(a == 1)
		// 	   throw(new Exception("trow exception message"));
		// 	b = 3;
		// 	Debug.LogError(b);
		// }
		// catch
		// {

		// }
		// string s1 = "";
		// string s2 = "";
		// TestRef(ref s1);
		// TestOut(out s2);
		//TestOut()
		superClass testclass = new superClass(); //子类可以直接转换成父类
		baseClass bc = (baseClass)testclass;
		bc.ShowName();

		baseClass bs = new baseClass();
		//superClass sc_2 = (superClass)bs; //父类是不能直接转换为子类的
        
		baseClass bs_2 = new superClass();
		superClass sc_3 = bs_2 as superClass; //父类指向的是子类对象，可以转换成子类
		sc_3.ShowName();

		superClass sc = bc as superClass;
		testclass.ShowName();
		gameObject.transform.Find("Capsule").gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(Time.time, animationCurve.Evaluate(Time.time), 0);
		Debug.DrawLine(transform.localPosition,transform.localPosition, Color.red);
		Debug.Log(gameObject.GetComponentInChildren<BoxCollider>());
	}
	void TestDic(IDictionary<string, string> testIdic)
	{
        testIdic["lzd"] = "5678";
	}
	public void TestRef(ref string str)
	{
        str = "test Ref";
	}
    public void TestOut(out string str)
	{
        str = "test out";
	}
	public void TestOut(out object[] p)
	{
        p = new object[3];
	}
}

public class baseClass
{
	public string name = "base";
	public virtual void ShowName()
	{
		Debug.Log(name);
	}
}
public class superClass : baseClass
{
	public new string name = "super";
	public int age = 0;
	// public new void ShowName()
	// {
	// 	Debug.Log(name);
	// }
	public override void ShowName()
	{
		Debug.Log(name);
	}
};

public interface IWriteSetting
{
   void SetSettings();
}
public interface IReadSettting
{
	void GetSettings();
}
interface ISettingsProvider : IReadSettting, IWriteSetting
{

}
