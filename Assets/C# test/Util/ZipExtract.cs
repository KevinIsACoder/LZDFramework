using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.IO;
public class ZipExtract : MonoBehaviour 
{
	private string filePath = "";
	private string outfilepath = "";	
	void Awake()
	{
		filePath = Application.streamingAssetsPath + "/locales.sqdb.zip";
		outfilepath = Application.persistentDataPath + "/locales.sqdb.zip";
	}
	void Start()
	{
		UnZipFile(filePath, outfilepath);
	}
	void UnZipFile(string filename, string outfilepath)
	{
		#if !UNITY_EDITOR && UNITY_ANDROID
			WWW wWW = new WWW(filename);
			while(!wWW.isDone){};
			File.WriteAllBytes(outfilepath, wWW.bytes);
		#else
			if(File.Exists(filename))
			File.Copy(filename, outfilepath, true);
		#endif

		try
		{
			FileStream fs = new FileStream(outfilepath, FileMode.Open);
			ZipFile zf = new ZipFile(fs);
			if(zf.TestArchive(true) == false)
			{
				Debug.Log("Zip File Failed");
			}
			else
			{
				foreach( ZipEntry zipEntry in zf )
                {
                   // Ignore directories
                   if( !zipEntry.IsFile )
                       continue;          
                   
                   string entryFileName = zipEntry.Name;
                   
                   // Skip .DS_Store files (these appear on OSX)
                   if( entryFileName.Contains( "DS_Store" ) )
                       continue;
                   
                   Debug.Log( "Unpacking zip file entry: " + entryFileName );
                   
                   byte[] buffer = new byte[ 4096 ];     // 4K is optimum
                   Stream zipStream = zf.GetInputStream( zipEntry );
 
                   // Manipulate the output filename here as desired.
                   string fullZipToPath = "c:\\" + Path.GetFileName( entryFileName );
 
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
		catch(System.Exception ex)
		{
			Debug.LogError(ex.Message);
		}
	}

}
