using UnityEngine;

public class GameManager
{
    public GridController gridController;
    public UniteController uniteController;
    private Camera _mainCamera;
    public int gold;
    public bool isGameStart;
    private GameObject _groupGame;
    private Data _unitData;

    public Ray ray;

    public GameManager()
    {
        _unitData = new Data();
        _groupGame = GameObject.Find("Game").gameObject;
        gridController = new GridController(_groupGame);
        uniteController = new UniteController(_groupGame, _unitData);
        _mainCamera = Camera.main;
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
        if (isGameStart)
        {
            gridController.Update();
            uniteController.Update();
            ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }

    public void BuyAUnit(Enums.UniteType type)
    {
        uniteController.GetUnitCostByUnitType(type);
        if (uniteController.unitsModel.unitCost <= gold)
        {
            gold -= uniteController.unitsModel.unitCost;
            // uniteController.SpawnEnemyUnit(type);
            uniteController.SpawnPlayerUnit(type);
        }
    }

    public void PlayNewRound()
    {
        for (int i = 0; i <= uniteController.playerUnits.Count - 1; i++)
        {
            uniteController.playerUnits[i].isUnitCanMove = true;
        }
        Main.Instance.uiManager.NewRound();
    }
}
