using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitDisplay : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Start()
	{
		Refresh(0);
	}
	void Refresh(int _actionPoint) //����ս���㲢����¼��Player���У�����Ҫ����ˢ��
	{
		if(player != null)
		{
			hpText.text = player.currentHP.ToString();
			actionPointText.text = _actionPoint.ToString();
		}
	}
}
