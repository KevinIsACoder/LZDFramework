//AuthorName : 梁振东;
//CreateDate : 9/23/2019 10:05:23 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//队列，用数组实现一个循环队列
public class QueueDataStructure : MonoBehaviour 
{
    private List<int> myqeue;
	private int size;
	private int head; //头指针
	private int tail; //尾指针
 	public QueueDataStructure(int size)
	{
	   int[] test = new int[7];
       this.size = size;
	   head = -1;
	   tail = -1;
	}
	public bool IsEmpty() //判断队列是否空
	{
        return head == -1;
	}
	public bool IsFull() //是否满的
	{
	   	return ((tail + 1) % size == head);
	}
	public bool Eequeue(int x) //入队
	{  
		if(IsFull()) return false; //队列已满
		if(IsEmpty())
		{
			head = 0;
			return false;
		}
		tail = (tail + 1) % size;
		myqeue[tail] = x;
		return true;
	} 
	public bool Dequeue(int x) //出队
	{
		if(IsEmpty()) return false;
		if(head == tail)
		{
			head = -1;
			tail = -1;
			return true;
		}
		head = (head + 1) % size;
		return true;
	}
}
