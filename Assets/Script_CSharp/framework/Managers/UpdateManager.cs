using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//热更新步骤： 1.第一次安装需要先进行解压，也就是将资源从安装目录（application.StreammingAssets）解压到运行目录(application.persistPath)
//这个持久化路径，如果不是第一次安装，就直接进行更新步骤，
public class UpdateManager:Mysingleton<UpdateManager>{

    public class FileInfo{
        public string md5Value;

        public FileInfo(string Value){
           this.md5Value = Value;
        }
    }

    private char SEPARATE = '|';
    private Dictionary<string,FileInfo> m_fileInfo;
    private List<string> fileList;

    private List<string> downLoadList;  //已更新文件

    private List<string> updateList; //需要更新的文件

    // 在进游戏时会忽略该前缀的更新
    public string ignorePrefix = "";
     // 在进游戏时会忽略该后缀的更新
    public string ignoreSuffix = "";

    private string filetxt = "file.txt";

    private string m_dataPath;
    private string m_respath;

    public float timeOut = 10;
    void Start() {
        m_fileInfo = new Dictionary<string, FileInfo>();
        fileList = new List<string>();
        downLoadList = new List<string>();
         
        m_respath = Application.streamingAssetsPath;
        if(Appconst.DebugMode) m_dataPath = Application.dataPath + "/" + Appconst.gameName + "/";
        else m_dataPath = Application.persistentDataPath + "/" + Appconst.gameName + "/";
    }
    
    public void StartUpdate(Action callback){ 
       StartCoroutine(cor_StartUpdate(callback));
    }

    IEnumerator cor_StartUpdate(Action callback){
        yield return cor_Exact();
        yield return cor_Update();
        callback();
    }
    private void LoadFileList(){
        m_fileInfo.Clear();
        string[] str = File.ReadAllLines(m_dataPath);
        for(int i = 0; i < str.Length;++i){
            if(string.IsNullOrEmpty(str[i])) continue;
            string[] fileinfo = str[i].Split(SEPARATE);
            fileinfo[1] = fileinfo[1].Trim();
            m_fileInfo.Add(fileinfo[0],new FileInfo(fileinfo[1]));
        }
    }
    IEnumerator cor_Exact(){
        yield return null;
        Debug.Log("正在解压....");
        string filePath = m_respath + filetxt;
        string outfilePath = m_dataPath + filetxt;
        if(Directory.Exists(filePath)) yield break;  //如果不是第一次安装，无需解压
        Directory.CreateDirectory(outfilePath);
        yield return cor_ExactFile(filePath,outfilePath);
        LoadFileList();
        float progress = 0;
        foreach(KeyValuePair<string,FileInfo> pairs in m_fileInfo){
            progress++;
            Debug.Log(string.Format("正在解压....{0}/{1}",progress,m_fileInfo.Count));
            string sourcePath = m_respath + pairs.Key;
            string destPath = m_dataPath + pairs.Key;
            if(!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
            yield return cor_ExactFile(sourcePath,destPath);
        }
    }

    IEnumerator cor_Update(){
        yield return null;
        Debug.Log("正在更新....");
        string remotePath = Appconst.url + Appconst.streammingAssets + "/" + filetxt;
        string localpath = m_dataPath + filetxt;
        yield return cor_UpdateFile(remotePath,localpath,timeOut);
        LoadFileList();
        List<string>  UpdateList = new List<string>(); //需要更新的文件列表
        foreach(var fileinfo in m_fileInfo){
            if(fileinfo.Key.StartsWith(ignorePrefix)) continue;
            if(fileinfo.Key.EndsWith(ignoreSuffix)) continue;
            if(CheckUpdate(fileinfo.Key)){
               updateList.Add(fileinfo.Key);
            }
        }
        float progress = 0;
        for(int i = 0;i < updateList.Count;++i){
            progress++;
            Debug.Log(string.Format("正在更新....{0}/{1}",progress,updateList.Count));
            string sourthPath = Appconst.url + Appconst.streammingAssets + "/" + updateList[i];
            string destPath = m_dataPath + updateList[i];
            yield return cor_UpdateFile(sourthPath,destPath,timeOut);
        }
    }

    private bool CheckUpdate(string file){
        string localpath = m_dataPath + file;
        if(Appconst.DebugMode) return false;  //如果是调试模式的话不启用更新
        else if(!Directory.Exists(localpath)) return true;
        else{
          string md5 = utility.Md5File(localpath);
          FileInfo fileinfo;
          if(m_fileInfo.TryGetValue(file,out fileinfo)){
              if(md5 != fileinfo.md5Value) return true;
              else return false;
          }
        }
        return false;
    }
    IEnumerator cor_ExactFile(string sourcePath,string destPath){      
        if(Application.isMobilePlatform){
          WWW wwwfile = new WWW(sourcePath);
          yield return wwwfile;
          if(wwwfile.isDone)  File.WriteAllBytes(destPath,wwwfile.bytes);
        }
        else{
          File.Copy(sourcePath,destPath,true);
        }
    }

    IEnumerator cor_UpdateFile(string sourcePath,string destPath,float timeout = float.PositiveInfinity){
        yield return null;
        if(!Directory.Exists(sourcePath)) yield break; //如果远程不存在文件，结束这个文件的跟新
        if(!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
        float time = 0;
        WWW www = new WWW(sourcePath);
        while(!www.isDone){
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            if(time >= timeout) break;
        }
        if(www.error == null){
            File.WriteAllBytes(destPath,www.bytes);
            downLoadList.Add(sourcePath);
        }
    }

    public WWWTask DownLoad(string file,Action<WWWTask,bool> callback){
        file = Appconst.url + Appconst.streammingAssets + "/" + file;
        file = file.ToLower();
        string localPath = m_dataPath + file;
        callback += delegate(WWWTask task,bool isSuceed){
            if(isSuceed) downLoadList.Add(file);
            File.WriteAllBytes(localPath,task.postData);
        };
        return DownLoadManager.Instance.NewTask(file,null,callback);
    }

    public bool IsDownLoading(string file){
        string url = (Appconst.url + Appconst.streammingAssets + "/" + file).ToLower();
        return DownLoadManager.Instance.IsRunning(url);
    }

    public bool IsSucceed(string file){
        string url = (Appconst.url + Appconst.streammingAssets + "/" + file).ToLower();
        return downLoadList.Contains(url);
    }
}