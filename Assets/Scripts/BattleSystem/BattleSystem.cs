using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
	[Header("��ҵ�λ")]
	public GameObject playerUnit;
	//�����Ϣ��
	PlayerInBattle player;
	List<int> deck;  //�ƶ�
	List<GameObject> handCards;  //���ƶ�  //max count = 10
	//List<int> usedCards;  //���ƶ�
	int cardUsedFlag;
	public List<GameObject> PlayerSurventUnitsList { get; private set; } //�������б�


	[Header("Boss��λ")]
	public GameObject bossUnit;
	public BossUnitManager bossUnitManager;
	public List<GameObject> BossSurventUnitsList { get; private set; } //Boss����б�  //max count = 7

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
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);

		//
		TestSetData();

		//
	}
	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (cardUsedFlag == deck.Count) //�ƿ�գ�����ϴ���Լ���ճͷ�
			{
				RefreshDeck();

				//ϴ�Ƴͷ�,�����5��Ѫ
				Effect.Set(playerUnit, EffectType.Attack, 5);
			}
			int randPos = Random.Range(0, deck.Count); //�������

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //����Ԥ�Ƽ�ʵ��

			//Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			//���ݱ�ţ����ļ��ж�ȡ��������
			//Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));
			CardSOAsset card = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[cardUsedFlag]));
			cardUsedFlag++;

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
		cardUsedFlag = 0;
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
			foreach (var obj in PlayerSurventUnitsList)
			{
				obj.SendMessage("CheckInStart");
			}
			foreach (var obj in BossSurventUnitsList)
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
			

			//boss����ж�
			foreach(var obj in BossSurventUnitsList)
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
			//bossUnitManager.CheckBuff();
			foreach (var obj in PlayerSurventUnitsList)
			{
				obj.SendMessage("CheckInEnd");
			}
			foreach (var obj in BossSurventUnitsList)
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
				if ((_card.CardType == CardType.Survent && PlayerSurventUnitsList.Count < 7))
				{
					GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
					newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
					PlayerSurventUnitsList.Add(newSurvent);
					//��������Ч��
					

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
		if(_card.CardType == CardType.Monster && BossSurventUnitsList.Count < 7)
		{
			GameObject newEnemy = Instantiate(enemyPrefab, enemyArea.transform);
			newEnemy.GetComponent<SurventUnitManager>().Initialized(_card);
			BossSurventUnitsList.Add(newEnemy);
		}
	}

	public void SurventUnitDie(GameObject unitObject)
	{
		if(PlayerSurventUnitsList.Contains(unitObject))
		{
			PlayerSurventUnitsList.Remove(unitObject);
		}
		else if(BossSurventUnitsList.Contains(unitObject))
		{
			BossSurventUnitsList.Remove(unitObject);
		}
	}

	#region �°�-Ч�����ͷźͽ��յ��Ⱥ���
	public GameObject EffectInitiator { get; private set; }
	public GameObject EffectTarget { get; private set; }
	public EffectPackage Package { get; private set; }
	public void ApplyEffectTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		if (_target != null && _initiator != null && _effect != null)
		{
			object[] ParameterList = { _initiator, _effect };
			_target.SendMessage("AcceptEffect", ParameterList);
		}
	}
	public void EffectSetupRequest(GameObject initiator, EffectPackage package)
	{
		EffectInitiator = initiator;
		Package = package;
	}
	public void EffectConfirm(GameObject target)
	{
		EffectTarget = target;
		if (EffectInitiator != EffectTarget && EffectInitiator != null && EffectTarget != null)
		{
			ApplyEffectTo(EffectTarget, EffectInitiator, Package);
		}
		EffectSetupOver();
	}
	public void EffectSetupOver()
	{
		EffectInitiator = null;
		EffectTarget = null;
		Package = null;
	}
	/// <summary>
	/// ���ض�����Ŀ��ֱ���ͷ�Ч��
	/// </summary>
	/// <param name="initiator">Ч��������</param>
	/// <param name="Target">Ч��Ŀ��</param>
	/// <param name="package">Ч����Ϣ</param>
	public void EffectDirectSetup(GameObject initiator, GameObject Target, EffectPackage package)
	{
		//TODO 

	}
	/// <summary>
	/// ֱ���ͷ�Ч����Ŀ����Ϣ��Ч����ȷ��
	/// </summary>
	/// <param name="initiator">Ч��������</param>
	/// <param name="package">Ч����Ϣ������Ŀ����Ϣ��</param>
	public void EffectDirectSetup(GameObject initiator, EffectPackageWithTargetOption package)
	{
		//TODO

	}
	#endregion

	#region �ɰ�-Ч�����ͷźͽ��յ��Ⱥ���
	//һ�׹�������
	public GameObject attacker { get; private set; }
	public CardType attackerType { get; private set; }
	public TargetOptions attackTarget { get; private set; }
	public EffectType actionType { get; private set; }
	public EffectPackage effect { get; private set; }
	public GameObject victim { get; private set; }

	public GameObject arrowPrefab;
	public GameObject arrow; //������ͷ
	public Transform canvas;
	//1 ��������
	public void AttackRequest(GameObject _request, CardType _atkerType , TargetOptions _target,EffectType _action , Vector2 _startPoint)
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
				Effect.Set(victim, EffectType.Attack, attacker.GetComponent<SurventUnitManager>().survent.ATK);
			}
			else if(attackerType == CardType.Spell)
			{
				CardSOAsset spellCard = attacker.GetComponent<CardDisplay>().card;
				//Effect.Set(victim, spellCard.SpellActionType, spellCard.SpellActionValue1, spellCard.SpellActionValue2);
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
	#endregion

	//ʤ��
	public GameObject vectory;

	public void GameEnd(GameResult result)
	{
		switch (result)
		{
			case GameResult.Success:
				{
					Debug.Log("��Ϸʤ��");
				}
				break;
			case GameResult.Failure:
				{
					Debug.Log("��Ϸʧ��");
				}
				break;
			case GameResult.Escape:
				{
					Debug.Log("��������");
				}
				break;
			default:
				break;
		}
	}

	//�����Ϣ���뷽��
	public void LoadBossInformation(int _bossID)
	{
		if(bossUnit != null)
		{
			bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(_bossID)));
		}
	}
	public void LoadPlayerInformation()
	{
		if(playerUnit != null)
		{
			playerUnit.SendMessage("Initialized");
		}
	}
	
	void TestSetData() //������������
	{
		Debug.Log("Start Data Setting...");
		ArchiveManager.LoadPlayerData(1);

		playerUnit.SendMessage("Initialized");

		bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(1)));

		Debug.Log("���������������");
	}

}

