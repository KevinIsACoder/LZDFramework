using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : #AUTHORNAME#
//DATE : #DATE#
//DESC : ****
//建造者模式： 讲一个复杂对象的构建与他的表示分离，使得同样的构建过程可以创建不同的表示
// 
public class builder : MonoBehaviour
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

// Builder 抽象接口， ConcreateBuilderA, ConcreateBuilderB具体实现 ， Director是构建Builder的接口的对象
public abstract class Builder
{
    public abstract void BuilderPartA();
    public abstract void BuilderPartB();
    public abstract void GetResult();
}
public class ConcreateBuilderA : Builder
{
    public override void BuilderPartA()
    {

    }
    public override void BuilderPartB()
    {

    }
    public override void GetResult()
    {

    }
}

public class ConcreateBuilderB : Builder
{
    public override void BuilderPartA()
    {

    }
    public override void BuilderPartB()
    {

    }
    public override void GetResult()
    {
        
    }
}
public class Director
{
    public void Construct(Builder builder) //这部分通过传入的不同类型的参数构建出不同类型对象
    {
        builder.BuilderPartA();
        builder.BuilderPartB();
    }
}