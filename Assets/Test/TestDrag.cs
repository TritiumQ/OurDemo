using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TestDrag : MonoBehaviour
{
    //public TargetingOptions Targets = TargetingOptions.AllCharacters;
    private SpriteRenderer sr;
    private LineRenderer lr;
    private Transform triangle;
    private SpriteRenderer triangleSR;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponentInChildren<LineRenderer>();
        lr.sortingLayerName = "AboveEverything";
        triangle = transform.Find("Triangle");
        triangleSR = triangle.GetComponent<SpriteRenderer>();
    }

    public void OnStartDrag()
    {
        sr.enabled = true;
        lr.enabled = true;
    }

    public void OnDraggingInUpdate()
    {
        // ����ͷ��Triangle��
        Vector3 notNormalized = transform.position - transform.parent.position; // Target�����Ƶ�����
        Vector3 direction = notNormalized.normalized; // ��׼��
        float distanceToTarget = (direction * 2.3f).magnitude; // ����
        if (notNormalized.magnitude > distanceToTarget)
        {
            // �ڿ�����target֮�仭��
            lr.SetPositions(new Vector3[] { transform.parent.position, transform.position - direction * 2.3f });
            lr.enabled = true;

            triangleSR.enabled = true;
            triangleSR.transform.position = transform.position - 1.5f * direction;

            // ��ת��ͷ
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            triangleSR.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            // ���target�뿨�����Ĳ�Զ������Ҫ��ʾ��ͷ
            lr.enabled = false;
            triangleSR.enabled = false;
        }

    }

    public void OnEndDrag()
    {

        // �Ѽ�ͷ��target��λ
        transform.localPosition = new Vector3(0f, 0f, 0.4f);
        sr.enabled = false;
        lr.enabled = false;
        triangleSR.enabled = false;

    }

    protected bool DragSuccessful()
    {
        return true;
    }
}

