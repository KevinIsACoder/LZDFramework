using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : #AUTHORNAME#
//DATE : #DATE#
//DESC : ****
//适配器模式，
public class Adapter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class Target //客户期待的接口
{
    public virtual void Request()
    {
        Debug.Log("普通请求！");
    }
}
//Adaptee:需要适配的类
public class Adaptee
{
    public void SpecificRequest()
    {
        Debug.Log("特殊请求!");
    }
}
public class MyAdapter : Target
{
    private Adaptee adaptee = new Adaptee();
    public override void Request()
    {
        adaptee.SpecificRequest();
    }
}
