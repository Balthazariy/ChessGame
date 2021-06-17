using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public GameManager gameManager;
    public UIManager uiManager;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        GetManager();
        gameManager.Start();
        uiManager.Start();
    }

    private void Update()
    {
        gameManager.Update();
        uiManager.Update();
    }

    private void GetManager()
    {
        gameManager = new GameManager();
        uiManager = new UIManager();
    }
}
