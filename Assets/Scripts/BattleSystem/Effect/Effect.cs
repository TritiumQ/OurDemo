using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effect
{
	/// <summary>
	/// �Ż��淽����ʵ�֣��÷�������
	/// </summary>
	/// <return></return>
    public static void Set(GameObject _targetObject, EffectType _action, int _value1, int _value2 = 0, GameObject _requester = null)
	{
		/*if( _targetObject != null )
		{
			string msg = "��" + _targetObject.name + "���𹥻�";
			Debug.Log(msg);
			if(_targetObject.GetComponent<BossUnitManager>() != null)
			{
				BossUnitManager target = _targetObject.GetComponent<BossUnitManager>();
				switch (_action)
				{
					case EffectType.Attack:
						target.BeAttacked(_value1);
						break;
					case EffectType.VampireAttack: //��Ѫ����
						if(_requester != null)
						{
							int damage = target.BeAttacked( _value1);
							Set(_requester,EffectType.Heal,damage);
						}
						else
						{
							Debug.LogWarning("���������Ѫ��������Ѫ�������������");
						}
						break;
					case EffectType.Heal:
						target.BeHealed(_value1);
						break;
					//case CardActionType.HPEnhance:
						//target.BeEnhanced(_value1);
						//break;
					case EffectType.Inspire:
						target.BeInspired(_value1,_value2);
						break;
					//case CardActionType.Waghhh:
						//target.Waghhh(_value1);
						//break;
					case EffectType.Conceal:
						//boss�����޷�������
					case EffectType.Taunt:
						//boss�����޷�������
					case EffectType.Protect:
						//boss�����޷����ӻ�
					default:
						break;		
				}
			}
			else if(_targetObject.GetComponent<SurventUnitManager>() != null)
			{
				SurventUnitManager target = _targetObject.GetComponent<SurventUnitManager>();
				switch (_action)
				{
					case EffectType.Attack:
						target.BeAttacked(_value1);
						break;
					case EffectType.VampireAttack:
						if (_requester != null)
						{
							int damage = target.BeAttacked(_value1);
							Set(_requester, EffectType.Heal, damage);
						}
						else
						{
							Debug.LogWarning("���������Ѫ��������Ѫ�������������");
						}
						break;
					case EffectType.Heal:
						target.BeHealed(_value1);
						break;
					//case CardActionType.HPEnhance:
						//target.BeEnhanced(_value1);
						//break;
					case EffectType.Inspire:
						target.BeInspired(_value1, _value2);
						break;
					//case CardActionType.Waghhh:
						//target.Waghhh(_value1);
						//break;
					case EffectType.Conceal:
						target.BeConcealed(_value1);
						break;
					case EffectType.Taunt:
						target.BeTaunted(_value1);
						break;
					case EffectType.Protect:
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
					case EffectType.Attack:
						target.BeAttacked(_value1);
						break;
					case EffectType.VampireAttack:
						if (_requester != null)
						{
							int damage = target.BeAttacked(_value1);
							Set(_requester, EffectType.Heal, damage);
						}
						else
						{
							Debug.LogWarning("���������Ѫ��������Ѫ�������������");
						}
						break;
					case EffectType.Protect:
						target.BeProtected(_value1);
						break;
					case EffectType.Heal:
						target.BeHealed(_value1);
						break;
					//case CardActionType.HPEnhance:
					case EffectType.Inspire:
					//case CardActionType.Waghhh:
					case EffectType.Conceal:
					case EffectType.Taunt:
					default:
						break;
				}
			}
		}*/
	}
	
	/// <summary>
	/// �ͷ�Ч����ͨ�÷���
	/// </summary>
	/// <param name="_target">Ч��Ŀ��</param>
	/// <param name="_initiator">Ч��������</param>
	/// <param name="_effect">Ч��������</param>
	public static void ApplyTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		if(_target != null && _initiator != null)
		{
			//Ч������
			object[] ParameterList = { _initiator, _effect };
			_target.SendMessage("AcceptEffect", ParameterList);
		}
	}
}
