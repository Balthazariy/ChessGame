using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEventArgs : MonoBehaviour
{
    public event Action IsUnitDraggingEvent;

    private void OnMouseDrag()
    {
        IsUnitDraggingEvent?.Invoke();
    }
}
