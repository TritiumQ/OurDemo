using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SurventUnitManager : MonoBehaviour
{
    public GameObject thisSurvent;
    public SurventInBattle survent { get; private set; }
    public int GetInf(GetSurventInfomation _inf)
	{
        switch (_inf)
		{
            case GetSurventInfomation.MaxHP:
                return survent.maxHP;
            case GetSurventInfomation.ATK:
                return survent.atk;
            case GetSurventInfomation.CurrentHP:
                return survent.currentHP;
            default:
                return 0;
		}
	}
    public CardType type;
    public bool isActive;

    [Header("Text Component References")]
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;

    [Header("Image References")]
    public Image cardImage;
    public Image activeGlowImage;

	[Header("特殊效果图标")]
    public Image tauntImage;  //嘲讽
    public Image raidImage;  //快速
    public Image deadWhisperImage;  //亡语
    public Image vampireImage;  //吸血
    public Image protectedImage;  //加护
    public Image concealImage;  //隐匿
    public Image doubleHitImage;  //连击
    public void Initialized(Card _card)
    {
        if(_card != null)
		{
            survent = new SurventInBattle(_card);
            if (_card.cardType == CardType.Survent || _card.cardType == CardType.Monster)
            {
                type = _card.cardType;
                cardImage.sprite = _card.cardImage;
                //
                isActive = _card.isRaid;

                //所有随从效果的Icon默认关闭
                tauntImage.enabled = false;
                raidImage.enabled = false;
                deadWhisperImage.enabled = survent.isUndead;
                vampireImage.enabled = false;
                protectedImage.enabled = false;
                concealImage.enabled = false;
                doubleHitImage.enabled = false;
                //激活光效默认关闭
                activeGlowImage.enabled = false;

                Refresh();
            }
        }
    }
    private void Update()
    {
        Refresh();
    }
	void Refresh()  //刷新随从当前状态
    {
        if(survent != null)
		{
            atkText.text = survent.atk.ToString();
            hpText.text = survent.currentHP.ToString();
            if (survent.currentHP <= 0)
            {
                Die();
            }
            activeGlowImage.enabled = isActive;
            if (survent.tauntRounds > 0 || survent.tauntRounds == Const.Forever)
            {
                //Debug.Log("Tank");
                tauntImage.enabled = true;
            }
            else
            {
                tauntImage.enabled = false;
            }
            raidImage.enabled = survent.isRaid;
            deadWhisperImage.enabled = survent.card.isUndead;
            if (survent.vampireRounds > 0 || survent.vampireRounds == Const.Forever)
            {
                vampireImage.enabled = true;
            }
            else
            {
                vampireImage.enabled = false;
            }
            if (survent.protectedTimes > 0)
            {
                protectedImage.enabled = true;
            }
            else
            {
                protectedImage.enabled = false;
            }
            if (survent.concealRounds > 0)
            {
                concealImage.enabled = true;
            }
            else
            {
                concealImage.enabled = false;
            }
            if (survent.doubleHitRounds > 0)
            {
                doubleHitImage.enabled = true;
            }
            else
            {
                doubleHitImage.enabled = false;
            }
        }
    }
    void Die()
	{
        //死亡动画
        string msg = survent.card.cardName + "死亡";
        Debug.Log(msg);
        if(type == CardType.Survent)
		{
            GameObject.Find("BattleSystem").GetComponent<BattleSystem>().PlayerSurventDie(thisSurvent);
		}
        else
		{
            GameObject.Find("BattleSystem").GetComponent<BattleSystem>().BossSurventDie(thisSurvent);
        }
        Destroy(thisSurvent);
        
	}
    public void Action() //随从行动
	{
        if(isActive == true)
		{
            //TODO 随从行动


            isActive = false;
		}
        else
		{
            return;
		}
	}

    public void CheckInStart() //每回合开始调用,重置活动状态,触发先机效果
	{
        isActive = true;
        //

	}
    public void CheckInEnd() //每回合结束调用,刷新buff并触发后手效果
	{
        if(survent.isRaid == true)
	    {
            survent.isRaid = false;
	    }
        if(survent.inspireRounds > 0)
		{
            survent.inspireRounds--;
            if(survent.inspireRounds == 0)
			{
                survent.atk -= survent.inspireValue;
                survent.inspireRounds = 0;
                survent.inspireValue = 0;
			}
        }
        if(survent.tauntRounds > 0 && survent.tauntRounds != Const.Forever)
		{
            survent.tauntRounds--;
		}
        if(survent.concealRounds > 0)
		{
            survent.concealRounds--;
		}
        if(survent.doubleHitRounds > 0)
		{
            survent.doubleHitRounds--;
		}
        if(survent.vampireRounds > 0 && survent.vampireRounds != Const.Forever)
		{
            survent.vampireRounds--;
		}
	}
    public int BeVampireAttack(int _value)
	{
        return 0;
	}
    public void SetVampire(int _value)
	{
        survent.vampireRounds += _value;
	}
    public void BeAttacked(int _value)
	{
        if(survent.protectedTimes > 0)
		{
            survent.protectedTimes--;
		}
        else
		{
            survent.currentHP -= _value;
		}
	}
    public void BeHealed(int _value)
	{
        if(survent.currentHP + _value <= survent.maxHP)
		{
            survent.currentHP += _value;
		}
        else
		{
            survent.currentHP = survent.maxHP;
		}
	}
    public void BeConcealed(int _value)
	{
        survent.concealRounds += _value;
	}
    public void BeEnhanced(int _value)
	{
        survent.maxHP += _value;
    }
    public void BeInspired(int _value, int _rounds) //可延长叠加
	{
        survent.atk += _value;
        survent.inspireValue += _value;
        survent.inspireRounds += _rounds;
	}
    public void Waghhh(int _value)
	{
        survent.atk += _value;
	}
    public void BeProtected(int _times)
	{
        
	}
    public void BeTaunted(int _rounds)
	{
        
	}
}
