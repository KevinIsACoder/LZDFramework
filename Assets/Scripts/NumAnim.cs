using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : #DESC#
public class NumAnim : MonoBehaviour {
    
	[SerializeField]
	public AnimationCurve animationCurve;
	[SerializeField]
	private int targetNum;
	[SerializeField]
	private float duration;
	[SerializeField]
	public Text numText;
	[SerializeField]
	private int curNum;
	void Start()
	{
		ChangeToNum(targetNum, duration);
		CreateAnimationCurve();
	}
	void Update()
	{
		curNum = (int)animationCurve.Evaluate(Time.time);
		numText.text = curNum.ToString();
	}
	public void ChangeToNum(int target, float duration)
	{
		targetNum = target;
		this.duration = duration;
	}

	AnimationCurve CreateAnimationCurve()
	{
		animationCurve = new AnimationCurve();
		animationCurve.AddKey(0, 0);
		animationCurve.AddKey(duration, targetNum);
		return animationCurve;
	}
}



