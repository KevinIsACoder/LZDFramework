//AuthorName : #author#;
//CreateDate : #dateTime#;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
//各种排序算法
public class Sort : MonoBehaviour 
{
    private const string test = "lzd"; //const 修饰的变量必须初始化, const前面不要有static，常量不能改变值
	private static readonly string test_1; //静态只读变量或者只读变量只可以初始化或在构造函数中赋值
	// Use this for initialization
	void Start () {
		//test = ""; 不对
		//test_1 = "lzd"; 不对
		int[] args = new int[]{5, 4, 7, 2, 3};
		// QuickSort quickSort = new QuickSort();
		// quickSort.Sort(args, 0, args.Length - 1);
		// BubbleSort bubbleSort = new BubbleSort();
		// bubbleSort.Sort(args);
		// SelectSort selectSort = new SelectSort();
		// selectSort.Sort(args);
		// InsertSort insertSort = new InsertSort();
		// insertSort.Sort(args);
		// for(int i = 0; i < args.Length; ++i)
		// {
		// 	Debug.Log(args[i]);
		// }
		// StaticInherit.StaticTest();
		// int b = StaticTest.b;
		// int c = StaticTest.c;
		// StaticInherit.Property = 10;

		// Debug.Log(StaticTest.a);
		// Debug.Log(StaticTest.Property);
		
		// Debug.Log(StaticInherit.a);
		// Debug.Log(StaticInherit.Property);
		List<StaticTest> list = new List<StaticTest>();
		StaticTest test = new StaticTest();
		StaticTest test_1 = new StaticTest();
		test_1.listTest = 8;
	    StaticTest test_2 = new StaticTest();
		test_2.listTest = 10;
		list.Add(test);
		test = test_1;
		list.Add(test);
		test = test_2;
		list.Add(test);
		foreach(var item in list)
		{
			Debug.Log(item.listTest);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
//冒泡排序
public class BubbleSort
{
	public void Sort(int[] args)
	{
		int n = args.Length;
		for(int m = n; m >= 0; m--)
		{
			for(int j = 0; j < m - 1; ++j)
			{
				if(args[j + 1] < args[j])
			    {
					Swap(args, j+1, j);
				}

			}
		}
	}
	void Swap(int[] args, int index_1, int index_2)
	{
        int temp = 0;
	    temp = args[index_1];
		args[index_1] = args[index_2];
		args[index_2] = temp;
	}
}
//选择排序, 外层控制
public class SelectSort
{
	public void Sort(int[] args)
	{
		int n = args.Length;
        for(int i = 0; i < n - 1; ++i) //外层到 n - 1
		{
			int index = i;
			for(int j = i + 1; j < n; ++j) //内层到 n
			{
				if(args[j] < args[index])
				{
					Swap(args, index, j);
				}
			}
		}
	}
	void Swap(int[] args, int index_1, int index_2)
	{
        int temp = 0;
	    temp = args[index_1];
		args[index_1] = args[index_2];
		args[index_2] = temp;
	}
}
//插入排序 : 在前面已经排好序的数组中插入新的
public class InsertSort
{
	public void Sort(int[] args)
	{
		int n = args.Length;
        for(int i = 1; i < n; ++i)
		{
			for(int j = i; j > 0; j--)
			{
				if(args[j] < args[j - 1])
				{
					Swap(args, j, j - 1);
				}
			}
		}
	}
    void Swap(int[] args, int index_1, int index_2)
	{
        int temp = 0;
	    temp = args[index_1];
		args[index_1] = args[index_2];
		args[index_2] = temp;
	}
}
//快速排序 ： 递归和分治思想
public class QuickSort
{
    public void Sort(int[] args, int left, int right)
	{
		int _left = left;
		int _right = right;
		int temp = 0; //基准数
        if(left < right)
		{
            temp = args[left]; //将最左边的数作为基准数
		    
			while(left != right) //当left == right的时候，终止，归位基准数
			{
				while(left < right && args[right] >= temp) 	//开始从右面找第一个比基准数小的数
				    right--;
				args[left] = args[right];
				while(left < right && args[left] <= temp)  //从左边找第一个比基准数大的数
				    left++;
				args[right] = args[left];
			}
			args[right] = temp; //归位基准数
			Sort(args, _left, left - 1); //递归排序左边
			Sort(args, right + 1, _right); //递归排序右边
		}
	}
}
//测试
public class StaticTest
{
	public static int a = 0;
	public static int b = 9;
	public static int c = 10;
	public int listTest = 2;
	public static int Property
	{
		get;
		set;
	}
	public static int GetInstance()
	{
		if(a == 0) a = 5; 
		return a;
	}
}
public class StaticInherit : StaticTest   //静态类不能被继承，staticTest如果是静态的会出错
{

	public static void StaticTest()
	{
        b = 6;   //经测试， 静态字段和属性都可以被继承
		Property = 0;
		GetInstance(); //静态方法可以被继承
	}
	public void ListTest()
	{
		listTest = 10;
	}
	// public new static int GetInstance()  //可以用new隐藏继承的成员方法
	// {

	// }
}

