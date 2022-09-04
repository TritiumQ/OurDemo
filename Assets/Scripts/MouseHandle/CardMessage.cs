using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardMessage : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [Header("���ò㼶")]
    public int layerMaskIndex;
    GameObject hintCard;
    [Header("��Ҫ��ʾ����Ϣ")]
    public string hintMessage;
    public void OnPointerEnter(PointerEventData eventData)//��������UI��ִ�е��¼�ִ�е�
    {
        if (gameObject.layer == layerMaskIndex)
        {
            Transform canvas = GameObject.Find("HintCanvas").transform;
            hintCard = Instantiate(gameObject, canvas);

        }
    }
    public void OnPointerExit(PointerEventData eventData)//������뿪UI��ִ�е��¼�ִ�е�
    {
        if (hintCard != null)
        {
            Destroy(hintCard);
        }
    }
}
