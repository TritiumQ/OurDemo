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
    //����Ч����
    public int inspireRounds;
    public int inspireValue;

    public int silenceRounds;

    public int tauntRounds;

    public int protectTimes;
    
    //
    public BossInBattle(BossSOAsset _bossAsset)
	{
        inspireRounds = 0;
        silenceRounds = 0;
        silenceRounds = 0;
        tauntRounds = 0;
        protectTimes = 0;

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
