using System;
public interface IMessage
{
    string Name{get;set;} //执行命令名称
    object Body{get;set;}

    string CommandType{get;set;}
}