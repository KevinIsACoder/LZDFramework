using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//AUTHOR : 梁振东
//DATE : 07/24/2019 20:39:01
//DESC : ****
public class ChangeToPng : MonoBehaviour
{

    public Texture[] srcTextures;
    public Shader outputShader;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < srcTextures.Length; ++i)
        {
            SaveRenderTextureToPNG(srcTextures[i], outputShader, "Assets/Images/", "test" + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool SaveRenderTextureToPNG(Texture inputTex,Shader outputShader, string contents, string pngName)
	{
		RenderTexture temp = RenderTexture.GetTemporary(inputTex.width, inputTex.height, 0, RenderTextureFormat.ARGB32);
		Material mat = new Material(outputShader);
		Graphics.Blit(inputTex, temp, mat);
		bool ret = SaveRenderTextureToPNG(temp, contents,pngName);
		RenderTexture.ReleaseTemporary(temp);
		return ret;
 
	} 
 
	//将RenderTexture保存成一张png图片
	public bool SaveRenderTextureToPNG(RenderTexture rt,string contents, string pngName)
	{
		// RenderTexture prev = RenderTexture.active;
		// RenderTexture.active = rt;
		// Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
		// png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		// byte[] bytes = png.EncodeToTGA();
		// if (!Directory.Exists(contents))
		// 	Directory.CreateDirectory(contents);
		// FileStream file = File.Open(contents + "/" + pngName + ".tga", FileMode.Create);
		// BinaryWriter writer = new BinaryWriter(file);
		// writer.Write(bytes);
		// file.Close();
		// Texture2D.DestroyImmediate(png);
		// png = null;
		// RenderTexture.active = prev;
		return true;
 
	}
}
