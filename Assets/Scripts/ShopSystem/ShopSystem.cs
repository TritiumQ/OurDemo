using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public ShopType shopType;
    /// <summary>
    /// ��λ�����,���̵��������
    /// </summary>
    int currentMoneys;
	public TextMeshProUGUI moneyText;

	[Header("����ѡ��")]
	public Button Card1;
	public Button Card2;
	public Button Card3;
	public Button Card4;

	[Header("��Ʒѡ��")]
	public Button goods1;
	public Button goods2;
	public Button goods3;

	private void Awake()
	{

	}

	private void Update()
	{
		moneyText.text = currentMoneys.ToString();
	}

	public void SetCard(int pos)
	{
		switch(pos)
		{
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			default:
				Debug.LogWarning("SetCard: �趨λ��λ�ô���");
				break;
		}
	}
	public void SetGoods(int pos, GoodsSOAsset asset)
	{
		switch (pos)
		{
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			default:
				Debug.LogWarning("SetCard: �趨λ��λ�ô���");
				break;
		}
	}
	public void TestLoadData()
	{

	}
}
public enum ShopType
{
    Void,
    Shop,
    ShopInGame,
}
