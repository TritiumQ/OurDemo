using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player
{
    public int name;
    public int maxHP;
    public int currentHP;
    public int mithrils; //����
    public int tears;  //���

    List<int> cardSet; //����

    //int[] unlock; ��¼�ѽ����Ŀ���

	private void Start()
	{
        cardSet = new List<int>();
        //unlock = new int[1000];
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
