using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;
    public GameObject healthCanvas;
    public GameObject scoreCanvas;

    [Header("Button")]
    public Button playButton;
    public Button quitButton;
    public Button reStartButton;

    private bool isPaused = false;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 0;  // Pause the game at the start

        menuPanel.SetActive(true);
        healthCanvas.SetActive(false);
        scoreCanvas.SetActive(false);

        reStartButton.gameObject.SetActive(false);

        playButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        reStartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGameOver) return;


            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }

    public void PauseGame()
    {
        menuPanel.SetActive(true);
        healthCanvas.SetActive(false);
        scoreCanvas.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ShowScore()
    {
        menuPanel.SetActive(true);
        healthCanvas.SetActive(false);
        scoreCanvas.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowScoreResult()
    {
        isGameOver = true;

        playButton.gameObject.SetActive(false);
        reStartButton.gameObject.SetActive(true);

        menuPanel.SetActive(true);
        healthCanvas.SetActive(false);
        scoreCanvas.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        GameObject playerParent = GameObject.Find("PlayerParent");
        if (playerParent != null)
        {
            Player player = playerParent.GetComponentInChildren<Player>(true);
            if (player != null)
            {
                player.gameObject.SetActive(true);
                player.RestartGame();

                playButton.gameObject.SetActive(true);
                reStartButton.gameObject.SetActive(false);

                isGameOver = false;
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false);
        healthCanvas.SetActive(true);
        scoreCanvas.SetActive(true);

        Time.timeScale = 1;  // Resume the game
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();  // Quit the application
    }
}
