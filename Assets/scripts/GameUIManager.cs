using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
   
    public GameManager gameManager;

    
    public GameObject startPanel;
    public GameObject mainPanel;
    public GameObject gameOverPanel;


    public TMP_Text balanceText;
    public TMP_Text betText;
    public TMP_Text timerText;
    


    public Button startButton;
    public Button increaseBetButton;
    public Button decreaseBetButton; 
    public Button stopGameButton;
    public Button restartButton;
    public Button quitButton;

    public float timer = 5;
    void Start()
    {
        timerText.text = timer.ToString();

        startButton.onClick.AddListener(OnStartClicked);
        increaseBetButton.onClick.AddListener(OnIncreaseBetClicked);
        decreaseBetButton.onClick.AddListener(OnDecreaseBetClicked);
        stopGameButton.onClick.AddListener(OnStopClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        ShowStartScreen();
    }

    void Update()
    {
        if (gameManager == null) return;

        balanceText.text = $" Balance: {gameManager.GetBalance()}";
        betText.text = $"Bet: {gameManager.GetCurrentBet()}";
    }

    private void OnStartClicked()
    {
        gameManager.ResetGame();
        ShowMainScreen();
    }

    private void OnIncreaseBetClicked()
    {
        gameManager.IncreaseBet();
    }

    private void OnDecreaseBetClicked()
    {
        gameManager.DecreaseBet();
    }

    private void OnStopClicked()
    {
        gameManager.StopGame();
        ShowGameOverScreen();
    }

    private void OnRestartClicked()
    {
        gameManager.ResetGame();
        ShowMainScreen();
    }

    public void ShowStartScreen()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowMainScreen()
    {
        startPanel.SetActive(false);
        mainPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameManager.StartCountdown(timer);
    }

    public void GamePlayScreen()
    {
        startPanel.SetActive(false);
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        startPanel.SetActive(false);
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(true);

   //     finalBalanceText.text = $"Final Balance: {gameManager.GetBalance()}";
    }
    public void UpdateTimer(float value)
    {
        if (timerText != null)
            timerText.text = $" {value:0}";
    }

    private void OnQuitClicked() => Application.Quit();
    
}

