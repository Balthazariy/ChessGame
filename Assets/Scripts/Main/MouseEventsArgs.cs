using System;
using UnityEngine;

public class MouseEventsArgs : MonoBehaviour
{
    public event Action PlayerUnitSelectEvent;

    private void OnMouseDown()
    {
        PlayerUnitSelectEvent?.Invoke();

    }
}
