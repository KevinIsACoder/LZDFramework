using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//AUTHOR : 梁振东
//DATE : 09/17/2019 14:03:11
//DESC : ****
public class NetStreamBuff : MonoBehaviour
{
    const int MAX_MSG_LENGTH = 1024 * 512;
    byte[] buff;
    int curPos;
    public NetStreamBuff()
    {
        buff = new byte[MAX_MSG_LENGTH];
        curPos = 0;
    }
    public void Clear()
    {
        curPos = 0;
    }
    public void AddBuff(byte[] bytes, int index, int length)
    {
        EnsureCapacity(curPos + length);
    }
    void EnsureCapacity(int size)
    {
        if(size > MAX_MSG_LENGTH)
        {
            int newSize = System.Math.Max(size, 2 * MAX_MSG_LENGTH);
            Array.Resize(ref buff, newSize);
        }
    }
    public NetMessage ParseMessage()
    {
        if(curPos < NetMessage.PACKAGE_LIMIT)
        {
            return null;
        }
        int messagesize = BitConverter.ToInt32(buff, 0);
        //if(BitConverter.IsLittleEndian) messagesize = 
        
    }
    void Remove(int messageSize)
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
