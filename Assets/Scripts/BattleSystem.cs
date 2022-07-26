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

	List<int> deck;  //�ƶ�
	List<GameObject> handCards;  //���ƶ�  //max count = 10
	//List<int> usedCards;  //���ƶ�
	int usedFlag;
	List<GameObject> surventUnits; //�������б�

	[Header("��ҵ�λ")]
	public GameObject playerUnit;
	PlayerUnitManager playerUnitDisplay;

	//Boss��Ϣ��
	BossInBattle boss;
	//int actionCycleFlg = 0;
	List<GameObject> enemyUnits;  //Boss����б�  //max count = 7
	
	[Header("Boss��λ")]
	public GameObject bossUnit;
	BossUnitManager bossUnitDisplay;

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
	}
	private void Awake()
	{
		//��ʼ��ʵ��
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };

		handCards = new List<GameObject>(10);
		//usedCards = new List<int>();
		usedFlag = 0;
		surventUnits = new List<GameObject>(7);
		enemyUnits = new List<GameObject>(7);
		playerUnitDisplay = playerUnit.GetComponent<PlayerUnitManager>();
		bossUnitDisplay = bossUnit.GetComponent<BossUnitManager>();

		//
		TestSetData();

		//
	}

	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (usedFlag == deck.Count) //�ƿ�գ�����ϴ���Լ���ճͷ�
			{
				RefreshDeck();
				//TODO ϴ�Ƴͷ� δʵ��
			}
			int randPos = Random.Range(0, deck.Count); //�������

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //����Ԥ�Ƽ�ʵ��

			//Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			//���ݱ�ţ����ļ��ж�ȡ��������
			//Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));
			Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[usedFlag])));
			usedFlag++;

			newCard.GetComponent<CardDisplay>().card = card;
			newCard.GetComponent<CardDisplay>().LoadInf();


			handCards.Add(newCard);
			//newCard.GetComponent<CardDisplay>().LoadInf();

			//deck.RemoveAt(randPos);
		}
	}
	void RefreshDeck()  //ϴ��ˢ���ƶ�
	{
		Debug.Log("ˢ���ƶ�");
		usedFlag = 0;
		for(int i = 0; i < deck.Count; i++)
		{
			var rnd = Random.Range(0, deck.Count);
			var swap = deck[rnd];
			deck[rnd] = deck[i];
			deck[i] = swap;
		}
		
		/* //��ˢ�·�����������
		for (int i = 0; i < usedCards.Count; i++)
		{
			deck.Add(usedCards[i]);
		}
		usedCards.Clear();
		*/
	}
	void EndRound()
	{
		//roundEndFlag = true;
		playerActionCompleted = true;
	}
	void GamePlay()
	{
		// ����
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
		//��Ҳ������/ʹ�÷�����
		// �������ж�
		/*
		if(player.CurrentActionPoint == 0) //ս��������ʱ�Զ���������ж� (ʵ�������ѣ��ݶ��ϳ�)
		{
			Debug.Log("����ж�");
			//
		}
		*/
		if (playerActionCompleted)  //����ж���ɺ�����ж�,�������غ�
		{
			BossAction();       // Boss��������ж�
			
			round++;
			roundText.text = round.ToString();

			if(player.MaxActionPoint < 10)
			{
				player.MaxActionPoint++; //ÿ����һ�غ�,ս��������1��,���Ϊ10
			}
			player.CurrentActionPoint = player.MaxActionPoint;
			
			playerActionCompleted = false;
			getCardCompleted = false;

			//��鲢ˢ��buff
			bossUnitDisplay.CheckBuff();
			foreach (var obj in surventUnits)
			{
				obj.GetComponent<SurventUnitManager>().CheckBuff();
			}
			foreach (var obj in enemyUnits)
			{
				obj.GetComponent<SurventUnitManager>().CheckBuff();
			}
			//�����غ�
		}
		
	}
	public void UseCard(GameObject _cardObject)  //ʹ�ÿ���
	{
		Card _card = _cardObject.GetComponent<CardDisplay>().card;
		if(player.CurrentActionPoint - _card.cost >= 0)
		{
			if (_card.cardType == CardType.Spell)
			{
				SpellTrigger(_card);
			}
			else
			{
				if ((_card.cardType == CardType.Survent && surventUnits.Count < 7)
					|| (_card.cardType == CardType.Monster && enemyUnits.Count < 7))
				{
					SurventSetup(_card);
				}
				else return;
			}
			player.CurrentActionPoint -= _card.cost;
			handCards.Remove(_cardObject);
			//usedCards.Add(_card.cardID);
			Destroy(_cardObject);
		}
		else 
		{
			Debug.Log("սʿ�㲻��");
		}
	}
	void SpellTrigger(Card _card)
	{
		Debug.Log("ʹ�÷�����");
		//
	}
	void SurventSetup(Card _card)
	{
		Debug.Log("ʹ����ӿ�");
		if (_card.cardType == CardType.Monster)
		{
			if(enemyUnits.Count < 7)
			{
				GameObject newEnemy = GameObject.Instantiate(surventPrefab, enemyArea.transform);
				newEnemy.GetComponent<SurventUnitManager>().Initial(_card);
				enemyUnits.Add(newEnemy);

			}
		}
		else
		{
			if(surventUnits.Count < 7)
			{
				GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
				newSurvent.GetComponent<SurventUnitManager>().Initial(_card);
				surventUnits.Add(newSurvent);

			}
		}
	}
	void BossAction()  //TODO Boss�ж�
	{
		//int flag = round % boss.actionCycle.Count;
		//BossActionType action = boss.actionCycle[flag];
		//TODO boss�ж�
		Debug.Log("Boss�ж�");
	}
	//�����Ϣ���뷽��
	public void SetBossInf(BossInBattle _boss)
	{
		boss = _boss;
	}
	public void SetPlayerInf(PlayerBattleInformation _info)
	{
		player.MaxHP = _info.maxHP;
		player.CurrentHP = _info.currentHP;
		deck = _info.cardSet;
	}
	void TestSetData() //������������
	{
		Debug.Log("Start Data Setting...");
		player = new PlayerInBattle(20, 10, 1, 1);
		playerUnitDisplay.player = player;

		boss = new BossInBattle(Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(1)));
		bossUnitDisplay.boss = boss;

		Debug.Log("���������������");
	}

}

