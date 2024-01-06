using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIManager uiManager;

    private int points;
    private int timeLine;
    private float gameSpeed;

    private bool isInGame = false;
    private bool gameEnded = false;

    public void SendActivityState(bool isFailed)
    {
        uiManager.SetUI(timeLine);
    }

    public void StartActivity()
    {
        if (isInGame)
            return;

        uiManager.SetConsoleVisibility(true);
        isInGame = true;
    }

    public void GameOver()
    {
        if(gameEnded) 
            return;

        uiManager.ShowGameOver();
        gameEnded = true;
    }

}
