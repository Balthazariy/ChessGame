using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GameObject _game;
    public GridController gridController;
    public UniteController uniteController;

    public int gold;

    public bool isGameStart;

    public GameManager()
    {
        _game = GameObject.Find("Game").gameObject;
        
        gridController = new GridController(_game);
        uniteController = new UniteController();
    }

    public void Start()
    {
        isGameStart = false;
        gold = 20;

        gridController.Start();
        uniteController.Start();
    }

    public void Update()
    {
        gridController.Update();
    }

    public void BuyAUnit(Enums.UniteType unite)
    {
        uniteController.unitsModel.SetAUniteType(unite);
    }
}
