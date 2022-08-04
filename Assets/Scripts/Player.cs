using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player
{
    //����ģʽ
    private Player() 
    {
        cardSet = new List<int>();
        unlock = new bool[400];
        //LoadData(); //��ȱ�����������᲻����bug
    }
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
    public int maxHP=1;
    public int currentHP=1;
    public int mithrils; //����
    public int tears;  //���

    public List<int> cardSet  { get; private set; } //����
    //0~199  200~299
    bool[] unlock=new bool[400]; //��¼�ѽ����Ŀ���

    public void LoadData()
	{
        
	} //�����ݴ洢�ļ���������

    //ս��ϵͳ���õ�����������û������
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
