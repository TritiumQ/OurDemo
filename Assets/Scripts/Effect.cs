using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

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
