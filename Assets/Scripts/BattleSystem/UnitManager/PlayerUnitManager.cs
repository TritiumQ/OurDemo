using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner
{
	BattleSystem system;
	public PlayerInBattle player { get; private set; }

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Awake()
	{
		system = GameObject.Find("BatttleSystem").GetComponent<BattleSystem>();
	}
	private void Update()
	{
		RefreshState();
	}

	/// <summary>
	/// ��ʼ��
	/// </summary>
	public void Initialized()
	{
		player = new PlayerInBattle(Player.Instance);
	}
	/// <summary>
	/// ����
	/// </summary>
	public void Settle()
	{
		Player.Instance.SetCurrentHP(player.CurrentHP);
	}

	public void AutoAction(int currentRound) { }

	public void RefreshState()
	{
		if (player != null)
		{
			hpText.text = player.CurrentHP.ToString();
			actionPointText.text = player.CurrentActionPoint.ToString();
		}
		if(player.CurrentHP <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		system.GameEnd(GameResult.Failure);
	}

	public void AcceptEffect(object[] _parameterList)
	{
		GameObject initiator = (GameObject)_parameterList[0];
		EffectPackage effect = (EffectPackage)_parameterList[1];
		switch (effect.EffectType)
		{
			case EffectType.Attack:
				{
					player.BeAttacked(effect.EffectValue1);
				}
				break;
			case EffectType.VampireAttack:
				{
					EffectPackage returnEffect = new EffectPackage();
					returnEffect.EffectType = EffectType.Heal;
					returnEffect.EffectValue1 = player.BeAttacked(effect.EffectValue1);
					system.ApplyEffectTo(initiator,gameObject,returnEffect);
				}
				break;
			case EffectType.Heal:
				{
					player.BeHealed(effect.EffectValue1);
				}
				break;
			default:
				break;
		}
	}

	public void UpdateEffect()
	{
		player.UpdateEffect();
	}

	#region �ж���ʹ�ýӿ�
	public bool CanUseActionPoint(int cost)
	{
		return (player.CurrentActionPoint - cost >= 0) ?  true : false;
	}
	public void UseActionPoint(int cost)
	{
		if(CanUseActionPoint(cost))
		{
			player.CurrentActionPoint -= cost;
		}
	}
	#endregion
}