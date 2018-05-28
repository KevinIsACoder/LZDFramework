using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UCroutine = UnityEngine.Coroutine;
public class DownLoadManager:Mysingleton<DownLoadManager>{

    [Range(1,10)]
    public int m_maxTasks = 5;
    
    private List<string> TaskList; //下载任务列表
    private Queue<string> TaskQueue; //下载队列
    private Dictionary<string,WWWTask> taskDic; //下载记录
    
    public WWWTask NewTask(string url,byte[] postData,Action<WWWTask,bool> callback){
       callback += OnComplete;
       WWWTask task = gameObject.AddComponent<WWWTask>();
       task.SetTask(url,postData,callback);
       if(TaskList.Count > m_maxTasks){
         TaskQueue.Enqueue(url);
         taskDic.Add(url,task);
       }
       else{
         TaskList.Add(url);
         taskDic.Add(url,task);
         task.StartTask();
       }

       return task;
    }

    public void NextTask(){
       if(TaskQueue.Count <= 0) return;
       WWWTask task = taskDic[TaskQueue.Dequeue()];
       task.StartTask();
    }
    
    public void OnComplete(WWWTask task,bool iscomplete){
      Debug.Log("Task complete->" + task.url);
      TaskList.Remove(task.url);
      taskDic.Remove(task.url);
      NextTask();
    }

    public bool HasTask(string url){
        return taskDic.ContainsKey(url);
    }

    public bool IsRunning(string url){
        return TaskList.Contains(url);
    }
   
}

 public class WWWTask:MonoBehaviour{

          public string url;
          public byte[] postData;
          public Action<WWWTask,bool> callback;
          public UCroutine cor;
          
          public void SetTask(string Url,byte[] PostData,Action<WWWTask,bool> CallBack){
              url = Url;
              postData = PostData;
              callback = CallBack;
          }
          public void StartTask(float timeOut = 600){

              cor = StartCoroutine(co_Task(timeOut));
          }

          IEnumerator co_Task(float timeOut){
              yield return null;
              WWW www = new WWW(url,postData);
              while(!www.isDone){
                timeOut = timeOut - Time.deltaTime;
                if(timeOut <= 0){
                   StopTask();
                }
                yield return null;
              }
              if(callback != null){
                 if(www.error == null) callback(this,true);
                 else callback(this,false);
              }
              Destroy(this);
          }

          public void StopTask(){
              StopCoroutine(cor);
              if(callback != null) callback(this,false);
              Destroy(this);
          }
}