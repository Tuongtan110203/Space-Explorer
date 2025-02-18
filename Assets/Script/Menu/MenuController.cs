using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;
    public GameObject healthCanvas;
    public GameObject scoreCanvas;
    public GameObject pauseGameObject;
    public GameObject settingGameObject;
    public GameObject RestartObject;
    public GameObject settingPanel;

    [Header("Button")]
    public Button playButton;
    public Button quitButton;
    public Button reStartButton;
    public Button instruction;
    private Button pauseGame;
    private Button setting;
    private Button restart;

    private bool isPaused = false;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 0;

        menuPanel.SetActive(true);
        healthCanvas.SetActive(true);
        scoreCanvas.SetActive(true);
        pauseGameObject.SetActive(true);
        RestartObject.SetActive(false);
        reStartButton.gameObject.SetActive(false);
        settingPanel.SetActive(false);

        playButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        reStartButton.onClick.AddListener(RestartGame);
        instruction.onClick.AddListener(OpenInstructionScene);

        pauseGame = pauseGameObject.GetComponentInChildren<Button>();
        pauseGame.onClick.AddListener(PauseGame);

        setting = settingGameObject.GetComponentInChildren<Button>();
        setting.onClick.AddListener(OpenSetting);
        Button backButton = settingPanel.GetComponentInChildren<Button>();
        backButton.onClick.AddListener(CloseSetting);


        restart = RestartObject.GetComponentInChildren<Button>();
        restart.onClick.AddListener(RestartGame);
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

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        menuPanel.SetActive(false);
        RestartObject.SetActive(false);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        menuPanel.SetActive(true);
        RestartObject.SetActive(true);
    }

    public void OpenInstructionScene()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void PauseGame()
    {
        menuPanel.SetActive(true);
        healthCanvas.SetActive(true);
        scoreCanvas.SetActive(true);
        RestartObject.SetActive(true);

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
                RestartObject.SetActive(false);
            }
        }
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false);
        healthCanvas.SetActive(true);
        scoreCanvas.SetActive(true);
        RestartObject.SetActive(false);


        Time.timeScale = 1;  // Resume the game
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();  // Quit the application
    }
}
