using System;
using UnityEngine;

public class MouseEventsArgs : MonoBehaviour
{
    public event Action PlayerUnitSelectEvent;
    public event Action EnemySelectAndAtackEvent;
    public event Action MovePlayerUnitsEvent;

    private void OnMouseDown()
    {
        PlayerUnitSelectEvent?.Invoke();
        if (Main.Instance.gameManager.uniteController.isPlayerSelected)
            EnemySelectAndAtackEvent?.Invoke();

        MovePlayerUnitsEvent?.Invoke();
    }
}
