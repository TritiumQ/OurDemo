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

	[Header("Exit Button")]
	public Button exitButton;

	private void Awake()
	{
		exitButton.onClick.AddListener(Exit);
		
		TestLoadData();
	}

	private void Update()
	{
		moneyText.text = currentMoneys.ToString();
	}

	void Initialized()
	{
		if(Card1 != null)
		{

		}
		if(Card2 != null)
		{

		}
		if(Card3 != null)
		{

		}
		if(Card4 != null)
		{

		}

		if(goods1 != null)
		{

		}
		if(goods2 != null)
		{

		}
		if(goods3 != null)
		{

		}
	}

	void Exit()
	{

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
		if (Card1 != null)
		{
			Card1.GetComponent("Card").GetComponent<CardManager>().Initialized(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(0)));
		}
		if (Card2 != null)
		{

		}
		if (Card3 != null)
		{

		}
		if (Card4 != null)
		{

		}

		if (goods1 != null)
		{

		}
		if (goods2 != null)
		{

		}
		if (goods3 != null)
		{

		}
	}
}
public enum ShopType
{
    Void,
    Shop,
    ShopInGame,
}
