using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitDisplay : MonoBehaviour
{
	PlayerInBattle player;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Update()
	{
		Refresh();
	}
	public void Refresh() //����ս���㲢����¼��Player���У�����Ҫ����ˢ��
	{
		if(player != null)
		{
			hpText.text = player.playerCurrentHP.ToString();
			actionPointText.text = player.playerCurrentActionPoint.ToString();
		}
	}
}
