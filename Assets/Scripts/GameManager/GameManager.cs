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
        PlayerDataTF.GetData(player);
    }
    void Start()
    {
        //������
        //PlayDataTF.EventEnd();
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
        //GameEventCount = new int[5] { 0, 5, 6, 7, 8 }; 
        GameEventCount = LevelEvent.GameEventCount;//��ʼ��ÿ��ؿ�����
        InitGameEvent();
        //���عؿ��������ݣ����½�����Ϸ�Ľ��ȼ��أ�
    }
    void InitGameEvent(int _level = 1)   //��ʼ��/���� ��Ϸ�¼�
    {
        level = _level; //��ʼ�����ڲ���
        if(level==1)//��ʼ�������¼�
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
        GetComponent<GetRandom>().GetRandomEvent(GameEvent,level);
        
        //��ʼ���¼�Ԥ�����������
        for (int i = 0; i < GameEventCount[_level]; i++)//��ʼ���¼�Ԥ�����������
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //�����¼�Ԥ�Ƽ�
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //�����¼����������
            eventUnit.Add(newEvent);
        }
        
        //������һ�س�������
        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);
    }
    
    void RefreshData() //������Ϸ��������
    {
        //�����������ݸ���
        PlayerDataTF.GetData(player);
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
        player.AddMoney(2 * step, 0); //��Ϸ����
        //_result���ƽ������,�ݶ� 0��ʧ�ܣ�1��ʤ��......
        //Debug.Log("��Ϸʧ��");
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
        if(level<=4)
        {
            InitGameEvent(level); //ˢ���¼�
        }
        else//��Ϸͨ��
        {
            Gameover(1); //�������ҳ��
        }
        step = 0;//���ò���
    }
    float SetPosition(int i,float low,float up)//����x��λ��
    {
        float len,x;
        len = (up - low) / GameEventCount[level];
        x = i * len + len / 2 + low;
        //Debug.Log(x);
        return x;
    }

}
