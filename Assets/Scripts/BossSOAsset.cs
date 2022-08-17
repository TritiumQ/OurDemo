using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ����Boss��Ϣ
/// </summary>
public class BossSOAsset : ScriptableObject
{
	[Header("������Ϣ")]
    public int ID;
    public string Name;
    public int MaxHP;
    public int ATK;
    public Sprite Icon;
    [Header("���ٻ�����б�")]
    public List<int> SummonList;
    [Header("����Ч���б�")]
    public List<AbilityPackage> SpecialAbilityList;
    /// <summary>
    /// ����boss���ж���
    /// </summary>
    [Header("�°��ж�ѭ��")]
    public List<BossActionPackage> ActionPackages;
    /// <summary>
    /// ����boss���ж�ѭ��
    /// </summary>
    public List<BossAction> Cycle;
	[Header("�ɰ��ж�ѭ��")]
    public List<BossActionType> ActionCycle;
}
[System.Serializable]
public class BossAction
{
    public List<int> ActionIndex;
}
