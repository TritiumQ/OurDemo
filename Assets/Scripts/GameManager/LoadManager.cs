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

    public int time = 2000;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());//开启协程
    }

    IEnumerator LoadLevel()
    {
        loadScreen.SetActive(true);//可以加载场景
        //AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        if (operation != null)
        {
            operation.allowSceneActivation = false;//不允许场景自动跳转
            while (!operation.isDone)//场景加载没有完成时
            {
                slider.value = operation.progress;//slider的值=加载的进度值
                text.text = operation.progress * 100 + "%";
                if (operation.progress >= 0.9F)
                {
                    slider.value = 1.0f;
                    text.text = "100%";
                    System.Threading.Thread.Sleep(time);
                    operation.allowSceneActivation = true;//允许场景自动跳转
                }

                yield return null;//跳出协程
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
                    scene = "安全屋";
                }
                break;
            case 3:
                {
                    scene = "随机遭遇";
                }
                break;
            case 4:
                {
                    scene = "Shop";
                }
                break;
            case 5:
                {
                    scene = "Fight";//boss战
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
