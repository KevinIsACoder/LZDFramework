//AuthorName : 梁振东;
//CreateDate : 10/2/2019 11:12:28 AM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using ReflectionAssembly;
//反射提供封装程序集、模块和类型的对象。 
//可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。
//然后，可以调用类型的方法或访问其字段和属性
public class Reflection : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        object obj = new ReflectionTest();
        Type type = obj.GetType();
        PropertyInfo[] property = type.GetProperties();
        FieldInfo[] fieldInfo = type.GetFields();
        foreach (var name in property)
        {
            Debug.Log(name.Name);
        };
        foreach(var field in fieldInfo)
        {
            Debug.Log("Filed is..." + field);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

namespace ReflectionAssembly
{
    public class ReflectionTest
    {
        private int _field; //私有字段也是找不到的  
        public int Num
        {
            set;
            get;
        }
        private string Name //私有属性用反射是找不到的
        {
            set;
            get;
        }
        //private的
        [Obsolete("the methos has error")]
        private void TestFlection()
        {

        }
        //public的
        public void TestFlectionPublic()
        {

        }
        [CommanAttribute("lzd")]
		public void ComstomeAttributeTest()
		{
            Debug.Log("CustomeAttribute!");
		}
    }
    //自定义特性
    public class CommanAttribute : Attribute
    {
        public CommanAttribute(params object[] str)
        {

        }
        public CommanAttribute(string alias)
        {
            Alias = alias;
        }
        public string Alias { get; private set; }
    }
}
