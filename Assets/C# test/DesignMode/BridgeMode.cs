using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 09/19/2019 14:28:10
//DESC : 桥接模式 ： 使抽象部分和实现部分分离，使他们能独立的变化
// 由于继承的关系，抽象类和派生类紧密联系在一起，难以对抽象部分和实现部分进行独立的修改和扩充
// 意图是将抽象化和实现话脱耦，所谓托藕指的是两者之间使用聚合关系代替继承关系， 使二者能独立的变化，
public class BridgeMode : MonoBehaviour
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
//行为接口,
public abstract class Implementor
{
    public abstract void Operation();
}
//抽象接口
public class Abstraction
{
    protected Implementor implementor;
    public Implementor Implementor
    {
        set
        {
           implementor = value; 
        }
    }
    public virtual void Operation()
    {
        implementor.Operation();
    }
}
public class ConcreateImplementA : Implementor
{
    public override void Operation()
    {
        Debug.Log("Implementor A Operation");
    }
}
public class ConcreateImplementB : Implementor
{
    public override void Operation()
    {
        Debug.Log("Implemnetor B Operation");
    }
}
public class RefinedAbstraction : Abstraction
{
    public override void Operation()
    {
        implementor.Operation();
    }
}