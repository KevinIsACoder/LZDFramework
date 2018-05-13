using System;
using System.Collections.Generic;
using UnityEngine;
public class Facade{

   private GameObject GameManager;
   private IController m_controller;
   private Dictionary<string,object> m_Managers;

   public GameObject AppGameManager{
       get{
           if(GameManager == null){
               GameManager = GameObject.Find("GameManger");
               if(GameManager == null){
                   GameManager = new GameObject("Gamemanager");
                   GameObject.DontDestroyOnLoad(GameManager);
               }
           }
           return GameManager;
       }
   }
   protected Facade(){
        InitFramework();
   }

   public virtual void InitFramework(){
       m_controller = Controller.Instance;
       m_Managers = new Dictionary<string, object>();
   }

   public virtual void RegisterCommand(string msg,Type command){
       if(!m_controller.HasCommand(msg)) m_controller.RegisterCommand(msg,command);
   }

   public virtual bool HasCommand(string msg){
       return m_controller.HasCommand(msg);
   }
   public void ExecuteCommand(string message,object body = null){
       m_controller.ExecuteCommand(new Message(message));
   }
   public void RemoveCommand(string msgName){
       m_controller.RemoveCommand(msgName);
   }

   public virtual void AddManager(string name,object type){
       if(!m_Managers.ContainsKey(name) && type != null)
         m_Managers.Add(name,type);
   }

   public virtual void AddManager<T>(string name) where T:Component{
       if(m_Managers.ContainsKey(name)) return;
       Component c = AppGameManager.AddComponent<T>();
       if(c != null) m_Managers.Add(name,c);
   }

   public virtual T GetManager<T>(string name) where T:Component {
       if(!m_Managers.ContainsKey(name)) return default(T);
       object result = null;
       m_Managers.TryGetValue(name,out result);
       return (T)result;
   }

   public virtual void RemoveManager<T>(string name) where T:Component{
       if(!m_Managers.ContainsKey(name)) return;
       object manager = m_Managers.TryGetValue(name,out manager);
       if(typeof(T).IsSubclassOf(typeof(MonoBehaviour))){
          GameObject.Destroy((Component)manager);
       }
       m_Managers.Remove(name);
   }
}