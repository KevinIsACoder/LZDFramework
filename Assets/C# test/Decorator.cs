using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//AUTHOR : 梁振东
//DATE : 09/03/2019 19:54:16
//DESC : ****
//装饰模式
//动态的给一个对象添加一些额外的职责，就增加功能来说，装饰模式比生成子类更为灵活

public class Decorator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //装饰模式
        ConcreateMyComponent behavior = new ConcreateMyComponent();
       // MyDecorator myDecorator = new MyDecorator(); //错误的，抽象类不能实例化
        //MyDecorator myDecorator = new ConcreateDecoratorA(); //正确的 这叫多态的向上转型，基类引用指向派生类的实际对象new后面跟哪个类就是哪个类的对象。
        ConcreateMyComponent c = new ConcreateMyComponent();
        ConcreateDecoratorA d1 = new ConcreateDecoratorA();
        ConcreateDecoratorB d2 = new ConcreateDecoratorB();
        d1.SetMyComponent(c);    /*装饰的方式是 ： 首先用concreateMyComponent实例化对象c, 然后用d1包装c, 再用d2包装d1 
                                装饰模式是利用setcomponent  来对对象进行包装的， 这样每个装饰对象的实现就和如何利用这个对象分离开了， 每个装饰对象只关心自己的功能，不需要关心如何被添加到对象链中*/
        d2.SetMyComponent(d1);
        d2.Operation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public abstract class MyComponent  //用于给其他类添加功能
{
    public virtual void Test(){}
    public abstract void Operation();
}
public class  ConcreateMyComponent : MyComponent
{
    public override void Operation()
    {
        Console.WriteLine("具体对象的操作");
    }
}
public abstract class MyDecorator : MyComponent //装饰抽象类
{
    protected MyComponent MyComponent;
    public void SetMyComponent(MyComponent MyComponent)
    {
        this.MyComponent = MyComponent;
    }
    public override void Operation()
    {
        if(MyComponent != null)
        {
            MyComponent.Operation();
        }
    }
}
public class ConcreateDecoratorA : MyDecorator
{
    private string addedState;  //本类的独有功能，区别于ConcreateDecoratorB
    public override void Operation()   //先执行原MyComponent的OPeration，在执行本类的功能，如addstate, 相当于对原coponent进行了装饰
    {
        base.Operation();
        addedState = "new State";
        Console.WriteLine("具体装饰对像A的操作");
    }
}
public class ConcreateDecoratorB : MyDecorator
{
    public override void Operation()
    {
        base.Operation();
        AddBehavior();
        Console.WriteLine("具体装饰对象B的操作");
    }
    void AddBehavior()
    {

    }
}
