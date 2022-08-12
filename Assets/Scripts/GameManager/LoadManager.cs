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

    public int time = 1000;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());//����Э��
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
                    System.Threading.Thread.Sleep(time);
                    operation.allowSceneActivation = true;//�������Զ���ת
                }

                yield return null;//����Э��
            }
            System.Threading.Thread.Sleep(time);
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
                    scene = "SafeHouse";
                }
                break;
            case 3:
                {
                    scene = "�������";
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
