using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class SurventInBattle : UnitInBattle
{
    //readonly CardSOAsset asset;
    public int CardID;
    public string CardName;

    public bool IsRaid;
	public bool IsUndead;

	//public int HitCount;
	public bool IsDoubleHit;
	private int DoubleHitRound;

	public bool IsSubsequent;
	public bool IsFeedback;
	public bool IsAdvanced;

	public List<AbilityPackage> SpecialAbilityList;

	public CardType SurventType;
	public SurventInBattle(CardSOAsset _asset) 
		: base(_asset.MaxHP, _asset.MaxHP, _asset.Atk, 0, 0, false, 0, _asset.IsTank, 0, false, false, 0, false, 0, _asset.IsVampire)
	{
       if(_asset != null)
		{
            CardID = _asset.CardID;
            CardName = _asset.CardName;
            if(_asset.IsTank)
			{
                TauntRounds = Const.INF;
			}
            IsRaid = _asset.IsRaid;
			IsDoubleHit = _asset.IsDoubleHit;
			DoubleHitRound = 0;
			SurventType = _asset.CardType;
			if(_asset.IsVampire)
			{
				VampireRounds = Const.INF;
			}
			IsUndead = IsSubsequent = IsAdvanced = IsFeedback = false;
			SpecialAbilityList = _asset.SpecialAbilityList;
			foreach(var ability in _asset.SpecialAbilityList)
			{
				switch(ability.SkillType)
				{
					case AbilityType.�ܻ�����:
						IsFeedback = true;
						break;
					case AbilityType.����Ч��:
						IsAdvanced = true;
						break;
					case AbilityType.����Ч��:
						IsSubsequent = true;
						break;
					case AbilityType.����Ч��:
						IsUndead = true;
						break;
					default:
						break;
				}
			}
		}
    }
	public override void UpdateEffect()
	{
		base.UpdateEffect();
		if (IsRaid == true)
		{
			IsRaid = false;
		}
		if(DoubleHitRound > 0)
		{
			IsDoubleHit = true;
			DoubleHitRound--;
		}
		else
		{
			IsDoubleHit = false;
		}
	}
}
