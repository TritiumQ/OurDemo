using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public TextMeshProUGUI text;
    public string scene;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());//����Э��
    }
    public void SelectFight()
    {
        StartCoroutine(LoadFight());
    }
    IEnumerator LoadLevel()
    {
        loadScreen.SetActive(true);//���Լ��س���
        //AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        if (operation != null)
        {
            operation.allowSceneActivation = false;//���������Զ���ת
            while (!operation.isDone)//��������û�����ʱ
            {
                slider.value = operation.progress;//slider��ֵ=���صĽ���ֵ
                text.text = operation.progress * 100 + "%";
                if (operation.progress >= 0.9F)
                {
                    slider.value = 1.0f;
                    text.text = "100%";
                    System.Threading.Thread.Sleep(300);
                    operation.allowSceneActivation = true;//�������Զ���ת
                }

                yield return null;//����Э��
            }
            System.Threading.Thread.Sleep(500);
            if (scene == "Fight")
            {
                GameObject obj = GameObject.Find("GameManager");
                int level = obj.GetComponent<GameManager>().level;
                int step = obj.GetComponent<GameManager>().step;
                int enemy = GetRandom.GetRandomEnemy(level, step);
                Debug.Log(enemy);
                GameObject _battle = GameObject.Find("BattleSystem");
                if (_battle != null)
                {
                    Debug.Log("find");
                    _battle.GetComponent<BattleSystem>().LoadBossInformation(enemy);
                    Debug.Log("������Ϣ����ɹ�");
                }
            }
            GameObject _obj = GameObject.Find("Panel");
            _obj.SetActive(false);
        }
    }

    IEnumerator LoadFight()
    {
        //loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Fight");
        if (operation != null)
        {
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                if (operation.progress >= 0.9F)
                {
                    GameObject obj = GameObject.Find("GameManager");
                    int level = obj.GetComponent<GameManager>().level;
                    int step = obj.GetComponent<GameManager>().step;
                    int enemy = GetRandom.GetRandomEnemy(level, step,true);
                    Debug.Log(enemy);
                    GameObject _battle = GameObject.Find("BattleSystem");
                    if (_battle != null)
                    {
                        Debug.Log("find");
                        _battle.GetComponent<BattleSystem>().LoadBossInformation(enemy);
                        Debug.Log("������Ϣ����ɹ�");
                    }
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
            
        }
    }

    public void NextScene(int key)
    {
        switch(key)
        {
            case 1:
                {
                    scene = "Fight";
                }
                break;
            case 2:
                {
                    scene = "SceureHouse";
                }
                break;
            case 3:
                {
                    int level = GameObject.Find("GameManager").GetComponent<GameManager>().level;
                    if(level==1)
                    {
                        scene = "CardSelect";
                    }
                    else
                    {
                        int pos = Random.Range(1, 2+1);
                        if (pos == 1)
                            scene = "CardSelect";
                        else if (pos == 2)
                            scene = "Select";
                        else scene = null;
                    }
                }
                break;
            case 4:
                {
                    scene = "Shop";
                }
                break;
            case 5:
                {
                    scene = "Fight";//bossս
                }
                break;
            default:
                {
                    scene = null;
                }
                break;
        }
    }
}
