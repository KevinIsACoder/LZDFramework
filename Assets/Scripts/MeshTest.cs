using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : #DESC#
//Note that while triangle meshes are the most common use case, 
//Unity also supports other mesh topology types, for example Line or Point meshes. For line meshes, each line is composed of two vertex indices and so on
public class MeshTest : MonoBehaviour {
	public delegate void TestHandler(string str, int a);
	public event TestHandler TestEvent;
	public event EventHandler<TestEventArgs> TestDelegateHandler; //EventHandler 其实就是一个委托
    public Transform enemyTransform;
	// Use this for initialization
	void Start () 
	{
		Debug.LogError(int.Parse("1111111111"));
		TestHandler testHandler = new TestHandler(TestDelegate);
		testHandler.Invoke("lzd", 10);

		//事件注册委托
		TestEvent += testHandler;
		TestEvent.Invoke("lzd", 10);

		TestDelegateHandler = TestEventHandler;
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
        
		mesh.vertices = new Vector3[]{new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
		mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[]{0, 1, 2};

		Vector3 vec = transform.position - enemyTransform.position;
		transform.Translate(vec);
	}
	void TestDelegate(string str, int a)
	{

	}
	void TestEventHandler(object sender, TestEventArgs args)
	{

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 vec = enemyTransform.position - transform.position; //代表相对位置距离
		transform.Translate(vec, Space.World);
	}
}
public class TestEventArgs : EventArgs
{

}

public struct interated
{

}
// public struct A : interated 
// {

// }
