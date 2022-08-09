using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TestCardHub : MonoBehaviour
{
	Button save, start;

	private void Awake()
	{
		save = GameObject.Find("Save").GetComponent<Button>();
		start = GameObject.Find("Start").GetComponent<Button>();
		save.onClick.AddListener(Save);
		start.onClick.AddListener(GameStart);

	}

	void Save()  //����ǰ������Ϣ������ʵ���ļ�
	{
		Debug.Log("Save data...");

	}
		
	void GameStart()
	{
		Debug.Log("Game Start...");
		//SceneManager.LoadScene("FightScene",LoadSceneMode.Additive);  //��Ҫbuild�Ժ����ʹ�ø÷����л�����
	}
}
