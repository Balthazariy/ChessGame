using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private GameManager _gameManager;

    private Canvas _canvas;
    private Button _openMarketButton, _closeMarketButton, _finishPreparingButton, _spawnNewEnemyButton, _buyPawnButton, _buyKnightButton, _buyBishopButton, _buyRookButton, _buyQueenButton;
    private Text _goldText, _pawnCost, _knightCost, _bishopCost, _rookCost, _queenCost;
    private GameObject _groupMarket;
    private Data _data;

    public UIManager()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _groupMarket = _canvas.transform.Find("Group_Market").gameObject;
        _goldText = _groupMarket.transform.Find("Text_GoldAmount").GetComponent<Text>();

        _openMarketButton = _canvas.transform.Find("Button_Market").GetComponent<Button>();
        _closeMarketButton = _canvas.transform.Find("Button_CloseMarket").GetComponent<Button>();
        _finishPreparingButton = _canvas.transform.Find("Button_FinishPreparing").GetComponent<Button>();
        _spawnNewEnemyButton = _canvas.transform.Find("Button_SpawnNewEnemy").GetComponent<Button>();

        _buyPawnButton = _groupMarket.transform.Find("Group_Pawn/Button_BuyPawn").GetComponent<Button>();
        _buyKnightButton = _groupMarket.transform.Find("Group_Knight/Button_BuyKnight").GetComponent<Button>();
        _buyBishopButton = _groupMarket.transform.Find("Group_Bishop/Button_BuyBishop").GetComponent<Button>();
        _buyRookButton = _groupMarket.transform.Find("Group_Rook/Button_BuyRook").GetComponent<Button>();
        _buyQueenButton = _groupMarket.transform.Find("Group_Queen/Button_BuyQueen").GetComponent<Button>();

        _pawnCost = _buyPawnButton.transform.Find("Text_PawnCost").GetComponent<Text>();
        _knightCost = _buyKnightButton.transform.Find("Text_KnightCost").GetComponent<Text>();
        _bishopCost = _buyBishopButton.transform.Find("Text_BishopCost").GetComponent<Text>();
        _rookCost = _buyRookButton.transform.Find("Text_RookCost").GetComponent<Text>();
        _queenCost = _buyQueenButton.transform.Find("Text_QueenCost").GetComponent<Text>();

        _openMarketButton.onClick.AddListener(OpenMarket);
        _closeMarketButton.onClick.AddListener(CloseMarket);
        _finishPreparingButton.onClick.AddListener(StartGame);
        _spawnNewEnemyButton.onClick.AddListener(SpawnNewEnemy);

        _buyPawnButton.onClick.AddListener(BuyPawn);
        _buyKnightButton.onClick.AddListener(BuyKnight);
        _buyBishopButton.onClick.AddListener(BuyBishop);
        _buyRookButton.onClick.AddListener(BuyRook);
        _buyQueenButton.onClick.AddListener(BuyQueen);
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _data = _gameManager.unitData;
        _groupMarket.SetActive(false);
        _closeMarketButton.gameObject.SetActive(false);
        _finishPreparingButton.gameObject.SetActive(false);
        _openMarketButton.gameObject.SetActive(true);
        _spawnNewEnemyButton.gameObject.SetActive(false);

        _pawnCost.text = "Cost: " + _gameManager.uniteController.GetUnitCostByUnitType(Enums.UniteType.Pawn).ToString() + "g";
        _knightCost.text = "Cost: " + _gameManager.uniteController.GetUnitCostByUnitType(Enums.UniteType.Knight).ToString() + "g";
        _bishopCost.text = "Cost: " + _gameManager.uniteController.GetUnitCostByUnitType(Enums.UniteType.Bishop).ToString() + "g";
        _rookCost.text = "Cost: " + _gameManager.uniteController.GetUnitCostByUnitType(Enums.UniteType.Rook).ToString() + "g";
        _queenCost.text = "Cost: " + _gameManager.uniteController.GetUnitCostByUnitType(Enums.UniteType.Queen).ToString() + "g";

        _gameManager.uniteController.newRoundEvent += NewRound;
    }

    public void Update()
    {
        _goldText.text = "Gold: " + _gameManager.gold.ToString();
    }

    private void OpenMarket()
    {
        _groupMarket.SetActive(true);
        _openMarketButton.gameObject.SetActive(false);
        _closeMarketButton.gameObject.SetActive(true);
    }

    private void CloseMarket()
    {
        _groupMarket.SetActive(false);
        _closeMarketButton.gameObject.SetActive(false);
        _finishPreparingButton.gameObject.SetActive(true);
        _gameManager.isGameStart = true;
        _spawnNewEnemyButton.gameObject.SetActive(true);
    }

    private void StartGame()
    {
        _finishPreparingButton.gameObject.SetActive(false);
        _gameManager.uniteController.PlayNewRound();
    }

    private void BuyPawn()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Pawn);
    }

    private void BuyKnight()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Knight);
    }

    private void BuyBishop()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Bishop);
    }

    private void BuyRook()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Rook);
    }

    private void BuyQueen()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Queen);
    }

    private void NewRound()
    {
        _finishPreparingButton.gameObject.SetActive(true);
        Debug.Log("New Round");
    }

    private void SpawnNewEnemy()
    {
        _gameManager.uniteController.SpawnEnemyUnit(Enums.UniteType.Queen);
    }
}
