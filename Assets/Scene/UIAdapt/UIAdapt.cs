using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UIAdapt : MonoBehaviour {
     
	public int manualHeight = 720;
	public int manualWidth = 1280;
	public UIRoot uIRoot;
	void Awake()
	{
		AdaptUI();
	}
	void AdaptUI()
	{
		if(uIRoot != null)
		{
			if(Screen.height * 1.0f / Screen.width > manualHeight * 1.0f / manualWidth)
			{
				uIRoot.manualHeight = Mathf.RoundToInt(manualWidth / Screen.width * Screen.height);
			}
			else
			{
				uIRoot.manualHeight = manualHeight;
			}
		}
	}
}
