using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    工厂方法模式 ： 分为简单工厂模式和工厂方法模式， 简单工厂方法模式使用了switch语句创建不同工厂，优点是将实例化放在工厂里面，只需要传不同的参数就可生成不同的实例，缺点是违背了开放封闭原则。需要更改工厂类
 */
public class Factory : MonoBehaviour
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
    public abstract Product CreateProduct();
}
public class MyFactory_A : MyFactory_1
{
    public override Product CreateProduct()
    {
        return new ProductA();
    }
}
public class MyFactory_B : MyFactory_1
{
    public override Product CreateProduct()
    {
        return new ProductB();
    }
}
