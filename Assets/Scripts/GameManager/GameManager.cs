using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//ר���������������Ϸ���ݵ������
public class GameManager : MonoBehaviour
{
    Player player;       //����Ϊ����ģʽ

    public int step;     //�ؿ�����
    public int[] GameEventCount; //ÿ��ؿ�����
    public int level;     //����
    List<int> GameEvent; //��Ϸ�¼�

    public GameObject eventPrefab;
    public GameObject levelUI;
    public GameObject LoadManager;

    public List<GameObject> eventUnit; //�¼���������
    private void Awake()
    {
        InitGameManager();
        player = Player.Instance;
        PlayDataTF.GetData(player);
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
    public void InitGameManager() //��ʼ����Ϸ�����߶���
    {

        //Player���ݼ���

        //�ؿ����ݳ�ʼ��
        GameEventCount = new int[5] { 0, 5, 6, 7, 8 }; //��ʼ��ÿ��ؿ�������1-4��
        InitGameEvent();
        //���عؿ��������ݣ����½�����Ϸ�Ľ��ȼ��أ�
    }
    void InitGameEvent(int _level = 1)   //��ʼ��/���� ��Ϸ�¼�
    {
        level = _level; //��ʼ�����ڲ���
        step = 0;//��ʼ�������¼�
        GameEvent = new List<int>();
        levelUI.GetComponent<LevelUI>().SetLevel(level); //ˢ�²���UI
        for (int i = eventUnit.Count - 1; i >= 0; i--) //����ԭ���¼�UI����
        {
            Destroy(eventUnit[i]);
        }
        eventUnit.Clear();
        //Debug.Log(GameEventCount[_level]);
        for (int i = 0; i < GameEventCount[_level]; i++) //��������¼�������
        {
            if (i == GameEventCount[_level] - 1) //���һ���¼��������
                GameEvent.Add(5);
            else GameEvent.Add(Random.Range(1, 5));
        }
        //Debug.Log(GameEventCount);
        for (int i = 0; i < GameEventCount[_level]; i++)//��ʼ���¼�Ԥ�����������
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //�����¼�Ԥ�Ƽ�
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //�����¼����������
            eventUnit.Add(newEvent);
        }
        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);//������һ�س�������
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
        if (judge == 1)//ͨ����ǰ�¼�
        {
            AddStep();
        }
        else if (judge == -1 || player.currentHP <= 0)
        {
            Gameover(0);//��Ϸʧ��
        }
    }
    void Gameover(int _result) //������Ϸ����
    {
        //_result���ƽ������,�ݶ� 0��ʧ�ܣ�1��ʤ��......
        Debug.Log("��Ϸʧ��");
        //�л�End����
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
    }
    void AddLevel()
    {
        step = 0;
        level++;
        if(level<=4)
        {
            InitGameEvent(level); //ˢ���¼�
        }
        else//��Ϸͨ��
        {
            Gameover(1);
            //�������ҳ��
        }
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
