using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossInBattle : UnitInBattle
{
    readonly BossSOAsset Asset;

    public int ID;
    public string Name;
    public Image Icon;
    public List<int> SurventList;

    public List<SpecialAbilityPackage> SpecialAbilityList;

    public List<BossActionType> ActionCycle;

    //�°��ж�ѭ��
    public List<BossActionPackage> ActionPackage;
    public List<BossAction> Cycle;

    //
    public BossInBattle(BossSOAsset asset):base(asset.MaxHP, asset.MaxHP, asset.ATK, 0, 0, 0, 0, 0)
	{
        Asset = asset;
        ID = asset.ID;
        Name = asset.Name;
        ActionCycle = asset.ActionCycle;
        SurventList = asset.SummonList;
        Icon = asset.Icon;
        SpecialAbilityList = asset.SpecialAbilityList;
        ActionPackage = asset.ActionPackages;
        Cycle = asset.Cycle;
	}

}
