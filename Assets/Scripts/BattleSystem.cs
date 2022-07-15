using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    int round;
	Button endButton;


	List<int> deck;  //�ƶ�
	List<int> hands;  //����
	List<int> usedCards;  //���ƶ�


	public GameObject surventPrefab;
	public GameObject cardPrefab;  

	public GameObject playerHands; //�����������
	public GameObject enemyArea;  //�з��������
	public GameObject surventArea;  //����������

	public GameObject playerBody;
	public GameObject bossBody;

	private void Awake()
	{
		endButton = GameObject.Find("EndButton").GetComponent<Button>();
		endButton.onClick.AddListener(EndRound);
		
	}
	void EndRound() // ������ǰ�غ�
	{
		Debug.Log("Click EndRound");

	}

}
