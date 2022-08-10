using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurventInBattle
{
    //public Card card;
    //public CardSOAsset asset;

    public int atk;
    public int currentHP;
    public int maxHP;

    //����Ч����
    public bool isRaid;
    public int inspireRounds;
    public int inspireValue;
    public int concealRounds;
    //public int silenceRounds;
    public int tauntRounds;
    public int protectedTimes;
    public int doubleHitRounds;
   
    //public bool isVampire;
    public int vampireRounds;

    public bool isUndead; //����
    public EffectType deadWhisperEffect;
    public int deadWhisperEffectValue1;
    public int deadWhisperEffectValue2;
    public TargetOptions deadWhisperTarget;
    public int DeadWhisperEffectTargetCount;

    public bool isAdvanced; //�Ȼ�
    public EffectType advancedEffect;
    public int advancedEffectValue1;
    public int advancedEffectValue2;
    public TargetOptions advancedEffectTarget;
    public int AdvancedEffectTargetCount;

    public bool isSubsequent; //����
    public EffectType subsequentEffect;
    public int subsequentEffectValue1;
    public int subsequentEffectValue2;
    public TargetOptions subsequentEffectTarget;
    public int SubsequentEffectTargetCount;

    //��ӷ���ʱЧ��
    public bool IsSetupEffect;
    public EffectType SetupEffect;
    public int SetupEffectValue1;
    public int SetupEffectValue2;
    public TargetOptions SetupEffectTarget;
    public int SetupEffectTargetCount;

    public SurventInBattle(CardSOAsset _asset)
	{
        //����CardSOAsset��������
        atk = _asset.Atk;
        currentHP =  maxHP = _asset.MaxHP;
        isRaid = _asset.IsRaid;

        if(_asset.IsTank)
		{
            tauntRounds = Const.Forever;
		}
        else
		{
            tauntRounds = 0;
		}

        inspireRounds = 0;
        inspireValue = 0;
        concealRounds = 0;
        protectedTimes = 0;
        doubleHitRounds = 0;

        if (_asset.IsVampire == true)
        {
            vampireRounds = Const.Forever;
        }
        else
        {
            vampireRounds = 0;
        }
        //TODO ���Ч��

        /*
        isUndead = _asset.IsUndead;

        isAdvanced = _asset.IsAdvanced;

        isSubsequent = _asset.IsSubsequent;

        IsSetupEffect = _asset.IsSetupEffect;
        */

    }
}
