using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 09/07/2019 15:23:18
//DESC : ****
//代理模式 ： 为其他对象提供一种代理以控制对这个对象的访问

public class Proxy : MonoBehaviour
{
    // Start is called before the first frame update
    private static readonly int Test = 0;
    private static string lzd; 
    public int CreateTest
    {
        get
        {
            return Test;
        }
    }
    private string GetName
    {
        get
        {
            return lzd;
        }
        set
        {
            lzd = "";
        }
    }
    void Start()
    {
        MyProxy myProxy = new MyProxy();
        myProxy.Request();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public abstract class Subject
{
    public abstract void Request();
}
public class RealSubject : Subject
{
    public override void Request()
    {
        System.Console.WriteLine("真实的请求");
    }
}
//proxy类， 保存一个引用使得代理可可以访问实体，并提供一个于subject相同接口的接口，这样代理就可以用来替代实体
public class  MyProxy : Subject
{
    RealSubject realSubject;
    public override void Request()
    {
        if(realSubject == null)
        {
            realSubject = new RealSubject();
        }
        realSubject.Request();
    }
}