using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Effect
{
	//��ֵ1����Ч����ֵ,���˺�ֵ,�ظ���
	//��ֵ2��������غ���,0ΪĬ��ֵ
    public static void Set(GameObject _targetObject, CardActionType _action, int _value1, int _value2 = 0, GameObject _requester = null)
	{
		if( _targetObject != null )
		{
			string msg = "��" + _targetObject.name + "���𹥻�";
			Debug.Log(msg);
			if(_targetObject.GetComponent<BossUnitManager>() != null)
			{
				BossUnitManager target = _targetObject.GetComponent<BossUnitManager>();
				switch (_action)
				{
					case CardActionType.Attack:
						target.BeAttacked(_value1);
						break;
					case CardActionType.VampireAttack: //��Ѫ����
						if(_requester != null)
						{
							int damage = target.BeAttacked( _value1);
							Set(_requester,CardActionType.Heal,damage);
						}
						else
						{
							Debug.LogWarning("���������Ѫ��������Ѫ�������������");
						}
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
						//boss�����޷�������
					case CardActionType.Taunt:
						//boss�����޷�������
					case CardActionType.Protect:
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
					case CardActionType.Attack:
						target.BeAttacked(_value1);
						break;
					case CardActionType.VampireAttack:
						if (_requester != null)
						{
							int damage = target.BeAttacked(_value1);
							Set(_requester, CardActionType.Heal, damage);
						}
						else
						{
							Debug.LogWarning("���������Ѫ��������Ѫ�������������");
						}
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
						if (_requester != null)
						{
							int damage = target.BeAttacked(_value1);
							Set(_requester, CardActionType.Heal, damage);
						}
						else
						{
							Debug.LogWarning("���������Ѫ��������Ѫ�������������");
						}
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
