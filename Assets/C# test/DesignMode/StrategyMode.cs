using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//策略模式：工厂模式和策略模式很像，都是用父类的多态性实现， 工厂模式是创建不同对象，
//策略模式是调用不同行为
public class StrategyMode : MonoBehaviour
{
    void Start()
    {
        StrategyContext strategy = new StrategyContext("A");
        strategy.ContextInteface(); //完全隐藏了具体实现对象，只是通过A告诉用哪种策略
    }
}

public abstract class MyStrategy
{
    public abstract void AlgorrithmInterface();
}
public class ConcreateStrategyA : MyStrategy
{
    public override void AlgorrithmInterface()
    {
        Debug.Log("A method");
    }
}
public class ConcreateStrategyB : MyStrategy
{
    public override void AlgorrithmInterface()
    {
        Debug.Log("B method");
    }
}
public class StrategyContext
{
    MyStrategy strategy;
    public StrategyContext(MyStrategy myStrategy)
    {
        strategy = myStrategy;
    }
    public void ContextInteface()
    {
        strategy.AlgorrithmInterface();
    }
    //结合简单工厂模式,产生策略
    public StrategyContext(string type)
    {
        switch(type)
        {
            case "A":
                strategy = new ConcreateStrategyA();
            break;
            case "B":
                strategy = new ConcreateStrategyB();
            break;
        }
    }
}
