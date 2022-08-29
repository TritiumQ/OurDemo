using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//�����л�����
public class SeriablizableEvent
{
    public bool isPass;
    public int eventSign;
    public int id;

    public SeriablizableEvent(GameObject _event)
    {
        EventUI _eventUI = _event.GetComponent<EventUI>();
        isPass = _eventUI.isPass;
        eventSign = _eventUI.eventSign;
        id = _eventUI.id;
    }
}
