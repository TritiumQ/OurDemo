using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{

	//�����Ϣ��
	PlayerInBattle player;
	int playerMaxHP;
	int playerCurrentHP;
	int playerCurrentActionPoint;
	int playerMaxActionPoint;
	List<int> deck;  //�ƶ�
	List<GameObject> handCards;  //����  //max count = 10
	List<int> usedCards;  //���ƶ�
	List<GameObject> surventUnits; //������

	[Header("��ҵ�λ")]
	public GameObject playerUnit;
	PlayerUnitDisplay playerUnitDisplay;
	//Boss��Ϣ��
	BossInBattle boss;
	//int actionCycleFlg = 0;
	List<GameObject> enemyUnits;  //Boss���  //max count = 7
	
	[Header("Boss��λ")]
	public GameObject bossUnit;
	BossUnitDisplay bossUnitDisplay;

	//�ؼ�
	//bool roundEndFlag = false;  //�غϽ�����־
	bool playerActionCompleted = false;
	bool getCardCompleted = false;
	int round;

	[Header("�غ���")]
	public TextMeshProUGUI roundText;
	[Header("�����غϰ�ť")]
	public Button endButton;

	[Header("���������")]
	public GameObject playerHands;
	[Header("����������")]
	public GameObject surventArea;
	[Header("�з������")]
	public GameObject enemyArea;
	[Header("���Ԥ����")]
	public GameObject surventPrefab;
	[Header("����Ԥ����")]
	public GameObject cardPrefab;  
	private void Update()
	{
		GamePlay();
		Refresh();
	}
	private void Awake()
	{
		//��ʼ��ʵ��
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };
		handCards = new List<GameObject>(10);
		usedCards = new List<int>();
		surventUnits = new List<GameObject>(7);
		enemyUnits = new List<GameObject>(7);
		playerUnitDisplay = playerUnit.GetComponent<PlayerUnitDisplay>();
		bossUnitDisplay = bossUnit.GetComponent<BossUnitDisplay>();
		//
		TestSetData();

		//��ʼ����ʾ
		Refresh();
	}

	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (deck.Count == 0) //�ƿ�գ�����ϴ���Լ���ճͷ�
			{
				RefreshDeck();
				//TODO ϴ�Ƴͷ� δʵ��
			}
			int randPos = Random.Range(0, deck.Count);

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //����Ԥ�Ƽ�ʵ��

			Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));  //���ݱ�ţ����ļ��ж�ȡ��������

			newCard.GetComponent<CardDisplay>().card = card;
			newCard.GetComponent<CardDisplay>().LoadInf();


			handCards.Add(newCard);
			//newCard.GetComponent<CardDisplay>().LoadInf();

			deck.RemoveAt(randPos);
		}
	}
	void RefreshDeck()  //ˢ���ƶ�
	{
		Debug.Log("ˢ���ƶ�");
		for (int i = 0; i < usedCards.Count; i++)
		{
			deck.Add(usedCards[i]);
		}
		usedCards.Clear();
	}
	void EndRound()
	{
		//roundEndFlag = true;
		playerActionCompleted = true;
	}
	void GamePlay()
	{
		//1 ����
		if (getCardCompleted == false)
		{
			if (round == 0)
			{
				GetCard(3);
			}
			else
			{
				GetCard(1);
			}
			getCardCompleted = true;
		}
		//2 ��Ҳ������/ʹ�÷�����
		//3 �������ж�
		if (playerActionCompleted)  //����ж���ɺ�����ж�,�������غ�
		{
			BossAction();       //4 Boss��������ж�
			
			round++;
			roundText.text = round.ToString();

			if(playerMaxActionPoint < 10)
			{
				playerMaxActionPoint++; //ÿ����һ�غ�,ս��������1��,���Ϊ10
			}
			playerCurrentActionPoint = playerMaxActionPoint;
			
			playerActionCompleted = false;
			getCardCompleted = false;
		}
		else
		{
			//�������ж�


		}
		//�����غ�
	}
	void BossAction()  //Boss�ж�
	{
		//int flag = round % boss.actionCycle.Count;
		//BossActionType action = boss.actionCycle[flag];
		//TODO boss�ж�
		Debug.Log("Boss�ж�");
	}
	void Refresh()
	{
		playerUnitDisplay.Refresh(playerCurrentActionPoint,playerCurrentHP);
	}
	//�����Ϣ���뷽��
	public void GetBossInf(BossInBattle _boss)
	{
		boss = _boss;
	}
	public void GetPlayerInf(PlayerBattleInformation _info)
	{
		playerMaxHP = _info.maxHP;
		playerCurrentHP = _info.currentHP;
		deck = _info.cardSet;
	}
	void TestSetData()
	{
		Debug.Log("Start Data Setting...");
		playerMaxHP = 10;
		playerCurrentHP = 10;
		playerCurrentActionPoint = playerMaxActionPoint = 1;
		playerUnitDisplay.Refresh(playerCurrentActionPoint, playerCurrentHP);

		BossInBattle bs = new BossInBattle(Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(1)));
		bossUnitDisplay.boss = bs;
	}

}

