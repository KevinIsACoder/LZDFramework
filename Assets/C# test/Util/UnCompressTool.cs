using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Compression;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : ****
[Serializable]
public class UnCompressTool 
{
	[Serializable]
	public enum Filefilter
	{
		ZIP,
		TAR
	};

	[SerializeField]
	public string filePath;
	[SerializeField]
	public bool overWritting;
	[SerializeField]
	public Filefilter[] filefilter;
	public void OnUnCompress()
	{
		if(!File.Exists(filePath))
		{
			Debug.LogError(string.Format("File Path is not Exist! " + filePath ));
		}

		using(ZipInputStream s = new ZipInputStream(File.OpenRead(filePath)))
		{
			//FastZip
		}
	}

	void ListZipFiles(string fileName)
	{
		using(ZipFile zipFile = new ZipFile(fileName))
		{
			//var fileter = new PathFilter()
			if(zipFile.Count <= 0)
			{
				throw new Exception("Has no Zip File");
			}
			else
			{
				for(int i = 0; i < zipFile.Count; ++i)
				{
					ZipEntry zipEntry = zipFile[i];
					string path = Path.GetFileName(zipEntry.Name);
					for(int j = 0; j < filefilter.Length; ++i)
					{
						//if(filefilter[i])
					}
				}
			}
		}
	}
}
