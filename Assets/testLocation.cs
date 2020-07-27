using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLocation : MonoBehaviour {

   void OnGUI()
   {
	   GUI.Label(new Rect(Screen.width-200, Screen.height - 200, 300, 200), string.Format("Install Location is-----{0}", Application.persistentDataPath));
   }
	// Use this for initialization
	void Start () {
		Debug.Log(string.Format("Install Location is------{0}", Application.persistentDataPath));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
