using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 08/14/2019 13:25:21
//DESC : ****
public class TestSort : MonoBehaviour
{
    
    void Start()
    {
        List<string> list = new List<string>();
        myclass testclass = new myclass();
        foreach(var keyvalue in testclass.testDic)
        {
            list.Add(keyvalue.Key);
        }
        list.Sort((k1, k2) => {
            if(testclass.testDic[k1] > testclass.testDic[k2])
            {
                return -1;
            }
            else if(testclass.testDic[k1] < testclass.testDic[k2])
            {
                return 1;
            }
            return 0;
        });
        for(int i = 0; i < list.Count; ++i)
        {
            Debug.Log(list[i]);
        }
    }
}

public class myclass
{
    public int a;
    public int b;
    public Dictionary<string, int> testDic = new Dictionary<string, int>();
    public myclass()
    {

       for(int i = 0; i < 10; ++i)
       {
           testDic.Add(i.ToString(), i);
       }
    }
}
