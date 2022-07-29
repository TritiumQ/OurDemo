using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestShowCard : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public float zoomSize;
    public void OnPointerEnter(PointerEventData eventData)//��������UI��ִ�е��¼�ִ�е�
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, -10f);
    }
    public void OnPointerExit(PointerEventData eventData)//������뿪UI��ִ�е��¼�ִ�е�
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}