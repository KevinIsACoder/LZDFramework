using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : #DESC#
public class BitTest : MonoBehaviour
{
    public int houseNum = 3; //初始房间数量
    public string targetStatus;
    private int operationNum; //至少需要操作的步骤数
    void Operation()
    {
        char curNum = ' ', preNum = ' ';
        for(int i = 0; i < targetStatus.Length; ++i)
        {
            curNum = targetStatus[i];
            if(targetStatus[0] == '1' && i == 0) operationNum++;
            if(curNum != preNum && i > 0)
            {
                operationNum++;
            }
            preNum = curNum;
        }
    }
    void Start()
    {
        operationNum = 0;
        Operation();
        Debug.Log(operationNum);
    }
}
