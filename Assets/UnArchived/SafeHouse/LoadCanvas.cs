using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCanvas : MonoBehaviour
{
    public static LoadCanvas Instance;
    GameObject obj;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LoadUI("SafeHouse/Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�رյ�ǰUI,����һ��UI;
    public void CloseAndOpen(string CanvasName)
    {
        obj = Resources.Load("" + CanvasName) as GameObject;
        Instantiate(obj);
    }
    public void LoadUI(string CanvasName)
    {
        obj = Resources.Load("" + CanvasName) as GameObject;
        Instantiate(obj);
    }
}
