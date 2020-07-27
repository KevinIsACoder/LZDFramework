using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : #DESC#
public class OnDragTest : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler 
{
	public GameObject go;
	public Image image;
	void OnEnable()
	{
		image = GetComponent<Image>();
	}
	public void OnDrag(PointerEventData eventData)
	{
		if(go == null) return;
		Debug.Log(string.Format("Drag eventData is ({0}, {1})", eventData.delta.x, eventData.delta.y));
		go.transform.localPosition = Input.mousePosition;
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		//if(image == null) return;
		//Debug.Log(string.Format("BeginDrag eventData is ({0}, {1})", eventData.delta.x, eventData.delta.y));
		go = Instantiate(eventData.pointerDrag);
		go.transform.SetParent(eventData.pointerDrag.transform.parent, false);
		go.transform.localPosition = eventData.pointerDrag.transform.localPosition;
		go.transform.localScale = Vector3.one;

		go.GetComponent<Image>().sprite = image.sprite;
		Debug.Log("eventData.pointerDrag.transform.localPosition" + eventData.pointerDrag.transform.localPosition);
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log(string.Format("EndDrag eventData is ({0}, {1})", eventData.delta.x, eventData.delta.y));
		Destroy(go);
		go = null;
	}
	public void OnDrop(PointerEventData eventData)
	{
		//A、B对象必须均实现IDropHandler接口，且A至少实现IDragHandler接口
    	//当鼠标从A对象上开始拖拽，在B对象上抬起时 B对象响应此事件
    	//此时name获取到的是B对象的name属性
    	//eventData.pointerDrag表示发起拖拽的对象（GameObject）
		Debug.Log(string.Format("Drog eventData is ({0}, {1})", eventData.delta.x, eventData.delta.y));
		Debug.Log(eventData.pointerDrag.name + " OnDrop to " + name);
	}
}
