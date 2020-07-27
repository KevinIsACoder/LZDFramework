using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ZipExtract : MonoBehaviour
{
    private string filePath = "";
    private string outfilepath = "";
    string testPath = "";
    string testFile = "";
    public Text progressText;
    WWW wwwDB;
    void Awake()
    {
        //filePath = Application.streamingAssetsPath + "/locales.sqdb.zip";
        filePath = "http://192.168.143.155/lzdtest.zip";
        outfilepath = Application.persistentDataPath + "/locales.sqdb.zip";
        testPath = Application.persistentDataPath + "/file.txt";
        testFile = "http://192.168.143.155/file.txt";
    }
    void Start()
    {
        string url = "";
        //UnityWebRequest webRequest = UnityWebRequest.Get()
        //UnZipFile(filePath, outfilepath);
	    StartCoroutine(UnZipFileTest());
    }
    void Update()
    {
        if(wwwDB != null)
        {
            progressText.text = string.Format("{0:F2}/{1}", wwwDB.progress * 9, 9);
            //Debug.Log("update Progress----" + wwwDB.progress);
        }
    }
    IEnumerator DownLoadSqdbData(string filename, string outfilePath)
    {
        WWW wWW = new WWW(filename);
        yield return wWW;
        Debug.Log("WWW Load " + wWW.bytesDownloaded);
        File.WriteAllBytes(outfilePath, wWW.bytes);
    }
    void UnZipFile(string filename, string outfilepath)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
			WWW wWW = new WWW(filename);
			while(!wWW.isDone) 
            {

            };
			if(!string.IsNullOrEmpty(wWW.error))
			{
				Debug.Log("WWW Error " + wWW.error);
			}
			File.WriteAllBytes(testPath, wWW.bytes);
			//StartCoroutine(DownLoadSqdbData(filename, outfilepath));
#else
        if (File.Exists(filename))
            File.Copy(filename, outfilepath, true);
#endif

        try
        {
            FileStream fs = new FileStream(outfilepath, FileMode.Open);
            ZipFile zf = new ZipFile(fs);
            if (zf.TestArchive(true) == false)
            {
                Debug.Log("Zip File Failed");
            }
            else
            {
                foreach (ZipEntry zipEntry in zf)
                {
                    // Ignore directories
                    if (!zipEntry.IsFile)
                        continue;

                    string entryFileName = zipEntry.Name;

                    // Skip .DS_Store files (these appear on OSX)
                    if (entryFileName.Contains("DS_Store"))
                        continue;

                    Debug.Log("Unpacking zip file entry: " + entryFileName);

                    byte[] buffer = new byte[4096];     // 4K is optimum

                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    string fullZipToPath = Application.persistentDataPath + "/" + Path.GetFileName(entryFileName);
                    Debug.Log("destpath is  " + fullZipToPath);
                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
                zf.IsStreamOwner = false;
                zf.Close();
                fs.Close();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    IEnumerator UnZipFileTest()
    {
        //string offline_db = System.IO.Path.Combine(Application.streamingAssetsPath, "locales.sqdb.zip");
        string DbUrl = "http://192.168.143.155/lzdtest.zip";
        string outPath = Application.persistentDataPath + "/locales/";
        wwwDB = new WWW(DbUrl);
        while(!wwwDB.isDone)
        {
            yield return null;
            // progressText.text = string.Format("{0:F2}/{1}", wwwDB.progress * 9, 9);
            // Debug.Log("update Progress----" + wwwDB.progress);
        }
        //progressText.text = string.Format("{0:F2}/{1}", wwwDB.progress * 9, 9);
        if (!string.IsNullOrEmpty(wwwDB.error))
        {
            Debug.LogError("---WWW Load locales.sqdb.zip Failed---" + wwwDB.error);
            yield break;
        }
        string DbPath = outPath + "locales.sqdb.zip";
        File.WriteAllBytes(DbPath, wwwDB.bytes);

        try
        {
            FileStream fs = new FileStream(DbPath, FileMode.Open);
            ZipFile zf = new ZipFile(fs);
            foreach (ZipEntry zipEntry in zf)
            {
                if (!zipEntry.IsFile)
                    continue;
                string entryFileName = zipEntry.Name;
                Debug.Log("Unpacking zip file entry: " + entryFileName);
                byte[] buffer = new byte[4096];
                Stream zipStream = zf.GetInputStream(zipEntry);

                using (FileStream streamWriter = File.Create(outPath + "locales.sqdb"))
                {
                    StreamUtils.Copy(zipStream, streamWriter, buffer);
                }
            }
            zf.IsStreamOwner = false;
            zf.Close();
            fs.Close();
            File.Delete(DbPath);
        }
        catch (System.Exception ex)
        {
            Debug.Log("DI8n LoadData Method Extract locales.sqdb.zip faild:  " + ex.Message);
        }
    }

}