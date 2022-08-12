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

    public string Name { get; private set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public int Mithrils { get; private set; }//����
    public int Tears { get; private set; }//���
    public List<int> cardSet  { get; private set; } //����
    //0~199�����  200~399������
    public bool[] Unlocked { get; private set; } //��¼�ѽ����Ŀ���

    /// <summary>
    ///  ��ʼ����Ϣ, ����ArchiveManager�����봢����Ϣ��, ����ֱ�ӵ���
    /// </summary>
    /// <returns></returns>
    public void Initialized(PlayerJSONInformation _info)
	{
        Name = _info.Name;
        MaxHP = _info.MaxHP;
        CurrentHP = _info.CurrentHP;
        Tears = _info.Tears;
        Mithrils = _info.Mithrils;

        cardSet = new List<int>(_info.CardSet);

        Unlocked = new bool[401];
        Array.Fill(Unlocked, false);
        foreach(var id in _info.UnlockCard)
		{
            Unlocked[id] = true;
		}
	}

    #region �����޸Ľӿ�
    public void SetData(string _name, int _maxHP, int _currentHp, int _mithrils, int _tears)
    {
        Name = _name;
        MaxHP = _maxHP;
        CurrentHP = _currentHp;
        Mithrils = _mithrils;
        Tears = _tears;
    }
    public void SetCurrentHP(int _value)
    {
        CurrentHP = _value;
    }
    public void SetMaxHP(int _value)
    {
        MaxHP = _value;
    }
    public void AddCurrentHp(int _value)
    {
        if(CurrentHP + _value <= MaxHP)
		{
            CurrentHP += _value;
		}
        else
		{
            CurrentHP = MaxHP;
		}
    }
    public void AddMaxHp(int _value)
    {
        MaxHP += _value;
    }
    public void SetMoney(int _mithrils, int _tears)
    {
        Mithrils = _mithrils;
        Tears = _tears;
    }
    public void AddMoney(int _mithrils, int _tears)
    {
        Mithrils += _mithrils;
        Tears += _tears;
    }
	#endregion

	#region �����޸Ľӿ�
	/// <summary>
	/// ˢ�¿��ƽ������
	/// </summary>
	public void CheckUnlockedCard()
    {
        for (int i = 0; i < cardSet.Count; i++)
        {
            if (Unlocked[cardSet[i]] == false)
                Unlocked[cardSet[i]] = true;
        }
    }

    /// <summary>
    /// �����޸�, �÷����Ḳ��ԭ�еĿ���
    /// </summary>
    /// <param name="_list">�޸ĺ�Ŀ���</param>
    public void SetCardSet(List<int> _list)
	{
        cardSet.Clear();
        cardSet = _list;
        CheckUnlockedCard();
	}

    /// <summary>
    /// �����Լ������б��޸ģ��÷����Ḳ��ԭ�еĿ���ͽ����б�
    /// </summary>
    /// <param name="_list">�޸ĺ�Ŀ���</param>
    /// <param name="_unlock">�޸ĺ�Ľ����б�</param>
    public void SetCardSet(List<int> _list, bool[] _unlock)
    {
        cardSet.Clear();
        cardSet = _list;
        Unlocked = _unlock;
        CheckUnlockedCard();
    }

    /// <summary>
    /// �����ſ���
    /// </summary>
    /// <param name="_cradID">������Ŀ���ID</param>
    public void AddCard(int _cradID)
	{
        cardSet.Add(_cradID);
        CheckUnlockedCard();
	}

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="cardList">������Ŀ���ID�б�</param>
    public void AddCard(List<int> cardList)
	{
        foreach(var card in cardList)
		{
            cardSet.Add(card);
		}
        CheckUnlockedCard();
	}

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="cardList">������Ŀ���ID�б�</param>
    public void AddCard(int[] cardList)
    {
        foreach (var card in cardList)
        {
            cardSet.Add(card);
        }
        CheckUnlockedCard();
    }

    /// <summary>
    /// ɾ�����ſ���
    /// </summary>
    /// <param name="_cardID">��ɾ������ID</param>
    public void DeleteCard(int _cardID)
	{
        cardSet.Remove(_cardID);
	}

	#endregion

}

