using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HintEvent : MonoBehaviour,IPointerExitHandler, IPointerEnterHandler
{
    [Header("���ò㼶")]
    public int layerMaskIndex;
    GameObject hint;
    [Header("��Ҫ��ʾ����Ϣ")]
    public string hintMessage;
    [Header("ƫ�Ƽ����Ŀ�꣨Ĭ������")]
    public GameObject offsetObj;
    public void OnPointerEnter(PointerEventData eventData)//��������UI��ִ�е��¼�ִ�е�
    {
        if(offsetObj==null||offsetObj.GetComponent<RectTransform>()==null)
        {
            offsetObj = eventData.pointerEnter.gameObject;
        }
        if(gameObject.layer==layerMaskIndex)
        {
            Transform canvas = GameObject.Find("HintCanvas").transform;
            hint = Instantiate(Resources.Load<GameObject>("Prefabs/Hint"),canvas);
            float offset = offsetObj.GetComponent<RectTransform>().sizeDelta.y / 2;
            hint.GetComponentInChildren<UnityEngine.UI.Text>().text = hintMessage;
            hint.transform.position = eventData.position+Vector2.up*offset;
        }
    }
    public void OnPointerExit(PointerEventData eventData)//������뿪UI��ִ�е��¼�ִ�е�
    {
        if(hint!=null)
        {
            Destroy(hint);
        }
    }
}
