using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
    int round;
	

	//public GameObject dataManager; //��Ϸ���ݹ�����

	List<int> deck;  //�ƶ�
	List<GameObject> handCards;  //����  //max count = 10
	List<int> usedCards;  //���ƶ�

	List<SurventUnit> surventUnits;
	List<SurventUnit> enemyUnits;  //���ʵ��  //max count = 7

	public Button endButton;

	public GameObject surventPrefab;
	public GameObject cardPrefab;  

	public GameObject playerHands; //�����������
	public GameObject enemyArea;  //�з��������
	public GameObject surventArea;  //����������

	public GameObject playerUnit;
	public GameObject bossUnit;

	public TextMeshProUGUI roundText;

	private void Awake()
	{
		//endButton.onClick.AddListener(GamePlay);

		roundText.text = round.ToString();
		
		//deck = new List<int>();
		//for test
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };
		handCards = new List<GameObject>(10);
		usedCards = new List<int>();

		surventUnits = new List<SurventUnit>(7);
		enemyUnits = new List<SurventUnit>(7);

		//TestLoadData();
		GamePlay();
	}
	void GamePlay()
	{
		RoundPlay();
		round++;
		roundText.text = round.ToString();
	}

	void GetCard()
	{
		if (handCards.Count == 10) return;
		if(deck.Count == 0) //�ƿ�գ�����ϴ���Լ���ճͷ�
		{
			RefreshDeck();
			//TODO ϴ�Ƴͷ� δʵ��
		}
		int randPos = Random.Range(0, deck.Count);

		GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //����Ԥ�Ƽ�ʵ��

		Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
		Card card = new Card(Resources.Load<CardAsset>(Const.CARD_DATA_PATH(deck[randPos])));  //���ݱ�ţ����ļ��ж�ȡ��������
		
		newCard.GetComponent<CardDisplay>().card = card;
		newCard.GetComponent<CardDisplay>().LoadInf();
		

		handCards.Add(newCard);
		//newCard.GetComponent<CardDisplay>().LoadInf();

		deck.RemoveAt(randPos);
	}
	
	void RefreshDeck()  //ˢ���ƶ�
	{
		Debug.Log("ˢ���ƶ�");
		for (int i = 0; i < usedCards.Count; i++)
		{
			deck.Add(usedCards[i]);
		}
		usedCards.Clear();
	}

	void RoundPlay() //���غ�����
	{
		//1 ��Ҵ��ƶѳ���
		if(round == 0) //���ֳ����ſ�
		{
			for(int i = 0; i < 3; i++)
			{
				GetCard();
				
			}
		}
		else //֮��ÿ�غϿ�ʼ��һ�ſ�
		{
			GetCard();
		}
		//2 ��Ҳ������/ʹ�÷�����

		//3 �������ж�

		//4 Boss�ж�

		//5 Boss����ж�

		//�����غ�


	}
	void TestLoadData()
	{
		Debug.Log("Start Data Test...");

		//dataManager.GetComponent<PlayerDataManager>().player;
	}

}

