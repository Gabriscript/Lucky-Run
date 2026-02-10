using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int startingBalance = 90;
    public int startingBet = 10;
    

    [SerializeField] PlayerMotor playerMotor;
    [SerializeField] private GameUIManager uiManager;

    public ChoiceTrigger leftDoor;
    public ChoiceTrigger rightDoor;
    
    int currentBalance;
    int currentBet;
    

    private void Start()
    {
        playerMotor.isMoving = false;
        playerMotor.speed = 0f;
   
    }
    public void ProcessChoice(bool isCorrect)
    {
        playerMotor.speed = 0f;
        playerMotor.isMoving = false;

        if (isCorrect) 
            currentBalance += currentBet * 2;


         StartCoroutine(PrepareNextRoundDelayed(1f));
    }
    
    private IEnumerator PrepareNextRoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        PrepareNextRound();
    }

    private void PrepareNextRound()
    {
        if (currentBalance <= 0)
        {
            EndGame();
            return;
        }
        currentBet = startingBet;

        ChoiceTrigger.ResetChoice();


        playerMotor.speed = 0f;
        playerMotor.isMoving = false;
        playerMotor.ResetPosition();

        uiManager.ShowMainScreen();
    }




    public void ResetGame()
    {

        currentBalance = startingBalance;
        currentBet = startingBet;
        Time.timeScale = 1f;

        Debug.Log($"New Game Started. Balance: {currentBalance}, Bet: {currentBet}");

        if (playerMotor != null)
        {
            playerMotor.speed = 0f;
            playerMotor.isMoving = false;
            playerMotor.ResetPosition();

        }

        ChoiceTrigger.ResetChoice();
    }

    public void IncreaseBet()
    {
        if (currentBalance >= 10)
        {
            currentBet += 10;
            currentBalance -= 10;
        }

        Debug.Log($"Bet increased to: {currentBet}, Balance: {currentBalance}");
    }

    public void DecreaseBet()
    {
        if (currentBet > 0)
        {
            currentBet -= 10;
            currentBalance += 10;
        }

        Debug.Log($"Bet decreased to: {currentBet}, Balance: {currentBalance}");
    }


    public void StopGame()
    {
         Debug.Log($"Your final balance: {GetBalance()} ");
        if (playerMotor != null)
        {
            playerMotor.speed = 0f;
            playerMotor.isMoving = false;
        }
    }



    public void StartCountdown(float seconds)
    {
        StartCoroutine(CountdownRoutine(seconds));
    }

    private IEnumerator CountdownRoutine(float seconds)
    {
        yield return new WaitForEndOfFrame();

        float timer = seconds;
        uiManager.UpdateTimer(timer);

        while (timer > 0)
        {
            uiManager.UpdateTimer(timer);
            yield return new WaitForSeconds(1f);
            timer--;
        }

        uiManager.UpdateTimer(0);
        uiManager.GamePlayScreen();
        playerMotor.isMoving = true;
        playerMotor.speed = 15f;
    }

    private void EndGame()
    {
        Debug.Log($"Game Over! Final Balance: {currentBalance}");
        playerMotor.speed = 0f;
        playerMotor.isMoving = false;

        if (uiManager != null)
            uiManager.ShowGameOverScreen();
    }

    public int GetBalance() => currentBalance;
    public int GetCurrentBet() => currentBet;
}
