using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossSOAsset : ScriptableObject
{
	[Header("基本信息")]
    public int ID;
    public string Name;
    public int MaxHP;
    public int ATK;

	[Header("行动循环")]
    public List<BossActionType> ActionCycle;
    [Header("技能列表")]
    public List<EffectPackageWithTargetOption> SkillList;
    [Header("可召唤随从列表")]
    public List<int> SummonList;

	[Header("特殊效果列表")]
    public List<SpecialSkillPackage> SpecialSkillList;
    //public Image HeadIcon;

}
