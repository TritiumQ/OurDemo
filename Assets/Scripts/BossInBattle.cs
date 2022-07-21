using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInBattle
{
    readonly BossSOAsset bossAsset;
    public int id;
    public string name;
    public int maxHP;
    public int curentHP;
    public int ATK;
    public List<BossActionType> actionCycle;
    //public string actionLogicName;
    public BossInBattle(BossSOAsset _bossAsset)
	{
        bossAsset = _bossAsset;
        if(bossAsset != null)
		{
            id = bossAsset.ID;
            name = bossAsset.Name;
            curentHP = maxHP = bossAsset.MaxHP;
            ATK = bossAsset.ATK;
            actionCycle = bossAsset.ActionCycle;
            //actionLogicName = bossAsset.ActionLogicName;
		}
	}
}