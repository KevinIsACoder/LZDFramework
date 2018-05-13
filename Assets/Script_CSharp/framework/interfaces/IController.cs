using System;
public interface IController
{
    void RegisterCommand(string msgName,Type commandType);
    void RemoveCommand(string msgName);
    bool HasCommand(string msgName);
    void ExecuteCommand(IMessage msg);
}