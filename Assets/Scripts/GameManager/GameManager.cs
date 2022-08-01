using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ר���������������Ϸ���ݵ������
public class GameManager : MonoBehaviour
{
    Player player;       //����������or��copy

    public int step;     //�ؿ�����
    public int GameEventCount = 5;//�ؿ�����,�������Ͱ������Ϊ����
    public int level;     //����
    List<int> GameEvent; //��Ϸ�¼�

    public GameObject eventPrefab;

    public List<GameObject> eventUnit; //�¼���������
    private void Awake()
    {
        InitGameManager();
        player = new Player();
        PlayDataTF.GetData(player);
    }
    void Start()
    {
        
    }

    void Update()
    {
        RefreshData(); //���ݸ���
        //RefreshScenes(); //������ʾ����,���ڸ��Ե�gameobject�����н���
    }
    public void InitGameManager() //��ʼ����Ϸ�����߶���
    {
        //Player���ݼ���

        //�ؿ����ݳ�ʼ��
        InitGameEvent();
        //���عؿ��������ݣ����½�����Ϸ�Ľ��ȼ��أ�
    }
    void InitGameEvent(int _level = 1)   //��ʼ����Ϸ�¼�  Ԥ�ò�������(δ��������)
    {
        level = _level; //��ʼ�����ڲ���
        step = 0;//��ʼ�������¼�
        GameEvent = new List<int>();
        for (int i = 0; i < GameEventCount; i++)
        {
            if (i == GameEventCount - 1) //���һ���¼��������
                GameEvent.Add(5);
            else GameEvent.Add(Random.Range(1, 5));//��������¼�
        }
        //��ʼ���¼�Ԥ�����������
        //Debug.Log(GameEventCount);
        for (int i = 0; i < GameEventCount; i++)
        {
            GameObject newEvent = Instantiate(eventPrefab,transform); //�����¼�Ԥ�Ƽ�
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725),500,0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //�����¼����������
            eventUnit.Add(newEvent);
        }
    }
    void RefreshScenes() //������Ϸ���ڳ���
    {
        //���������ʾ����
        //��Ϸ������ʾ����
        //�¼���ť��ʾ����
        //
    }
    void RefreshData() //������Ϸ��������
    {
        //�����������ݸ���
        PlayDataTF.GetData(player);
        //������Ϸ���ݸ���
        int judge = PlayDataTF.GetResult();
        if (judge == 1)
        {
            step++;
            if (step == GameEventCount)
            {
                step = 0;
                //������һ�����Ϸʤ��
                Gameover(1);//��Ϸͨ��
            }
        }
        else if (judge == -1 || player.currentHP <= 0)
        {
            Gameover(0);//��Ϸʧ��
        }
    }
    void Gameover(int _result) //������Ϸ����
    {
        //_result���ƽ������,�ݶ� 0��ʧ�ܣ�1��ʤ��......
    }
    
    
    //�����ǹ��߷���
    float SetPosition(int i,float low,float up)//����x��λ��
    {
        float len,x;
        len = (up - low) / GameEventCount;
        x = i * len + len / 2 + low;
        Debug.Log(x);
        return x;
    }
}
