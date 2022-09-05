using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ShopSystem : MonoBehaviour
{
    public ShopType shopType;
	public GoodsEffectManager manager;
    /// <summary>
    /// ��λ�����,���̵��������
    /// </summary>
    int currentMoneys;
	public TextMeshProUGUI moneyText;

	[Header("����ѡ��")]
	public Button Card1;
	int cardID1;
	int cardPrice1;

	public Button Card2;
	int cardID2;
	int cardPrice2;

	public Button Card3;
	int cardID3;
	int cardPrice3;

	public Button Card4;
	int cardID4;
	int cardPrice4;

	[Header("��Ʒѡ��")]
	public Button goods1;
	int goods1Count = 2;
	public Button goods2;
	int goods2Count = 2;
	public Button goods3;
	int goods3Count = 2;

	[Header("Exit Button")]
	public Button exitButton;

	private void Awake()
	{
		exitButton.onClick.AddListener(Exit);
		currentMoneys = 0;

		manager = GetComponent<GoodsEffectManager>();

		Initialized();
	}

	private void Update()
	{
		moneyText.text = currentMoneys.ToString();
		if(shopType == ShopType.Shop)
		{
			goods1.GetComponent<GoodsManager>().SetCount(goods1Count);
			goods2.GetComponent<GoodsManager>().SetCount(goods2Count);
			goods3.GetComponent<GoodsManager>().SetCount(goods3Count);
			if(goods1Count <= 0)
			{
				goods1.enabled = false;
			}
			if(goods2Count <= 0)
			{
				goods2.enabled = false;
			}
			if (goods3Count <= 0)
			{
				goods3.enabled = false;
			}
		}
	}

	void Initialized()
	{
		for (int i = 1; i <= 4; i++)
		{
			Debug.Log("Card" + i);
			SetCard(i, ArchiveManager.LoadCardAsset(GetRandom.GetRandomCard()));
		}
		for (int i = 1; i <= 3; i++)
		{
			GoodsSOAsset asset = null;
			if(shopType == ShopType.Shop)
			{
				asset = ArchiveManager.LoadGoodsAsset(i);
			}
			else if(shopType == ShopType.ShopInGame)
			{
				asset = ArchiveManager.LoadGoodsAsset(i + 10);
			}
			if(asset != null)
			{
				SetGoods(i, asset);
				Debug.Log("Load Goods:" + asset.name);
			}

		}
		Load();
	}

	void Exit()
	{
		//TODO �˳��̵�
		Save();
		if(shopType == ShopType.Shop)
		{
			SceneManager.LoadScene("Main");
		}
		else if(shopType == ShopType.ShopInGame)
		{
			PlayerDataTF.EventContinue();
			SceneManager.LoadScene("GameProcess");
		}
	}

	public void SetCard(int pos, CardSOAsset asset)
	{
		int price;
		switch(asset.CardRarity)
		{
			case RarityRank.Normal:
				price = Const.NormalCardPrice;
				break;
			case RarityRank.Rare:
				price = Const.RareCardPrice;
				break;
			case RarityRank.Epic:
				price = Const.EpicCardPrice;
				break;
			case RarityRank.Legend:
				price = Const.LegendCardPrice;
				break;
			default:
				price = 39;
				break;
		}
		if(shopType == ShopType.ShopInGame)
		{
			price /= 2;
		}
		switch(pos)
		{
			case 1:
				{
					cardPrice1 = price;
					Card1.GetComponentInChildren<CardManager>().Initialized(asset);
					Card1.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card1.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice1.ToString();
				}
				break;
			case 2:
				{
					cardPrice2 = price;
					Card2.GetComponentInChildren<CardManager>().Initialized(asset);
					Card2.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card2.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice2.ToString();
				}
				break;
			case 3:
				{
					cardPrice3 = price;
					Card3.GetComponentInChildren<CardManager>().Initialized(asset);
					Card3.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card3.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice3.ToString();
				}
				break;
			case 4:
				{
					cardPrice4 = price;
					Card4.GetComponentInChildren<CardManager>().Initialized(asset);
					Card4.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card4.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice4.ToString();
				}
				break;
			default:
				//Debug.LogWarning("SetCard: �趨λ��λ�ô���");
				break;
		}
	}
	public void SetGoods(int pos, GoodsSOAsset asset)
	{
		switch (pos)
		{
			case 1:
				goods1.GetComponent<GoodsManager>().Initialized(asset);
				break;
			case 2:
				goods2.GetComponent<GoodsManager>().Initialized(asset);
				break;
			case 3:
				goods3.GetComponent<GoodsManager>().Initialized(asset);
				break;
			default:
				Debug.LogWarning("SetCard: �趨λ��λ�ô���");
				break;
		}
	}

	public void BuyGoods(int pos)
	{
		Debug.Log("Buy Goods 1");
		int price;
		Button goods;
		switch (pos)
		{
			case 1:
				price = goods1.GetComponent<GoodsManager>().asset.GoodsPrice;
				goods = goods1;
				break;
			case 2:
				price = goods2.GetComponent<GoodsManager>().asset.GoodsPrice;
				goods = goods2;
				break;
			case 3:
				price = goods3.GetComponent<GoodsManager>().asset.GoodsPrice;
				goods = goods3;
				break;
			default:
				price = 39;
				goods = null;
				Debug.LogWarning("��Ʒλ�ô���");
				break;
		}
		if(goods != null && currentMoneys - price > 0)
		{
			currentMoneys -= price;
			manager.SendMessage(goods.GetComponent<GoodsManager>().asset.GoodsEffectName);
			if(goods == goods1)
			{
				goods1Count--;
			}
			if(goods == goods2)
			{
				goods2Count--;
			}
			if(goods == goods3)
			{
				goods3Count--;
			}
		}
		
	}

	public void BuyCard(int pos)
	{
		Debug.Log("Buy Card 1");
		switch (pos)
		{
			case 1:
				if(currentMoneys - cardPrice1 > 0)
				{
					currentMoneys -= cardPrice1;
					Player.Instance.AddCard(cardID1);
					Card1.enabled = false;
				}
				break;
			case 2:
				if(currentMoneys - cardPrice2 > 0)
				{
					currentMoneys -= cardPrice2;
					Player.Instance.AddCard(cardID2);
					Card2.enabled = false;
				}
				break;
			case 3:
				if(currentMoneys - cardPrice3 > 0)
				{
					currentMoneys -= cardPrice3;
					Player.Instance.AddCard(cardID3);
					Card3.enabled = false;
				}
				break;
			case 4: 
				if(currentMoneys - cardPrice4 > 0)
				{
					currentMoneys -= cardPrice4;
					Player.Instance.AddCard(cardID4);
					Card4.enabled = false;
				}
				break;
			default:
				Debug.LogWarning("��Ʒλ�ô���");
				break;
		}
	}

	void Load()
	{
		if (shopType == ShopType.Shop)
		{
			currentMoneys =  Player.Instance.Mithrils;
		}
		else if (shopType == ShopType.ShopInGame)
		{
			currentMoneys = Player.Instance.Tears;
		}
		else
		{
			currentMoneys = 39;
		}
	}

	void Save()
	{
		if (shopType == ShopType.Shop)
		{
			Player.Instance.SetMithrils(currentMoneys);
		}
		else if (shopType == ShopType.ShopInGame)
		{
			Player.Instance.SetTears(currentMoneys);
		}
		ArchiveManager.SavePlayerData(1);
	}
	
}
public enum ShopType
{
    Void,
    Shop,
    ShopInGame,
}
