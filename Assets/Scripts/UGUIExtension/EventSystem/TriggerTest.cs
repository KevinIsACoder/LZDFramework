using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : #DESC#
public class TriggerTest : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler 
{
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Position is--- " +  eventData.position);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("delta is ----- " + eventData.delta);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("name is ----" + eventData.pointerEnter.name);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("" + eventData.pointerEnter.name);
	}
}
