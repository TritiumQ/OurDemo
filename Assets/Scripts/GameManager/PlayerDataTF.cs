using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataTF
{
    //��Ϸ������������ڸ��¼����������ݽ�����תվ
    private static bool eventAllow = false;//�Ƿ������ȡ�¼����

    //�¼����
    private static bool eventgo = false;


    public static int GetResult() //��ȡ�¼����
    {
        if(eventAllow==true)
        {
            eventAllow = false;
            if (eventgo == true)
                return 1; //�¼�ͨ��
            else return -1;//�¼�ʧ��
        }
        else return 0; //����Ӧ
    }

    //�����������¼�������ݴ���
    public static void EventContinue()
    {
        eventgo = true;
        eventAllow = true;
    }
    public static void EventEnd()
    {
        eventgo = false;
        eventAllow = true;
    }
    
}
