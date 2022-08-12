using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//ר���������������Ϸ���ݵ������
public class GameManager : MonoBehaviour
{
    Player player;       //����Ϊ����ģʽ

    //TODO
    //�ṹ�Ż�
    public int step;     //�ؿ�����
    public int[] GameEventCount; //ÿ��ؿ�����
    public int level;     //����
    public int result=-1;    //��Ϸ���
    public List<int> GameEvent; //��Ϸ�¼�

    public GameObject eventPrefab;
    public GameObject levelUI;
    public GameObject LoadManager;

    public List<GameObject> eventUnit; //�¼���������
    private void Awake()
    {
        InitGameManager();
        player = Player.Instance;
    }
    void Start()
    {
        //������
        //step = 4; level = 2;
        //PlayerDataTF.EventEnd();
        //InitGameEvent(4);
    }

    void Update()
    {
        RefreshData(); //���ݸ���
    }
    void InitGameManager() //��ʼ����Ϸ�����߶���
    {

        //Player���ݼ���

        //�ؿ����ݳ�ʼ��
        GameEventCount = GameConst.GameEventCount;//��ʼ��ÿ��ؿ�����
        InitGameEvent();
        //���عؿ��������ݣ����½�����Ϸ�Ľ��ȼ��أ�
    }
    void InitGameEvent(int _level = 1)   //��ʼ��/���� ��Ϸ�¼�
    {
        level = _level; //��ʼ�����ڲ���
        step = 0;
        GameEvent = new List<int>();

        //ˢ�²���UI
        levelUI.GetComponent<LevelUI>().SetLevel(level);
        
        //����ԭ���¼�UI����
        for (int i = eventUnit.Count - 1; i >= 0; i--)
        {
            Destroy(eventUnit[i]);
        }
        eventUnit.Clear();
        
        //α��������¼�������
        GetRandom.GetRandomEvent(GameEvent,level);
        
        //��ʼ���¼�Ԥ�����������
        for (int i = 0; i < GameEventCount[level]; i++)//��ʼ���¼�Ԥ�����������
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //�����¼�Ԥ�Ƽ�
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //�����¼����������
            eventUnit.Add(newEvent);
        }
        
        //������һ�س�������
        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[0]);
    }
    
    void RefreshData() //������Ϸ��������
    {
        //������Ϸ���ݸ���
        int judge = PlayerDataTF.GetResult();
        if (judge == 1)//ͨ����ǰ�¼�
        {
            AddStep();
        }
        else if (judge == -1 /*|| player.currentHP <= 0*/)
        {
            Gameover(0);//��Ϸʧ��
        }
    }
    void Gameover(int _result) //������Ϸ����
    {
        result = _result;
        GetReward();
        //_result���ƽ������,�ݶ� 0��ʧ�ܣ�1��ʤ��......
        //�л�End����
        SceneManager.LoadScene("EndScene");
    }


    //�����ǹ��߷���

    void AddStep()
    {
        eventUnit[step].GetComponent<EventUI>().SetPass();//�¼�ͼ�����
        step++;
        if (step < GameEventCount[level])
        {
            LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);//������һ�س�������
        }
        else//ͨ����ǰ����
        {
            AddLevel();
        }
        //TODO
        //���ݱ���
    }
    void AddLevel()
    {
        level++;
        step = 0;//���ò���
        if (level<GameConst.GameEventCount.Length)
        {
            InitGameEvent(level); //ˢ���¼�
        }
        else//��Ϸͨ��
        {
            Gameover(1); //�������ҳ��
        }
    }
    float SetPosition(int i,float low,float up)//����x��λ��
    {
        float len,x;
        len = (up - low) / GameEventCount[level];
        x = i * len + len / 2 + low;
        return x;
    }

    void GetReward()
    {
        int value=0;
        for(int i=1;i<level;i++)
        {
            value += GameEventCount[i];
        }
        value += step;
        value *= 2;
        player.AddMoney(value, 0);
    }

}
