using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//协程部分被转换成了一个迭代器，本质是一个状态机。函数体从yield return的位置被切分开，在MoveNext函数内，针对不同的协程状态执行不同的分支，每走一次分支状态值加1。

// yield return后面的部分则被设置成current，也就是迭代器的当前值，最终被返回。
// MoveNext的返回值为false时，表示执行到了协程的终点。
// 每次执行协程，都会在堆上生成一个迭代器对象，这是GC部分的来源。
// 协程里使用的临时变量都会被转成类字段，而且至少有一个父级对象的引用。这就是协程GC的大小。

// 根据有Unity源码的人提供的代码，StartCoroutine
// （以及诸如IEnumerator Start()这些系统自动调用StartCoroutine的地方）会先调用一次迭代器的MoveNext，然后根据其current的值来生成一个C++端的delayCall对象塞进MonoBehaviour的数组里。如果你的current值是WaitForSeconds，
// 就会设置成“按时间等待”的枚举，记录开始时间和时长。然后如果是null或者其他任意值，就会设置成“下一帧执行”枚举。
// 然后MonoBehaviour的各个事件阶段（Update,FixUpdate什么的）里，会遍历一下这个delayCall列表，挑出符合类型的delayCall对象。如果有时间限制就检查一下时间，通过后执行对应迭代器的MoveNext方法，然后再重复一遍上面的逻辑，直到MoveNext返回false。这就是协程延时执行的原理。

//Lua中的携程是可以暂停的，unity中的协程是不可以暂停的

// 禁用MonoBehaviour并不会暂停或者终止协程。
// 不知道为啥，禁用GameObject却能终止协程，这里要格外注意。
// 协程只能终止不能暂停（但是基于Update的等待，还是可以用过timeScale=0来“暂停”）
// 禁用GameObject错误终止协程这个事情让人很恶心，很容易产生意外的BUG，所以把协程放在另一个一直存在的GameObject上确实会保险一点。但为了避免协程执行期间MonoBehaviour销毁产生的空引用调用错误，就要记得在OnDestory里及时停止携程。管理类是有必要的。
public class Coroutine : MonoBehaviour {
	IEnumerator Start()
	{
		// int i = 1;
		// Debug.LogWarning(i);
		// yield return new WaitForEndOfFrame();
        // i++;
		// Debug.LogWarning(i);
		yield return null;
		StartCoroutine(TestCoroutine_1());
		StartCoroutine(TestCoroutine_2());
		// yield return TestCoroutine_1();
		// yield return TestCoroutine_2();
		
		Debug.Log("Finished");
	}
    IEnumerator TestCoroutine_1()
	{
		while(true)
		{
			yield return null;
			Debug.Log("TestCoroutine_1");
		}
	}
	IEnumerator TestCoroutine_2()
	{
		yield return null;
		Debug.Log("TestCoroutine_2");
	}
	//不要在子协程的循环中嵌套协程，会导致大量的GC
	IEnumerator StartCoroutine()
    {
        Debug.Log("START");
        for (int i = 0;i < 10;i++)
        {
            yield return Coroutine2();
            Debug.Log(i.ToString());
        }
        Debug.Log("END");
    }
    IEnumerator Coroutine2()
    {
        Debug.Log("IN");
        yield return new WaitForEndOfFrame();
        Debug.Log("OUT");
    }
}

// 2.2 迭代器的执行流程
// 如下的代码，展示了迭代器的执行流程，代码输出(0,1,2,-1)然后终止。

// class Program
// {
//     static readonly String Padding = new String(' ', 30);
//     static IEnumerable<Int32> CreateEnumerable()
//     {
//         Console.WriteLine("{0} CreateEnumerable()方法开始", Padding);
//         for (int i = 0; i < 3; i++)
//         {
//             Console.WriteLine("{0}开始 yield {1}", i);
//             yield return i;
//             Console.WriteLine("{0}yield 结束", Padding);
//         }
//         Console.WriteLine("{0} Yielding最后一个值", Padding);
//         yield return -1;
//         Console.WriteLine("{0} CreateEnumerable()方法结束", Padding);
//     }

//     static void Main(string[] args)
//     {
//         IEnumerable<Int32> iterable = CreateEnumerable();
//         IEnumerator<Int32> iterator = iterable.GetEnumerator();
//         Console.WriteLine("开始迭代");
//         while (true)
//         {
//             Console.WriteLine("调用MoveNext方法……");
//             Boolean result = iterator.MoveNext();
//             Console.WriteLine("MoveNext方法返回的{0}", result);
//             if (!result)
//             {
//                 break;
//             }
//             Console.WriteLine("获取当前值……");
//             Console.WriteLine("获取到的当前值为{0}", iterator.Current);
//         }
//         Console.ReadKey();
//     }
// }

// 直到第一次调用MoveNext，CreateEnumerable中的方法才被调用。
// 在调用MoveNext的时候，已经做好了所有操作，返回Current属性并没有执行任何代码。
// 代码在yield return之后就停止执行，等待下一次调用MoveNext方法的时候继续执行。
// 在方法中可以有多个yield return语句。
// 在最后一个yield return执行完成后，代码并没有终止。调用MoveNext返回false使得方法结束。
