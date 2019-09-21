using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
/*
    工厂方法模式 ： 分为简单工厂模式和工厂方法模式， 简单工厂方法模式使用了switch语句创建不同工厂，优点是将实例化放在工厂里面，只需要传不同的参数就可生成不同的实例，缺点是违背了开放封闭原则。需要更改工厂类
 */
public class Factory : MonoBehaviour
{
    public delegate void OnCreateFactor(MyFactory_1 factory);
    public  OnCreateFactor CreateFactorHandler;

    // Start is called before the first frame update
    void Start()
    {
       IStudent student = new Student();
       student.Show("lzd"); //显示调用接口
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//简单工厂模式
public abstract class Product
{
    public abstract void Show();
}
public class ProductA : Product
{
    public override void Show(){
        Debug.Log("Product A");
    }
}
public class ProductB : Product
{
    public override void Show()
    {
        Debug.Log("product B");
    }
}

public class MyFactory
{
    public static Product Show(string name)
    {
        switch(name)
        {
            case "productA":
                return new ProductA();
            case "productB":
                return new ProductB();
            default:
                return null;

        }
    }
}
//工厂方法模式
public abstract class MyFactory_1
{
    private string name
    {
        get;
        set;
    }
    public MyFactory_1(string name)
    {
        
    }
    public abstract Product CreateProduct();
}
public class MyFactory_A : MyFactory_1
{
    private string name;
    public MyFactory_A(string name) : base(name)
    {
        this.name = name;
    }
    public override Product CreateProduct()
    {
        return new ProductA();
    }
}
public class MyFactory_B : MyFactory_1
{
    private string name;
    public MyFactory_B(string name) : base(name)
    {

    }
    public override Product CreateProduct()
    {
        return new ProductB();
    }
}

//接口的隐士实现和显示实现
/*隐示实现对象声明为接口和类都可以访问到其行为，显示实现只有声明为接口可以访问。

如果两个接口中有相同的方法名，那么同时实现这两个接口的类，就会出现不确定的情形，在编写方法时，也不知道实现哪个接口的方法了。为解决这一问题，C#提供了显示接口实现技术，就是在方法名前加接口名称，用接口名称来限定成员。用“接口名.方法名()”来区分实现的是哪一个接口。

注意：显示接口实现时，在方法名前不能加任何访问修饰符。这种方式和普通方法不同，普通方法前不加访问修饰符，默认为私有的，而显式接口实现时方法前不加任何修饰符，默认为公有的，如果前面加上修饰符，会出现编译错误。

调用显示接口实现的正确方式是通过接口引用，通过接口引用来确定要调用的版本。*/
public interface IPerson
{
    string name
    {
        get;
        set;
    }
    void Show(string name);
}
public interface IStudent
{
    string name
    {
        get;
        set;
    }
    void Show(string name);
}
public class Student : IPerson, IStudent
{
    private string personName;
    public  string name{
        get
        {
            return personName;
        }
        set
        {
            personName = value;
        }
    }
    public string studentName;
    public string StudentName{
        get
        {
            return studentName;
        }
        set
        {
            studentName = value;
        }
    }
    void IPerson.Show(string name)  //显示实现前面不能有任何修饰符：private overrride
    {

    }
    void IStudent.Show(string name)
    {

    }

}
//抽象工厂方式: 结合反射
//提供一个创建一系列相关或者相互依赖对象的接口，而无需指定具体的类

