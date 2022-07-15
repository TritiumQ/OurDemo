using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
    int round;
	Button endButton;

	public GameObject dataManager; //��Ϸ���ݹ�����

	List<int> deck;  //�ƶ�
	List<int> hands;  //����
	List<Card> handCards;
	List<int> usedCards;  //���ƶ�

	List<SurventUnit> surventUnits;  //������ʵ��
	List<SurventUnit> enemyUnits;  //�������ʵ��

	public GameObject surventPrefab;
	public GameObject cardPrefab;  

	public GameObject playerHands; //�����������
	public GameObject enemyArea;  //�з��������
	public GameObject surventArea;  //����������

	public GameObject playerBody;
	public GameObject bossBody;

	public TextMeshProUGUI roundText;

	private void Awake()
	{
		endButton = GameObject.Find("EndButton").GetComponent<Button>();


		roundText.text = round.ToString();
		
		deck = new List<int>();
		hands = new List<int>();
		handCards = new List<Card>();
		usedCards = new List<int>();

		surventUnits = new List<SurventUnit>();
		enemyUnits = new List<SurventUnit>();

		//TestLoadData();
		GamePlay();
	}
	void GamePlay()
	{
		for(; ; )
		{
			RoundPlay();
			round++;
			roundText.text = round.ToString();
		}
	}

	void GetCard()
	{
		if(deck.Count == 0) //�ƿ�գ�����ϴ���Լ���ճͷ�
		{
			RefreshDeck();
			//punish δʵ��
		}
		int rand = Random.Range(0, deck.Count);
		hands.Add(deck[rand]);
		deck.RemoveAt(rand);
	}
	void ShowHandCard() // չʾ����ĵ���
	{
		GameObject newCard = Instantiate(cardPrefab,playerHands.transform);

	}
	void RefreshDeck()  //ˢ���ƶ�
	{
		for(int i = 0; i < usedCards.Count; i++)
		{
			deck.Add(usedCards[i]);
		}
		usedCards.Clear();
	}

	void RoundPlay() //���غ�����
	{
		//1 ��Ҵ��ƶѳ���
		if(round == 0) //���ֳ����ſ�
		{
			for(int i = 0; i < 3; i++)
			{
				GetCard();
				
			}
		}
		else //֮��ÿ�غϿ�ʼ��һ�ſ�
		{
			GetCard();
		}
		//2 ��Ҳ������/ʹ�÷�����

		//3 �������ж�

		//4 Boss�ж�

		//5 Boss����ж�

		//�����غ�


	}
	void TestLoadData()
	{
		Debug.Log("Start Data Test...");

		//dataManager.GetComponent<PlayerDataManager>().player;
	}

}

