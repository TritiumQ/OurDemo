using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player
{
    public int name;
    public int maxHP;
    public int curentHP;
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
}
