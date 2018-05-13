/*"AUTHOR:" 梁振东
  "DATATIME:" 5/13/2018 12:00:16 PM*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartUpCommand : Command {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  
	public override void Execute(IMessage msg){
		 //初始化管理器
     AppFacade.Instance.AddManager<ResourceManager>(ManagerName.Resource);
		 
	}

}
