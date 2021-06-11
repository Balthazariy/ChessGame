using System;
using UnityEngine;

public class EventsArgs : MonoBehaviour
{
    public event Action mouseButtonDownEvent;
    public event Action mouseButtonUpEvent;

    private void OnMouseDown()
    {
        mouseButtonDownEvent?.Invoke();
    }

    private void OnMouseUp()
    {
        mouseButtonUpEvent?.Invoke();
    }


}
