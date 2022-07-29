using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Effect
{
	//数值1代表效果数值,如伤害值,回复量
	//数值2代表持续回合数,0为默认值
    public static void SingleTargetAttack(GameObject _targetObject, CardActionType _action, int _value1, int _value2 = 0)
	{
		if( _targetObject != null )
		{
			if(_targetObject.GetComponent<BossUnitManager>() != null)
			{
				BossUnitManager target = _targetObject.GetComponent<BossUnitManager>();
				switch (_action)
				{
					case CardActionType.Attack:
						target.BeAttacked(_value1);
						break;
					case CardActionType.VampireAttack: //吸血攻击未实现
						//
						//需要返回一个吸血值
						break;
					case CardActionType.Heal:
						target.BeHealed(_value1);
						break;
					case CardActionType.HPEnhance:
						target.BeEnhanced(_value1);
						break;
					case CardActionType.Inspire:
						target.BeInspired(_value1,_value2);
						break;
					case CardActionType.Waghhh:
						target.Waghhh(_value1);
						break;
					case CardActionType.Conceal:
						//boss本体无法被隐匿
					case CardActionType.Taunt:
						//boss本体无法被嘲讽
					case CardActionType.Protect:
						//boss本体无法被加护
					default:
						break;		
				}
			}
			else if(_targetObject.GetComponent<SurventUnitManager>() != null)
			{
				SurventUnitManager target = _targetObject.GetComponent<SurventUnitManager>();
				switch (_action)
				{
					case CardActionType.Attack:
						target.BeAttacked(_value1);
						break;
					case CardActionType.VampireAttack: //吸血攻击未实现
						//
						//需要返回一个吸血值
						break;
					case CardActionType.Heal:
						target.BeHealed(_value1);
						break;
					case CardActionType.HPEnhance:
						target.BeEnhanced(_value1);
						break;
					case CardActionType.Inspire:
						target.BeInspired(_value1, _value2);
						break;
					case CardActionType.Waghhh:
						target.Waghhh(_value1);
						break;
					case CardActionType.Conceal:
						target.BeConcealed(_value1);
						break;
					case CardActionType.Taunt:
						target.BeTaunted(_value1);
						break;
					case CardActionType.Protect:
						target.BeProtected(_value1);
						break;
					default:
						break;
				}
			}
			else if(_targetObject.GetComponent<PlayerUnitManager>() != null)
			{
				PlayerUnitManager target = _targetObject.GetComponent<PlayerUnitManager>();
				switch (_action)
				{
					case CardActionType.Attack:
						target.BeAttacked(_value1);
						break;
					case CardActionType.VampireAttack: 
						//吸血攻击未实现						   
						//					   
						//需要返回一个吸血值
						break;
					case CardActionType.Protect:
						target.BeProtected(_value1);
						break;
					case CardActionType.Heal:
						target.BeHealed(_value1);
						break;
					case CardActionType.HPEnhance:
					case CardActionType.Inspire:
					case CardActionType.Waghhh:
					case CardActionType.Conceal:
					case CardActionType.Taunt:
					default:
						break;
				}
			}
		}
	}
	public static void MultiTargetAttack(List<GameObject> _targets, CardActionType _action, int _value1, int _value2 = 0)
	{
		
	}
	

}
