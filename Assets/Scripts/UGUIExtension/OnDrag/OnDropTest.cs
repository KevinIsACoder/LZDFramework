using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnDropTest : MonoBehaviour, IDropHandler 
{
	public void OnDrop(PointerEventData eventData)
	{
		gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
	}
}
