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
	����Ч��,
	/// <summary>
	/// ÿ�غϿ�ʼʱ����
	/// </summary>
	�Ȼ�Ч��,
	/// <summary>
	/// ÿ�غϽ���ʱ����
	/// </summary>
	����Ч��,
	/// <summary>
	/// ��λ����ʱ����
	/// </summary>
	����Ч��
}