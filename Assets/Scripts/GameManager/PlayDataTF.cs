using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayDataTF
{
    //��Ϸ������������ڸ��¼�������������ݽ�����תվ
    private static bool playerAllow = false; //�Ƿ������ȡ�������
    private static bool cardAllow = false;//�Ƿ������ȡ��������
    private static bool eventAllow = false;//�Ƿ������ȡ�¼����
    
    //������ݻ�����
    public static string name;   //���Ӧ�ò��øİ�
    public static int maxHP;
    public static int currentHP;
    public static int mithrils; //����
    public static int tears;  //���
    static List<int> cardSet=new List<int>(); //����

    //�¼����
    private static bool eventgo = false;

    public static void GetData(Player _player) //Player������ȡ�����Ϣ
    {
        if(playerAllow==true)
        {
            _player.name = name;
            _player.maxHP = maxHP;
            _player.currentHP = currentHP;
            _player.mithrils = mithrils;
            _player.tears = tears;
            playerAllow = false;
        }
        if (cardAllow == true)
        {
            for (int i = 0; i < cardSet.Count; i++)
            {
                if (cardSet[i] >= 0)
                {
                    _player.cardSet[i] = cardSet[i];
                }
            }
            cardAllow = false;
        }
    }
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
    //������7������������ݷ���
    public static void SetData(string _name,int _maxHP,int _currentHp,int _mithrils,int _tears)
    {
        name = _name;
        maxHP = _maxHP;
        currentHP = _currentHp;
        mithrils = _mithrils;
        tears = _tears;
        playerAllow = true;
    }
    public static void SetCurrentHP(int _value)
    {
        currentHP = _value;
        playerAllow = true;
    }
    public static void SetMaxHP(int _value)
    {
        maxHP = _value;
        playerAllow = true;
    }
    public static void AddCurrentHp(int _value)
    {
        currentHP += _value;
        playerAllow = true;
    }
    public static void AddMaxHp(int _value)
    {
        maxHP += _value;
        playerAllow = true;
    }
    public static void SetMoney(int _mithrils, int _tears)
    {
        mithrils = _mithrils;
        tears = _tears;
        playerAllow = true;
    }
    public static void AddMoney(int _mithrils, int _tears)
    {
        mithrils += _mithrils;
        tears += _tears;
        playerAllow = true;
    }
    
    //�����޸Ŀ���ķ���
    public static void SetCard(List<int> _newCardSet)
    {
        //��ʱ�޷��޸�ʹ�ÿ�����������
        for(int i=0;i<_newCardSet.Count;i++)
        {
            if (cardSet.Count > i)
                cardSet[i] = _newCardSet[i];
            else cardSet.Add(_newCardSet[i]);
        }
        cardAllow = true;
    }
    public static void AddCard(int _newCard)
    {
        cardSet.Add(_newCard);
        cardAllow = true;
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
