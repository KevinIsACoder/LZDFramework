using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author:#AUTHORNAME#
// Date:#DATE#
// DESC:#Desc#
public class AniEvent : MonoBehaviour 
{
	public int intValue;
	public float floatValue;
	public void SetFloat(float value)
	{

	}
	public void SetInt(int value)
	{

	}
	void Start()
	{
		WalkStateMachineBehaviouir walkStateMachineBehaviouir = new WalkStateMachineBehaviouir();
		Animator animator = new Animator();
		AnimatorStateInfo animatorStateInfo = new AnimatorStateInfo();
		walkStateMachineBehaviouir.OnStateUpdate(animator, animatorStateInfo, 0);
	}
}
