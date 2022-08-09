using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using TMPro;
public class SafeHouse : MonoBehaviour
{

    private Transform canvasTf;
    // Start is called before the first frame update
    void Awake()
    {
        canvasTf = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //ѡ��������
    public void selectTear()
    {
        Player.Instance.tears += 20;
        GameObject button = this.gameObject.transform.Find("Button").gameObject;
        button.SetActive(false);
        PlayerDataTF.EventContinue();
        ShowTip("�������20���", Color.red);
        Destroy(this.gameObject, 1);

    }
    //ѡ����
    public void selectCard()
    {
        //�������������ӵ�������ȥ��
        for (int i = 0; i < 2; i++)
        {
            int RandomNewCard = Random.Range(0, 200);//���Ҫ���֮�󿨿⿨����Ϣ��������
            Player.Instance.AddCard(RandomNewCard);
        }
        GameObject button = this.gameObject.transform.Find("Button (2)").gameObject;
        button.SetActive(false);
        PlayerDataTF.EventContinue();
        PlayerDataTF.EventContinue();
        ShowTip("����������2�ſ���", Color.red);

        Destroy(this.gameObject, 1);
    }
    //ѡ��ָ���
    public void selectCur()
    {
        if (Player.Instance.currentHP - Player.Instance.maxHP >= 2)
        {
            Player.Instance.currentHP += 2;
        }
        else
        {
            Player.Instance.currentHP = Player.Instance.maxHP;
        }
            GameObject button = this.gameObject.transform.Find("Button (1)").gameObject;
        button.SetActive(false);
        ShowTip("�������2��Ѫ���ָ�", Color.red);
        Destroy(this.gameObject, 1);
    }
    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("SafeHouse/Tips"), canvasTf) as GameObject;
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScale(2, 0.4f);
        Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.4f);
        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });
        MonoBehaviour.Destroy(obj, 2);
    }
}