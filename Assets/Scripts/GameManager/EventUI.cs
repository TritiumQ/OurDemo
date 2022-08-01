using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/*
 * ������Ϸ�¼����ͣ�
 * 1.ս��
 * 2.
 * 3.
 * 4.�̵�
 * 5.bossս
 * 
 */
public class EventUI : MonoBehaviour
{
    // �洢�¼�����������¼�ͼ��
    public int eventSign = 0; //�����Ϸ�¼�����
    public bool isPass = false; //�����Ϸ�¼��Ƿ�ͨ��
    public int id;

    public GameObject iconBattle;
    public GameObject icon;
    public GameObject iconMeet;
    public GameObject iconStore;
    public GameObject iconBoss;
    void Start()
    {

    }
    void Update()
    {
        Refresh();
    }
    public void SetEventSign(int _value)//�����¼�����
    {
        eventSign = _value;
        //�������
        switch (eventSign)
        {
            case 1:
                {
                    iconBattle.SetActive(true);
                }
                break;
            case 2:
                {
                    icon.SetActive(true);
                }
                break;
            case 3:
                {
                    iconMeet.SetActive(true);
                }
                break;
            case 4:
                {
                    iconStore.SetActive(true);
                }
                break;
            case 5:
                {
                    iconBoss.SetActive(true);
                }
                break;
            default:
                {
                }
                break;
        }
    }
    void Refresh()
    {
        if(isPass==true)
        {
            //��Ϸ�¼�ͼ����
        }
        
    }
}
