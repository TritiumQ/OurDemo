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
	public List<GameObject> PlayerSurventUnits; //�������б�

	[Header("��ҵ�λ")]
	public GameObject playerUnit;
	public PlayerUnitManager playerUnitDisplay;

	//Boss��Ϣ��
	BossInBattle boss;
	//int actionCycleFlg = 0;
	public List<GameObject> BossSurventUnits;  //Boss����б�  //max count = 7
	
	[Header("Boss��λ")]
	public GameObject bossUnit;
	public BossUnitManager bossUnitManager;

	//�ؼ�
	//bool roundEndFlag = false;  //�غϽ�����־
	bool playerActionCompleted = false;
	bool getCardCompleted = false;
	bool roundStart;
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
		PlayerSurventUnits = new List<GameObject>(7);
		BossSurventUnits = new List<GameObject>(7);
		playerUnitDisplay = playerUnit.GetComponent<PlayerUnitManager>();
		bossUnitManager = bossUnit.GetComponent<BossUnitManager>();

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
		//1 ��ҳ���
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
		//2 �غϿ�ʼ,��������ж�״̬,��������Ȼ�Ч��
		else if(roundStart == false)
		{
			foreach (var obj in PlayerSurventUnits)
			{
				obj.SendMessage("CheckInStart");
			}
			foreach (var obj in BossSurventUnits)
			{
				obj.SendMessage("CheckInStart");
			}
			roundStart = true;
		}
		//3 ��Ҳ������/ʹ�÷�����
		//4 ����ж���ɺ�����ж�,�������غ�
		else  if (playerActionCompleted)  
		{
			bossUnitManager.Action(round);// Boss�ж�
			//boss����ж�
			round++;
			roundText.text = round.ToString();

			if(player.MaxActionPoint < 10)
			{
				player.MaxActionPoint++; //ÿ����һ�غ�,ս��������1��,���Ϊ10
			}
			player.CurrentActionPoint = player.MaxActionPoint;
			
			playerActionCompleted = false;
			getCardCompleted = false;
			roundStart = true;
			//��鲢ˢ��buff,�Լ�������Ӻ���Ч��
			bossUnitManager.CheckBuff();
			foreach (var obj in PlayerSurventUnits)
			{
				obj.SendMessage("CheckInEnd");
			}
			foreach (var obj in BossSurventUnits)
			{
				obj.SendMessage("CheckInEnd");
			}
			//�����غ�
		}
		
	}
	public void UseCardByPlayer(GameObject _cardObject)  //ʹ�ÿ���
	{
		Card _card = _cardObject.GetComponent<CardDisplay>().card;
		if(player.CurrentActionPoint - _card.cost >= 0)
		{
			if (_card.cardType == CardType.Spell)
			{
				//ʹ�÷�����
			}
			else
			{
				if ((_card.cardType == CardType.Survent && PlayerSurventUnits.Count < 7))
				{
					GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
					newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
					PlayerSurventUnits.Add(newSurvent);

					player.CurrentActionPoint -= _card.cost;
					handCards.Remove(_cardObject);
					//usedCards.Add(_card.cardID);
					Destroy(_cardObject);
				}
			}
		}
		else 
		{
			Debug.Log("ս���㲻��");
		}
	}
	public void SetupSurventByBoss(Card _card)
	{
		if(_card.cardType == CardType.Monster && BossSurventUnits.Count < 7)
		{
			GameObject newEnemy = Instantiate(surventPrefab, enemyArea.transform);
			newEnemy.GetComponent<SurventUnitManager>().Initialized(_card);
			BossSurventUnits.Add(newEnemy);
		}
	}
	//
	public void PlayerSurventDie(GameObject _obj)
	{
		PlayerSurventUnits.Remove(_obj);
	}
	public void BossSurventDie(GameObject _obj)
	{
		BossSurventUnits.Remove(_obj);
	}
	//һ����ӹ�������
	GameObject attacker;
	GameObject victim;
	//1 ��ӷ��𹥻�����
	public void AttackRequest(GameObject _request)
	{
		attacker = _request;
	}
	//2 Ŀ��ȷ���ܻ�
	public void AttackConfirm(GameObject _confirm)
	{
		if(attacker != null)
		{
			victim = _confirm;
			int damage = attacker.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.ATK);
			victim.SendMessage("BeAttacked", damage);
			attacker.GetComponent<SurventUnitManager>().isActive = false;
			victim = null;
			attacker = null;
		}
	}
	//2 ��ȡ������
	public void AttackCancel()
	{
		attacker = null;
		victim = null;
	}
	//
	public void Vectory()
	{
		Debug.Log("��Ү~��");
		//
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
		bossUnitManager.boss = boss;

		Debug.Log("���������������");
	}

}

