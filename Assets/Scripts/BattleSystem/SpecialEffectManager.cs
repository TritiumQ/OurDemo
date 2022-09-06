using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Ч��������
/// </summary>
public class SpecialEffectManager : MonoBehaviour
{
	#region ����Ч������������������SpecialEffectRegistry������һ����̬�ַ���
	/// <summary>
	/// ʹ1������ٴξ���
	/// </summary>
	void SetSurventActive(GameObject target)
	{
		Debug.Log("SetSurventActive");
		if(target.GetComponent<SurventUnitManager>() != null)
		{
			target.GetComponent<SurventUnitManager>().isActive = true;
		}
	}



	#endregion
}

/// <summary>
/// ���ڱ�������Ч��������
/// </summary>
public static class SpecialEffectRegistry
{
	
}