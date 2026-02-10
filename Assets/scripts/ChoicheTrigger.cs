using UnityEngine;

public class ChoiceTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public bool isThisLeftDoor;

    private static bool? winningDoorIsLeft;
    private static bool choiceMade = false;
    private static bool needsReinitialization = true;

    private Renderer doorRenderer;
    private Material doorMaterial;

    void Awake()
    {
        doorRenderer = GetComponent<Renderer>();

        if (doorMaterial == null)
        {
            doorMaterial = new Material(doorRenderer.material);
            doorRenderer.material = doorMaterial;
        }
        InitializeChoice();
    }

    void Update()
    {
        if (needsReinitialization)
            InitializeChoice();
    }

    void InitializeChoice()
    {
        if (winningDoorIsLeft == null || needsReinitialization)
        {
            winningDoorIsLeft = Random.Range(0, 2) == 0;
            needsReinitialization = false;
            Debug.Log("Correct door: " + (winningDoorIsLeft.Value ? "LEFT" : "RIGHT"));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !choiceMade)
        {

            if (!winningDoorIsLeft.HasValue)
            {
                InitializeChoice();
                return;
            }

            choiceMade = true;

            bool playerWins = (isThisLeftDoor == winningDoorIsLeft.Value);
            ChangeDoorColor(playerWins);

            Debug.Log("Player chose: " + (isThisLeftDoor ? "LEFT" : "RIGHT"));
            Debug.Log("Result: " + (playerWins ? "WIN" : "LOSE"));

            if (gameManager != null)
                gameManager.ProcessChoice(playerWins);
        }
    }

    void ChangeDoorColor(bool isWin)
    {
        if (doorMaterial != null)
            doorMaterial.color = isWin ? Color.green : Color.red;
        Invoke("ResetDoorVisual",1);

    }

    public void ResetDoorVisual()
    {
        if (doorMaterial != null)
        doorMaterial.color = Color.black;
    }

    public static void ResetChoice()
    {
        winningDoorIsLeft = null;
        choiceMade = false;
        needsReinitialization = true;
    }
}




