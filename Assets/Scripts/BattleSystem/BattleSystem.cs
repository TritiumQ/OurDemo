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
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 0,200 };
		handCards = new List<GameObject>(10);
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);
		//
		TestSetData();
	}
	/// <summary>
	/// ����鿨
	/// </summary>
	/// <param name="_count">�鿨��Ŀ</param>
	void DrawCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count < 10)
			{
				if (cardUsedFlag == deck.Count) //�ƿ�գ�����ϴ���Լ���ճͷ�
				{
					RefreshDeck();
					Debug.Log("ϴ�Ƴͷ�");
					//ϴ�Ƴͷ�,�����5��Ѫ
					EffectPackage effect = new EffectPackage(EffectType.Attack, 5, 0, 0, null);
					ApplyEffectTo(playerUnit, null, effect);

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
	}
	/// <summary>
	/// ��ȡ�ض�����
	/// </summary>
	/// <param name="_ID">����ID</param>
	/// <param name="_count">��������</param>
	void GetCard(int _ID, int _count)
	{
		if(handCards.Count < 10)
		{
			CardSOAsset asset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(_ID));
			if (asset != null)
			{

			}
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
				DrawCard(3);
			}
			else
			{
				DrawCard(1);
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
		//�鿨��Ч
		if (_effect.EffectType == EffectType.DrawSpecificCard || _effect.EffectType == EffectType.DrawRandomCard)
		{
			if (_target != null && _target.GetComponent<PlayerUnitManager>() != null)
			{
				switch (_effect.EffectType)
				{
					case EffectType.DrawSpecificCard:
						GetCard(effect.EffectValue1, effect.EffectValue2);
						break;
					case EffectType.DrawRandomCard:
						DrawCard(effect.EffectValue1);
						break;
					default:
						break;
				}
			}
		}
		else if (_effect.EffectType == EffectType.SpecialEffect)
		{
			//TODO ����Ч��
		}
		else
		{
			if (_target != null && _effect != null)
			{
				object[] ParameterList = { _initiator, _effect };
				_target.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
			}
		}
	}
	/// <summary>
	/// ֱ���ͷ�Ч��, Ŀ��ѡ��ʽ��Ч����ȷ��
	/// </summary>
	/// <param name="initiator">Ч��������</param>
	/// <param name="effect">Ч����Ϣ��������Ŀ����Ϣ��</param>
	public void ApplyEffect(GameObject initiator, EffectPackageWithTargetOption effect)
	{
		//�鿨��Ч
		if(effect.EffectType == EffectType.DrawSpecificCard || effect.EffectType == EffectType.DrawRandomCard)
		{
			switch (this.effect.EffectType)
			{
				case EffectType.DrawSpecificCard:
					GetCard(effect.EffectValue1, effect.EffectValue2);
					break;
				case EffectType.DrawRandomCard:
					DrawCard(effect.EffectValue1);
					break;
				default:
					break;
			}
		}
		else if(effect.EffectType == EffectType.SpecialEffect)
		{
			//TODO ����Ч��
		}
		else
		{
			EffectPackage eft = (EffectPackage)Package;
			object[] ParameterList = { initiator, eft };
			switch (effect.Target)
			{
				case TargetOptions.AllCreatures:
					{
						playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllPlayerCreatures:
					{
						playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllEnemyCreatures:
					{
						bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllCreaturesExcludeMainUnit:
					{
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllPlayerCreaturesExcludePlayerUnit:
					{
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.ALlEnemyCreaturesExcludeBossUnit:
					{
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.SinglePlayerTarget:
					{
						switch (effect.SingleTargetOption)
						{
							case SingleTargetOption.RandomTarget:
								{
									int rnd = Random.Range(0, PlayerSurventUnitsList.Count + 1);
									if(rnd == PlayerSurventUnitsList.Count)
									{
										playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
									}
									else
									{
										PlayerSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
									}
								}
								break;
							case SingleTargetOption.HighestHPTarget:
								{
									GameObject obj = playerUnit;
									int maxhp = playerUnit.GetComponent<PlayerUnitManager>().player.CurrentHP;
									foreach(var tmp in PlayerSurventUnitsList)
									{
										if(tmp.GetComponent<SurventUnitManager>().survent.CurrentHP > maxhp)
										{
											obj = tmp;
											maxhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestHPTarget:
								{
									GameObject obj = playerUnit;
									int minhp = playerUnit.GetComponent<PlayerUnitManager>().player.CurrentHP;
									foreach (var tmp in PlayerSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.CurrentHP < minhp)
										{
											obj = tmp;
											minhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.HigestATKTarget:
								{
									GameObject obj = playerUnit;
									int maxatk = playerUnit.GetComponent<PlayerUnitManager>().player.ATK;
									foreach (var tmp in PlayerSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK  > maxatk)
										{
											obj = tmp;
											maxatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestATKTarget:
								{
									GameObject obj = playerUnit;
									int minatk = playerUnit.GetComponent<PlayerUnitManager>().player.ATK;
									foreach (var tmp in PlayerSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK < minatk)
										{
											obj = tmp;
											minatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							default:
								break;
						}
					}
					break;
				case TargetOptions.SingleEnemyTarget:
					{
						switch (effect.SingleTargetOption)
						{
							case SingleTargetOption.RandomTarget:
								{
									int rnd = Random.Range(0, BossSurventUnitsList.Count + 1);
									if (rnd == BossSurventUnitsList.Count)
									{
										bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
									}
									else
									{
										BossSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
									}
								}
								break;
							case SingleTargetOption.HighestHPTarget:
								{
									GameObject obj = bossUnit;
									int maxhp = bossUnit.GetComponent<BossUnitManager>().Boss.CurrentHP;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.CurrentHP > maxhp)
										{
											obj = tmp;
											maxhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestHPTarget:
								{
									GameObject obj = bossUnit;
									int minhp = bossUnit.GetComponent<BossUnitManager>().Boss.CurrentHP;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.CurrentHP < minhp)
										{
											obj = tmp;
											minhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.HigestATKTarget:
								{
									GameObject obj = bossUnit;
									int maxatk = bossUnit.GetComponent<BossUnitManager>().Boss.ATK;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK > maxatk)
										{
											obj = tmp;
											maxatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestATKTarget:
								{
									GameObject obj = bossUnit;
									int minatk = bossUnit.GetComponent<BossUnitManager>().Boss.ATK;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK < minatk)
										{
											obj = tmp;
											minatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							default:
								break;
						}
					}
					break;
				case TargetOptions.MultiPlayerTargets:
					break;
				case TargetOptions.MultiEnemyTargets:
					break;
				default:
					break;
			}
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

