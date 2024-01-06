using Mono.Cecil;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameStatus
    {
        RUNNING,
        CONSOLE,
        GAMEOVER
    }

    public GameStatus actualStatus { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ActivityHandler activityHandler;

    private int points;
    private int timeLine;
    private const int TIMELINELIMIT = 5;
    private float gameSpeed;
    private int gamesWonInARow = 0;

    public void Start()
    {
        if(Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
        Init();
    }

    private void Init()
    {
        timeLine = TIMELINELIMIT - 1;
        actualStatus = GameStatus.RUNNING;
        gameSpeed = 2f;
        points = 0;
    }

    public void SetGameStatus(GameStatus newStatus)
    {
        actualStatus = newStatus;
    }

    public void SendActivityState(bool isFailed)
    {
        if(isFailed)
        {
            timeLine -= 1;
            if(timeLine <= 0)
                GameOver();
        }
        else
        {
            gamesWonInARow++;
            if(gamesWonInARow >= 2)
            {
                timeLine += 1;
                gamesWonInARow = 0;
            }
            gameSpeed += 0.35f;
            SetGameStatus(GameStatus.RUNNING);
        }
        uiManager.SetUI(timeLine, points);
    }
    [ContextMenu("startsito")]
    public void StartActivity()
    {
        if (actualStatus != GameStatus.RUNNING)
            return;

        uiManager.SetConsoleVisibility(true, () => activityHandler.StartGame(gameSpeed));
        SetGameStatus(GameStatus.CONSOLE);
    }

    public void GameOver()
    {
        if (actualStatus == GameStatus.RUNNING || actualStatus == GameStatus.CONSOLE)
        {
            uiManager.ShowGameOver();
            SetGameStatus(GameStatus.GAMEOVER);
        }
    }

}
