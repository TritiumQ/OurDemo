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
	List<int> deck;  //�ƶ�
	List<GameObject> handCards;  //���ƶ�  //max count = 10
	//List<int> usedCards;  //���ƶ�
	int cardUsedFlag;
	public List<GameObject> PlayerSurventUnitsList { get; private set; } //�������б�

	[Header("Boss��λ")]
	public GameObject bossUnit;
	public List<GameObject> BossSurventUnitsList { get; private set; } //Boss����б�  //max count = 7

	//�ؼ�
	//�غϽ�����־
	bool playerActionCompleted = false;
	//�غϿ�ʼ��־
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
	[Header("����Ԥ����")]
	public GameObject cardPrefab;  
	private void Update()
	{
		GamePlay();
	}
	private void Awake()
	{
		victory.SetActive(false);
		//��ʼ��ʵ��
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 0,200 };

		handCards = new List<GameObject>(10);
		//usedCards = new List<int>();
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);

		//
		TestSetData();
	}
	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (cardUsedFlag == deck.Count) //�ƿ�գ�����ϴ���Լ���ճͷ�
			{
				RefreshDeck();
				Debug.Log("ϴ�Ƴͷ�");
				//TODO ϴ�Ƴͷ�,�����5��Ѫ
				//Effect.Set(playerUnit, EffectType.Attack, 5);
				

			}
			//����
			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //����Ԥ�Ƽ�ʵ��
			//���ݱ�ţ����ļ��ж�ȡ��������
			CardSOAsset card = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[cardUsedFlag]));
			cardUsedFlag++;

			newCard.GetComponent<CardDisplay>().Initialized(card);
			newCard.GetComponent<CardDisplay>().LoadInf();

			handCards.Add(newCard);
		}
	}
	void RefreshDeck()  //ϴ��ˢ���ƶ�
	{
		Debug.Log("ϴ��");
		cardUsedFlag = 0;
		for(int i = 0; i < deck.Count; i++)
		{
			var rnd = Random.Range(0, deck.Count);
			var swap = deck[rnd];
			deck[rnd] = deck[i];
			deck[i] = swap;
		}
	}
	void EndRound()
	{
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
			roundStart = true;
			//�����������Ȼ�Ч��
			foreach (var unit in PlayerSurventUnitsList)
			{
				unit.SendMessage("AdvancedEffectTrigger");
			}
		}
		//2 ��Ҳ������/ʹ�÷�����
		//3 ����ж���ɺ�����ж�,�������غ�
		else  if (playerActionCompleted)  
		{
			//���������Ӻ���Ч��
			foreach(var unit in PlayerSurventUnitsList)
			{
				unit.SendMessage("SubsequentEffectTrigger");
			}

			// Boss�Լ�������ӣ��Ȼ�Ч�����ж�
			bossUnit.SendMessage("AdvancedEffectTrigger");
			bossUnit.SendMessage("AutoAction", round);
			foreach (var unit in BossSurventUnitsList)
			{
				unit.SendMessage("AdvancedEffectTrigger");
			}
			foreach(var unit in BossSurventUnitsList)
			{
				unit.SendMessage("AutoAction", round);
			}

			//ˢ�»غ�
			round++;
			roundText.text = round.ToString();
			playerActionCompleted = false;
			roundStart = false;

			//��鲢ˢ��buff,�Լ�����boss�͵�����Ӻ���Ч��
			bossUnit.SendMessage("SubsequentEffectTrigger");
			foreach(var unit in BossSurventUnitsList)
			{
				unit.SendMessage("SubsequentEffectTrigger");
			}
			//�����غ�
		}
		
	}
	public void UseCardByPlayer(GameObject _cardObject)  //ʹ�ÿ���
	{
		if(_cardObject != null)
		{
			CardSOAsset card = _cardObject.GetComponent<CardDisplay>().Asset;
			if (card != null && playerUnit.GetComponent<PlayerUnitManager>().CanUseActionPoint(card.Cost))
			{
				if (card.CardType == CardType.Spell)
				{
					//TODO ʹ�÷�����
				}
				else if ((card.CardType == CardType.Survent && PlayerSurventUnitsList.Count < 7))
				{
					SetupSurvent(card, CardType.Survent);
					playerUnit.GetComponent<PlayerUnitManager>().UseActionPoint(card.Cost);
					handCards.Remove(_cardObject);
					Destroy(_cardObject);
				}
			}
			else
			{
				Debug.Log("ս���㲻��");
			}
		}
	}
	public void SetupSurvent(CardSOAsset _card, CardType _type)
	{
		if (_card != null) 
		{
			GameObject newSurvent = null;
			switch (_type)
			{
				case CardType.Survent:
					{
						newSurvent = Instantiate(surventPrefab, surventArea.transform);
						newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
						PlayerSurventUnitsList.Add(newSurvent);
					}
					break;
				case CardType.Monster:
					{
						if (BossSurventUnitsList.Count < 7)
						{
							GameObject newEnemy = Instantiate(surventPrefab, enemyArea.transform);
							newEnemy.GetComponent<SurventUnitManager>().Initialized(_card);
							BossSurventUnitsList.Add(newEnemy);
						}
					}
					break;
				default:
					break;
			}
			//��������Ч��
			newSurvent.SendMessage("SetupEffectTrigger");
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
	//TODO �°�Ч������
	public GameObject EffectInitiator { get; private set; }
	public GameObject EffectTarget { get; private set; }
	public EffectPackage Package { get; private set; }
	/// <summary>
	/// ���ض�����Ŀ��ֱ���ͷ�Ч��
	/// </summary>
	/// <param name="_initiator">Ч��������</param>
	/// <param name="_target">Ч��Ŀ��</param>
	/// <param name="_effect">Ч����Ϣ</param>
	public void ApplyEffectTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		if (_target != null && _initiator != null && _effect != null)
		{
			object[] ParameterList = { _initiator, _effect };
			_target.SendMessage("AcceptEffect", ParameterList);
		}
	}
	/// <summary>
	/// ֱ���ͷ�Ч��, Ŀ��ѡ��ʽ��Ч����ȷ��
	/// </summary>
	/// <param name="initiator">Ч��������</param>
	/// <param name="package">Ч����Ϣ��������Ŀ����Ϣ��</param>
	public void ApplyEffect(GameObject initiator, EffectPackageWithTargetOption package)
	{
		//TODO ����Ч��
		switch (package.EffectTarget)
		{
			case TargetOptions.AllCreatures:
				break;
			case TargetOptions.AllPlayerCreatures:
				break;
			case TargetOptions.AllEnemyCreatures:
				break;
			case TargetOptions.AllCharacters:
				break;
			case TargetOptions.ALlPlayerCharacter:
				break;
			case TargetOptions.ALlEnemyCharacters:
				break;
			case TargetOptions.SinglePlayerTarget:
				break;
			case TargetOptions.MultiPlayerTargets:
				break;
			case TargetOptions.MultiEnemyTargets:
				break;
			default:
				break;
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
				//attacker.GetComponent<SurventUnitManager>().isActive = false;
				Effect.Set(victim, EffectType.Attack, attacker.GetComponent<SurventUnitManager>().survent.ATK);
			}
			else if(attackerType == CardType.Spell)
			{
				CardSOAsset spellCard = attacker.GetComponent<CardDisplay>().Asset;
				//Effect.Set(victim, spellCard.SpellActionType, spellCard.SpellActionValue1, spellCard.SpellActionValue2);
				//player.CurrentActionPoint -= spellCard.Cost;
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

	//TODO ʤ��
	public GameObject victory;

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
		if(playerUnit != null && Player.Instance != null)
		{
			playerUnit.SendMessage("Initialized");
			deck = Player.Instance.cardSet;
		}
	}
	
	void TestSetData() //������������
	{
		Debug.Log("Start Data Setting...");
		ArchiveManager.LoadPlayerData(1);
		
		playerUnit.SendMessage("Initialized");

		bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(0)));
		Debug.Log(Const.BOSS_DATA_PATH(0));

		Debug.Log("���������������");
	}

}

