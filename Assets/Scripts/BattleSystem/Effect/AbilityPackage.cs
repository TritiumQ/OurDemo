using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��λ����Ч����
/// </summary>
[System.Serializable]
public class AbilityPackage
{
    public AbilityType SkillType;
    public EffectPackageWithTargetOption Package;
}

/// <summary>
/// ��������Ч��
/// </summary>
public enum AbilityType
{
	/// <summary>
	/// �ܵ��˺�ʱ����
	/// </summary>
	�ܻ�����,
	/// <summary>
	/// ��ӵ�λר��,����������ʱ����
	/// </summary>
	�Ȼ�Ч��,
	/// <summary>
	/// ÿ�غϿ�ʼʱ����
	/// </summary>
	�غϿ�ʼЧ��,
	/// <summary>
	/// ÿ�غϽ���ʱ����
	/// </summary>
	�غϽ���Ч��,
	/// <summary>
	/// ��λ����ʱ����
	/// </summary>
	����Ч��
}