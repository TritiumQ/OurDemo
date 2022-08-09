using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
[SerializeField]

public class Player
{
    //����ģʽ
    private Player() { }
    private static Player instance;//ȫ��Ψһʵ��
    public static Player Instance //��ȡʵ��������
    {
        get
        {
            if (instance == null)
                instance = new Player();
            return instance;
        }
    }

    public string name;
    public int maxHP;
    public int currentHP;
    public int mithrils;//����
    public int tears;//���
    public List<int> cardSet  { get; private set; } //����
    //0~199�����  200~399������
    public bool[] unlock { get; private set; } //��¼�ѽ����Ŀ���

    /// <summary>
    ///  ��ʼ����Ϣ, ���봢����Ϣ��
    /// </summary>
    /// <returns></returns>
    public void Initialized(PlayerJSONInformation _info)
	{
        name = _info.Name;
        maxHP = _info.MaxHP;
        currentHP = _info.CurrentHP;
        tears = _info.Tears;
        mithrils = _info.Mithrils;

        cardSet = new List<int>(_info.CardSet);

        unlock = new bool[401];
        Array.Fill(unlock, false);
        foreach(var id in _info.UnlockCard)
		{
            unlock[id] = true;
		}
	}
    
    
    //ս��ϵͳ���õ���������
	public PlayerBattleInformation GetBattleInf()
	{
        PlayerBattleInformation info = new PlayerBattleInformation();
        info.maxHP = maxHP;
        info.currentHP = currentHP;
        info.cardSet = cardSet;
        return info;
	}
    public void SetBattleInf(PlayerBattleInformation _info)
	{
        currentHP = _info.currentHP;
	}
    public void SetBattleInf(int _currentHP)
	{
        currentHP = _currentHP;
	}

    //�������������޸ķ���
    public void SetData(string _name, int _maxHP, int _currentHp, int _mithrils, int _tears)
    {
        name = _name;
        maxHP = _maxHP;
        currentHP = _currentHp;
        mithrils = _mithrils;
        tears = _tears;
    }
    public void SetCurrentHP(int _value)
    {
        currentHP = _value;
    }
    public void SetMaxHP(int _value)
    {
        maxHP = _value;
    }
    public void AddCurrentHp(int _value)
    {
        currentHP += _value;
    }
    public void AddMaxHp(int _value)
    {
        maxHP += _value;
    }
    public void SetMoney(int _mithrils, int _tears)
    {
        mithrils = _mithrils;
        tears = _tears;
    }
    public void AddMoney(int _mithrils, int _tears)
    {
        mithrils += _mithrils;
        tears += _tears;
    }

    //������޸��Ƽ�ʹ�����������ַ���
    public void SetCard(List<int> _newCardSet)
    {
        //��ʱ�޷��޸�ʹ�ÿ�����������
        for (int i = 0; i < _newCardSet.Count; i++)
        {
            if (cardSet.Count > i)
                cardSet[i] = _newCardSet[i];
            else cardSet.Add(_newCardSet[i]);
        }
        CheckCard();
    }
    public void AddCard(int _newCard)
    {
        cardSet.Add(_newCard);
        CheckCard();
    }


    public void CheckCard() //��⿨�ƽ���
    {
        for(int i=0;i<cardSet.Count;i++)
        {
            if (unlock[cardSet[i]] == false)
                unlock[cardSet[i]] = true;
        }
    }
}

//����json����
