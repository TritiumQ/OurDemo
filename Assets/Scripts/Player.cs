using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int maxHP;
    int curentHP;
    int mithrils; //����
    int tears;  //���

    List<int> cardSet;
    int[] unlock;

	private void Start()
	{
        cardSet = new List<int>();
        unlock = new int[1000];
    }

}
