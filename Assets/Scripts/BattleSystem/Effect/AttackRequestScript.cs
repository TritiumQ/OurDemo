using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AttackRequestScript : MonoBehaviour, IPointerClickHandler
{
    BattleSystem system;
    SurventUnitManager manager;
    void Start()
    {
        system = GameObject.Find(FightSceneObjectName.BattleSystem).GetComponent<BattleSystem>();
        manager = GetComponent<SurventUnitManager>();
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2 && manager.survent.SurventType == CardType.Survent)
		{
            if (manager.isActive && system != null)
            {
                if (system.EffectInitiator == null)
                {
                    EffectPackageWithTargetOption effect = new EffectPackageWithTargetOption();
                    effect.EffectType = manager.survent.IsVampire ? EffectType.VampireAttack : EffectType.Attack;
                    Debug.Log(effect.EffectType);
                    effect.EffectValue1 = manager.survent.ATK;
                    effect.Target = TargetOptions.SingleEnemyTarget;
                    effect.SingleTargetOption = SingleTargetOption.SpecificTarget;
                    system.EffectSetupRequest(gameObject, effect, transform.position, CardType.Survent);
                }
                else
                {
                    system.EffectSetupOver();
                    Debug.Log("Attack Dame");
                }
            }
        }
    }
}