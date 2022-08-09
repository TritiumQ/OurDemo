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
	//bool getCardCompleted = false;
	bool roundStart = false;
	int round;

	//����
	

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
	public GameObject enemyPrefab;
	[Header("����Ԥ����")]
	public GameObject cardPrefab;  
	private void Update()
	{
		GamePlay();
	}
	private void Awake()
	{
		vectory.SetActive(false);
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

				//ϴ�Ƴͷ�,�����5��Ѫ
				Effect.Set(playerUnit, CardActionType.Attack, 5);
			}
			int randPos = Random.Range(0, deck.Count); //�������

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //����Ԥ�Ƽ�ʵ��

			//Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			//���ݱ�ţ����ļ��ж�ȡ��������
			//Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));
			CardSOAsset card = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[usedFlag]));
			usedFlag++;

			newCard.GetComponent<CardDisplay>().Initialized(card);
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
		//1 ��ҳ���,�غϿ�ʼ,��������ж�״̬,��������Ȼ�Ч��
		if (roundStart == false)
		{
			if(round == 0)
			{
				GetCard(3);
			}
			else
			{
				GetCard(1);
			}
			Debug.Log("�غϿ�ʼ");
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
		//2 ��Ҳ������/ʹ�÷�����
		//3 ����ж���ɺ�����ж�,�������غ�
		else  if (playerActionCompleted)  
		{
			// Boss�ж�
			bossUnitManager.Action(round);
			//boss����ж�
			foreach(var obj in BossSurventUnits)
			{

				obj.SendMessage("Action");
			}
			round++;
			roundText.text = round.ToString();

			if(player.MaxActionPoint < 10)
			{
				player.MaxActionPoint++; //ÿ����һ�غ�,ս��������1��,���Ϊ10
			}
			player.CurrentActionPoint = player.MaxActionPoint;
			
			playerActionCompleted = false;
			roundStart = false;
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
		CardSOAsset _card = _cardObject.GetComponent<CardDisplay>().card;
		if(player.CurrentActionPoint - _card.Cost >= 0)
		{
			if (_card.CardType == CardType.Spell)
			{
				//ʹ�÷�����
				//AttackRequest(_cardObject, EffectType.SpellAttack, _cardObject.transform.position);
			}
			else
			{
				if ((_card.CardType == CardType.Survent && PlayerSurventUnits.Count < 7))
				{
					GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
					newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
					PlayerSurventUnits.Add(newSurvent);
					//��������Ч��
					newSurvent.GetComponent<SurventUnitManager>().SetupEffect();

					player.CurrentActionPoint -= _card.Cost;
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
	public void SetupBossSurvent(CardSOAsset _card)
	{
		if(_card.CardType == CardType.Monster && BossSurventUnits.Count < 7)
		{
			GameObject newEnemy = Instantiate(enemyPrefab, enemyArea.transform);
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
	//һ�׹�������
	public GameObject attacker { get; private set; }
	public CardType attackerType { get; private set; }
	public TargetOptions attackTarget { get; private set; }
	public CardActionType actionType { get; private set; }
	public GameObject victim { get; private set; }

	public GameObject arrowPrefab;
	public GameObject arrow; //������ͷ
	public Transform canvas;
	//1 ��������
	public void AttackRequest(GameObject _request, CardType _atkerType , TargetOptions _target,CardActionType _action , Vector2 _startPoint)
	{
		if(arrow == null)
		{
			Debug.Log("��������");
			arrow = GameObject.Instantiate(arrowPrefab, canvas);
			arrow.GetComponent<TestArrow>().SetStartPoint(_startPoint);
			attacker = _request;
			attackerType = _atkerType;
			attackTarget = _target;
			actionType = _action;
		}
	}
	//2 Ŀ��ȷ���ܻ�
	public void AttackConfirm(GameObject _confirm)
	{
		if(attacker != null)
		{
			victim = _confirm;
			//����Ƿ��󹥻�
			if(attackTarget == TargetOptions.SinglePlayerTarget)
			{

			}
			else if(attackTarget == TargetOptions.SingleEnemyTarget)
			{

			}
			else
			{
				AttackOver();
			}
			//TODO ���з��Ƿ��г�����������
			if (attackerType == CardType.Survent)
			{	

				//Debug.Log("�����ɹ�");
				attacker.GetComponent<SurventUnitManager>().isActive = false;
				Effect.Set(victim, CardActionType.Attack, attacker.GetComponent<SurventUnitManager>().survent.atk);
			}
			else if(attackerType == CardType.Spell)
			{
				CardSOAsset spellCard = attacker.GetComponent<CardDisplay>().card;
				Effect.Set(victim, spellCard.SpellActionType, spellCard.SpellActionValue1, spellCard.SpellActionValue2);
				player.CurrentActionPoint -= spellCard.Cost;
				handCards.Remove(attacker);
				Destroy(attacker);
			}

			AttackOver();
		}
	}
	//��������
	public void AttackOver()
	{
		Debug.Log("Cancel");
		if(arrow != null)
		{
			Destroy(arrow);
		}
		attacker = null;
		victim = null;
	}
	//ʤ��
	public GameObject vectory;
	public void Victory()
	{
		Debug.Log("��Ү~��");
		vectory.SetActive(true);
		//TODO ʤ�������
		PlayerDataTF.EventContinue();
		SceneManager.LoadScene("GameProcess");

	}
	//�����Ϣ���뷽��
	public void SetBossInf(int _bossID)
	{
		boss = new BossInBattle(Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(_bossID)));
	}
	public void SetPlayerInf(PlayerBattleInformation _info)
	{
		player = new PlayerInBattle(_info);
		//player.MaxHP = _info.maxHP;
		//player.CurrentHP = _info.currentHP;
		//deck = _info.cardSet;
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

