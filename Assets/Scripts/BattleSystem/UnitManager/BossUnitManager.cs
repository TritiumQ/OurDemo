using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner, IAbilityRunner
{
    public BossInBattle Boss;
    public TextMeshProUGUI HpText;
	public TextMeshProUGUI AtkText;
	public Image HeadIcon;

	public void Initialized(BossSOAsset _asset)
	{
		Boss = new BossInBattle(_asset);
	}
	private void Update()
	{
		RefreshState();
	}
	public void RefreshState()
	{
		if (Boss != null) //��ʾˢ��
		{
			HpText.text = Boss.CurrentHP.ToString();
			AtkText.text = Boss.ATK.ToString();
			if (Boss.CurrentHP <= 0)
			{
				Debug.Log("Boss�ѻ��ܣ�ս��ʤ��");
				//ʤ����Ч
				GameObject.Find("BattleSystem").SendMessage("GameEnd", GameResult.Success);
			}
		}
	}

	public void Die()
	{
		
	}

	public void AutoAction(int currentRound)
	{
		BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
		int mode = currentRound % Boss.Cycle.Count;
		//TODO �°��ж�����
	}

	#region �Զ��ж��ӿ�(�ɰ�boss�ж�ѭ��)
	public void _Action(int currentRound)
	{
		BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
		int mod = currentRound % Boss.ActionCycle.Count;
		//TODO �޸�boss�ж�
		switch (Boss.ActionCycle[mod])
		{
			case BossActionType.AOEAttack:
				{
					EffectPackageWithTargetOption package = new EffectPackageWithTargetOption(EffectType.Attack, Boss.ATK, 0, 0, TargetOptions.AllPlayerCreatures, 0);
					sys.EffectDirectSetup(this.gameObject, package);
				}
				break;
			case BossActionType.AOEAttackExcludePlayer:
				{
					EffectPackageWithTargetOption package = new EffectPackageWithTargetOption(EffectType.Attack, Boss.ATK, 0, 0, TargetOptions.ALlPlayerCharacter, 0);
					sys.EffectDirectSetup(this.gameObject, package);
				}
				break;
			case BossActionType.SingleAttack:
				{
					Debug.Log("Boss���幥��");

					var TypeArr = System.Enum.GetValues(typeof(SingleTargetOption));
					SingleTargetOption rnd = (SingleTargetOption)TypeArr.GetValue(Random.Range(0, TypeArr.Length));
					switch (rnd)
					{
						case SingleTargetOption.RandomTarget:
							{

							}
							break;
						case SingleTargetOption.PlayerTarget:
							{
								//sys.playerUnitDisplay.BeAttacked(boss.ATK);
								Effect.Set(sys.playerUnit, EffectType.Attack, Boss.ATK);
							}
							break;
						case SingleTargetOption.LowestATKTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.ATK
											< target.GetComponent<SurventUnitManager>().survent.ATK)
										{
											target = obj;
										}
									}
									Effect.Set(target, EffectType.Attack, Boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case SingleTargetOption.HigestATKTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.ATK
											> target.GetComponent<SurventUnitManager>().survent.ATK)
										{
											target = obj;
										}
									}
									Effect.Set(target, EffectType.Attack, Boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case SingleTargetOption.LowestHPTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.CurrentHP
											< target.GetComponent<SurventUnitManager>().survent.CurrentHP)
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, EffectType.Attack, Boss.ATK);
								}
							}
							break;
						case SingleTargetOption.HighestHPTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.CurrentHP
											> target.GetComponent<SurventUnitManager>().survent.CurrentHP)
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, EffectType.Attack, Boss.ATK);
								}
							}
							break;
						default:
							break;
					}
				}
				break;
			case BossActionType.Summon:
				{
					Debug.Log("Boss�������");
					int rnd = Random.Range(0, Boss.SurventList.Count);
					sys.SetupBossSurvent(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(Boss.SurventList[rnd])));

				}
				break;
			case BossActionType.Skip:
				Debug.Log("Boss�����غ�");
				break;
			default:
				break;
		}
	}
	#endregion

	#region Ч�����պ����нӿ�
	public void AcceptEffect(object[] _parameterList)
	{
		GameObject initiator = (GameObject)_parameterList[0];
		EffectPackage effect = (EffectPackage)_parameterList[1];
		switch (effect.EffectType)
		{
			case EffectType.Attack:
				{
					Boss.BeAttacked(effect.EffectValue1);
				}
				break;
			case EffectType.VampireAttack:
				{
					EffectPackage returnEffect = new EffectPackage();
					returnEffect.EffectType = EffectType.Heal;
					returnEffect.EffectValue1 = Boss.BeAttacked(effect.EffectValue1);
					if(initiator != null)
					{
						object[] parameterTable = { null, returnEffect };
						initiator.SendMessage("AcceptEffect", parameterTable);
					}
				}
				break;
			case EffectType.Heal:
				Boss.BeHealed(effect.EffectValue1);
				break;
			case EffectType.Enhance:
				Boss.SetEnhance(effect.EffectValue1, effect.EffectValue2);
				break;
			case EffectType.Inspire:
				Boss.SetInspire(effect);
				break;
			default:
				break;
		}
	}
	public void UpdateEffect()
	{
		Boss.UpdateEffect();
	}
	#endregion

	#region ����Ч�����нӿ�
	public void SetupEffectTrigger() { }
	public void AdvancedEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if(effect.SkillType == AbilityType.�Ȼ�Ч��)
			{
				BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
				if(sys != null)
				{
					sys.EffectDirectSetup(this.gameObject, effect.Package);
				}
			}
		}
	}
	public void SubsequentEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if (effect.SkillType == AbilityType.����Ч��)
			{
				BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
				if (sys != null)
				{
					sys.EffectDirectSetup(this.gameObject, effect.Package);
				}
			}
		}
	}
	public void FeedbackEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if (effect.SkillType == AbilityType.�ܻ�����)
			{
				BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
				if (sys != null)
				{
					sys.EffectDirectSetup(this.gameObject, effect.Package);
				}
			}
		}
	}
	public void UndeadEffectTrigger() { }
	#endregion
}
