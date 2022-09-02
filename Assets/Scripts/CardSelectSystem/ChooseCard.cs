using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ChooseCard : MonoBehaviour,IPointerDownHandler
{
   
    private GameObject newCard;
    int RanNum;
    // Start is called before the first frame update
    void Start()
    {
        ArchiveManager.LoadPlayerData();
        RanNum = GetRandom.GetRandomCard();
        Debug.Log(RanNum);
        newCard = this.gameObject;
        Load();
    }
    public void Load()
    {

        var asset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(RanNum));
        if (asset != null)
        {
            newCard.GetComponent<CardManager>().Initialized(asset);
            newCard.GetComponent<CardManager>().IsActive = Player.Instance.Unlocked[RanNum];
            newCard.AddComponent<ZoomUI>();//���һ����껮���ͷŴ��Ч����
        }
            
          
    }
    //����ͽ�����id����
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Player.Instance.AddCard(RanNum);
        ArchiveManager.SavePlayerData(RanNum);
        FadeModel.Instance.fadeToScene();
        PlayerDataTF.EventContinue();
       
        //SceneManager.LoadScene("GameProcess");
    }
}