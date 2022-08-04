using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    //���ƽ��̽���㼶UI
    
    int level;
    
    public GameObject image_1;
    public GameObject image_2;
    public GameObject image_3;
    public GameObject image_4;
    void Start()
    {
        //level = 1;
        //image_1.SetActive(true);
    }
    public void SetLevel(int _level)//���ò�����ʾ
    {
        switch(_level)
        {
            case 1:
                {
                    image_1.SetActive(true);
                    image_2.SetActive(false);
                    image_3.SetActive(false);
                    image_4.SetActive(false);
                }
                break;
            case 2:
                {
                    image_1.SetActive(false);
                    image_2.SetActive(true);
                    image_3.SetActive(false);
                    image_4.SetActive(false);
                }
                break;
            case 3:
                {
                    image_1.SetActive(false);
                    image_2.SetActive(false);
                    image_3.SetActive(true);
                    image_4.SetActive(false);
                }
                break;
            case 4:
                {
                    image_1.SetActive(false);
                    image_2.SetActive(false);
                    image_3.SetActive(false);
                    image_4.SetActive(true);
                }
                break;
            default:
                {

                }
                break;

        }
        level = _level;
    }
}
