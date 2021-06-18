using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventsArgs : MonoBehaviour
{
    public event Action PlayerUnitSelectEvent;
    public event Action EnemyUnitSelectEvent;

    private void OnMouseDown()
    {
        PlayerUnitSelectEvent?.Invoke();

        if (Main.Instance.gameManager.uniteController.isPlayerSelected)
            EnemyUnitSelectEvent?.Invoke();
    }
}
