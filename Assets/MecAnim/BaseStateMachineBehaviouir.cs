using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author:#AUTHORNAME#
// Date:#DATE#
// DESC:#Desc#
public class BaseStateMachineBehaviouir : StateMachineBehaviour {
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("OnStateEnter");
	}
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("OnStateExit");
	}
	public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("OnStateIK");
	}
	public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("OnStateMove");
	}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("OnStateUpdate");
	}
}
