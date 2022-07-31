using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player
{
    
    public int name;
    public int maxHP;
    public int currentHP;
    public int mithrils; //����
    public int tears;  //���

    List<int> cardSet; //����
    //0~199  200~299
    bool[] unlock; //��¼�ѽ����Ŀ���

	private void Start()
	{
        cardSet = new List<int>();
        unlock = new bool[400];
    }

    public void LoadData()
	{
        
	}

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

}
