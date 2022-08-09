using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeckManager : MonoBehaviour
{
    public Transform deckPanel;//��Ӧ����contextλ�ã�
    public Transform libraryPanel;//��Ӧ����contextλ�ã�
    public GameObject deckPrefab;//���鿨��Ԥ���壻
    public GameObject cardPrefab;//�ֿ⿨��Ԥ���壻
    public GameObject DataManager;//���ص�Ԥ���壻
    //private PlayerData playerData;//������ݣ�

    //private CardStore CardStore;//�ֿ⿨�ƣ�

    private List<GameObject> cardPool = new List<GameObject>();
    private Dictionary<int, GameObject> libraryDic = new Dictionary<int, GameObject>();//��¼���⿨��Ԥ�����뿨��id��Ϣ��
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();//��¼���鿨���뿨��id��Ϣ��
    // Start is called before the first frame update
    void Start()
    {
        //playerData = DataManager.GetComponent<PlayerData>();
        //CardStore = playerData.GetComponent<CardStore>();
        UpdateLibrary();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //�����⿨��չ�ֳ�����
    public void UpdateLibrary()
    {
        /*
        for (int i = 0; i < playerData.playerCards.Length; i++)
		{
            if (playerData.playerDeck[i] != 0)
            {
                CreatCard(i, CardState.Library);
            }
        }
        */
    }
    /*
    public void UpdataDeck()
    {
       for(int i=0;i<playerData.playerDeck.Length;i++)
        {
            if(playerData.playerDeck.Length!=0)
            {
                CreatCard(i, CardState.Deck);
            }
        }
    }*/
    public void UpdataCard(CardState _state,int _id)
    {
        /*
        //���ǵ���˿����ƣ������Ƽ�1�ص����⣻
        if (_state == CardState.Deck)
        {
           
            playerData.playerDeck[_id]--;
            playerData.playerCards[_id]++;
            CreatCard(_id, CardState.Library);//���ƻص����⣻

            // ���������ĳ�ſ�����Ϊ����ݻ�Ԥ����
            if (playerData.playerDeck[_id] == 0)
            {
                Destroy(deckDic[_id]);
                return;
            }
            //�ı俨���ϵĸÿ���������
            TextMeshProUGUI Text = deckDic[_id].transform.Find("Number").GetComponent<TextMeshProUGUI>();//�ҵ������Ͽ��Ƶ�������Ϣ��
            int count = int.Parse(Text.text);
            count--;
            Text.text = count.ToString();//����stringתint��ת��string��ʾ��
           

        }
        // //���ǵ���˿����ƣ�������ת�Ƶ����飻
        else if (_state==CardState.Library)
        {
            //�����и������������ڶ����޷���ӣ�
            if(playerData.playerDeck[_id] == 2)
            {
                return;
            }
            playerData.playerCards[_id]--;
            Destroy(libraryDic[_id]);//����ݻ�Ԥ���壻

            //��������л�û�д˿��򴴽���
            if (playerData.playerDeck[_id]==0)
            {
                CreatCard(_id, CardState.Deck);
            }
            playerData.playerDeck[_id]++;
            // //�ı俨���ϵĸÿ���������
            TextMeshProUGUI Text = deckDic[_id].transform.Find("Number").GetComponent<TextMeshProUGUI>();//�ҵ������ϸÿ��Ƶģ�
            int count = int.Parse(Text.text);
            count++;
            Text.text = count.ToString();//����stringתint��ת��string��ʾ��
           
        }
        */
    }
   public void CreatCard(int _id,CardState _cardState)
    {
        /*
        Transform targetPanel= libraryPanel;
        GameObject targetPrefab = cardPrefab;
        var reData = playerData.playerCards;
        if(_cardState==CardState.Library)
        {
            targetPanel = libraryPanel;
            targetPrefab = cardPrefab;

        }
        else if(_cardState==CardState.Deck)
        {
            targetPanel = deckPanel;
            targetPrefab = deckPrefab;
            reData = playerData.playerDeck;
        }
        GameObject newCard = Instantiate(targetPrefab, targetPanel);
        //TODO
        //newCard.GetComponent<CardDisplay>().card = CardStore.cardList[_id];
        deckDic.Add(_id, newCard);
        */
    }
}
   
