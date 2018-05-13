using System;
using System.Collections.Generic;
public class Controller:IController{

    private Dictionary<string,Type> m_Controllers;
    private static readonly object SyncObject = new object();
    
    private static readonly object syncObject = new object();
    protected Controller(){
        m_Controllers = new Dictionary<string, Type>();
    }

    private static Controller m_instance;
    public static Controller Instance{
         get{
             if(m_instance == null){
                 lock(SyncObject){
                    m_instance = new Controller();
                 }
             }
             return m_instance;
         }
    }

    public virtual void RegisterCommand(string msgName,Type type){
          lock(syncObject){
              if(!m_Controllers.ContainsKey(msgName)) m_Controllers.Add(msgName,type);
          }
    }

    public virtual bool HasCommand(string msgName){
         lock(syncObject){
             return m_Controllers.ContainsKey(msgName);
         }
    }

    public virtual void ExecuteCommand(IMessage msg){
        lock(syncObject){
            Type commandType = null;
            if(m_Controllers.TryGetValue(msg.Name,out commandType)){
             object commandInstance = Activator.CreateInstance(commandType);
               if(commandInstance is ICommand){
                 ((ICommand)commandInstance).Execute(msg);
               }
            }
        }
    }

    public virtual void RemoveCommand(string msgName){
        lock(syncObject){
            if(m_Controllers.ContainsKey(msgName))
            m_Controllers.Remove(msgName);
        }
    }
}