using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
    int round;
	Button endButton;

	public GameObject dataManager; //��Ϸ���ݹ�����

	List<int> deck;  //�ƶ�
	List<int> hands;  //����
	List<Card> handCards;
	List<int> usedCards;  //���ƶ�

	List<SurventUnit> surventUnits;  //������ʵ��
	List<SurventUnit> enemyUnits;  //�������ʵ��

	public GameObject surventPrefab;
	public GameObject cardPrefab;  

	public GameObject playerHands; //�����������
	public GameObject enemyArea;  //�з��������
	public GameObject surventArea;  //����������

	public GameObject playerBody;
	public GameObject bossBody;

	public TextMeshProUGUI roundText;

	private void Awake()
	{
		endButton = GameObject.Find("EndButton").GetComponent<Button>();
		endButton.onClick.AddListener(GamePlay);

		roundText.text = round.ToString();
		
		//deck = new List<int>();
		//for test
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };

		hands = new List<int>();
		handCards = new List<Card>();
		usedCards = new List<int>();

		surventUnits = new List<SurventUnit>();
		enemyUnits = new List<SurventUnit>();

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
		if (hands.Count == 10) return;
		if(deck.Count == 0) //�ƿ�գ�����ϴ���Լ���ճͷ�
		{
			RefreshDeck();
			//TODO ϴ�Ƴͷ� δʵ��
		}
		int randPos = Random.Range(0, deck.Count);
		hands.Add(deck[randPos]);

		GameObject newCard = Instantiate(cardPrefab,playerHands.transform); //����Ԥ�Ƽ�ʵ��

		Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
		Card card = new Card(Resources.Load<CardAsset>(Const.CARD_DATA_PATH(deck[randPos])));  //���ݱ�ţ����ļ��ж�ȡ��������
		
		handCards.Add(card);
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

