using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Effect
{
	//��ֵ1����Ч����ֵ,���˺�ֵ,�ظ���
	//��ֵ2��������غ���,0ΪĬ�ϲ�����/����
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
					case CardActionType.VampireAttack: //��Ѫ����δʵ��
						target.boss.curentHP -= _value1;
						//��Ҫ����һ����Ѫֵ
						break;
					case CardActionType.Taunt:
						//boss�����޷�������?
						break;
					case CardActionType.Protect:
						//boss�����޷����ӻ�?
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
					default:
						break;		
				}
			}
			else if(_targetObject.GetComponent<SurventUnitManager>() != null)
			{
				
			}
		}
	}
	public static void MultiTargetAttack(List<GameObject> _targets, CardActionType _action, int _value1, int _value2 = 0)
	{
		
	}
	

}
