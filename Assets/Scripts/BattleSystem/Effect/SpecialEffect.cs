using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ⴥ������Ч��
/// </summary>
public enum SpecialSkillType
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
}
[System.Serializable]
public class SpecialSkillPackage
{
    public SpecialSkillType SkillType;
    public EffectPackageWithTargetOption Package;
}