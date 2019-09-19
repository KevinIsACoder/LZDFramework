using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//观察者模式：定义了一种一对多的依赖关系，让多个观察者对象监听一个主题对象，当主题对象发生改变时，通知所有其他的监听者，
//使得他们能够跟新自己
public class Observer : MonoBehaviour
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
public abstract class MySubject
{
    private IList<MyObserver> observers = new List<MyObserver>();
    public  void AddObserver(MyObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(MyObserver observer)
    {
        observers.Remove(observer);
    }
    public void Notify()
    {
        foreach(var observer in observers)
        {
            observer.Update();
        }
    }
}
//具体的主题对象
public class ConcreateSubject : MySubject
{
    private string subjectState;
    public string SubJectState
    {
        get
        {
            return subjectState;
        }
    }
}
public abstract class MyObserver
{
    public abstract void Update();
}
public class ConcreteObserver : MyObserver
{
    private string name;
    private string observerState;
    private ConcreateSubject subject;

    public ConcreteObserver(ConcreateSubject subject, string name)
    {
        this.name = name;
        this.subject = subject;
    }
    public override void Update()
    {
        observerState = subject.SubJectState;
        System.Console.WriteLine("观察者的{0}的状态是{1}", name, subject);
    }
}
//上面的观察者模式有缺点，因为所有的观察者都需要继承同一个抽象类，现实中是需要在不同的观察者类当中实现监听主题对象
//解决方法是使用委托
public interface MySubject_2
{
    void Notify();
    string subjectState //接口可以包含事件、索引器、方法和属性，不能包含字段
    {
        get;
        set;
    }
} 
public class MySubject_Child : MySubject_2
{
    public delegate void NotifyHandler();

    public NotifyHandler update;
    public void Notify()
    {
        update();
    }
    public string subjectState
    {
        get;
        set;
    }
    
}
public class MyObserver_Child
{
    private string name;
    private MySubject_Child subject_Child;
    public MyObserver_Child(string name, MySubject_Child subject_Child)
    {
        this.name = name;
        this.subject_Child = subject_Child;
    }
    public void ReceiveNotice()
    {
    }

}

public class TestObserver
{
    void main()
    {
       MySubject_Child subject_2 = new MySubject_Child();
       MyObserver_Child observer_Child = new MyObserver_Child("李雷", subject_2);
       subject_2.update += observer_Child.ReceiveNotice;
       subject_2.subjectState = "boss";
       subject_2.Notify();
    }
}
