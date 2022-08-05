using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardTrigger : MonoBehaviour, IPointerClickHandler
{

	public GameObject thisCard;
	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.clickCount == 2 )
		{

			BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
			if (thisCard.GetComponent<CardDisplay>().card.cardType == CardType.Spell)
			{
				if (sys.attacker == null)
				{
					Debug.Log("����ʹ�÷�������");
					sys.UseCardByPlayer(thisCard);
				}
				else
				{
					sys.AttackOver();
				}
			}
			else
			{
				Debug.Log("����ʹ����ӿ���");
				sys.UseCardByPlayer(thisCard);
			}
		}
	}
}