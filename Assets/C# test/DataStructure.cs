//AuthorName : #author#;
//CreateDate : #dateTime#;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
//数据结构练习
public class DataStructure : MonoBehaviour 
{
    //递归函数        1;           N <=1;
	         // fn =
			//        Nfn(n - 1); n > 1;
	// Use this for initialization
	//程序的空间复杂度： 指该程序运行所需要的内存大小
	private int test = 0;
	void Start () {
		//斐波那契数列】
	   // fn = f(n-1) + f(n - 2)
       Debug.Log(Factory(3));

	   Dictionary<string, TestDic> dic = new Dictionary<string, TestDic>();
	   TestDic dataStructure = new TestDic();
	   dic.Add("test", dataStructure);
	   foreach(var kv in dic)
	   {
		   kv.Value.test = 2;
	   }
	   Debug.Log(dic);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	int Factory(int n)
	{
		int N1 = 0, N2 = 1;
		if(n == 0) return N1;
		if(n == 1) return N2;
		if(n == 2)
		{
			return N1 + N2;
		}
		else if(n > 2)
		{
           return Factory(n - 1) + Factory(n - 2);
		}
		return -1;
	}
}
public class TestDic
{
	public int test = 0;
	public LinkedList<string> linkList = new LinkedList<string>();
    
}
