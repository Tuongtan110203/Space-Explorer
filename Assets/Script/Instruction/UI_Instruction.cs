using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Instruction : MonoBehaviour
{
    [SerializeField] private Button BackToSceneMenuGame;

    private void Start()
    {
        BackToSceneMenuGame.onClick.AddListener(() => BackToGameScene());
    }
    public void BackToGameScene()
    {
        SceneManager.LoadScene("GameScene"); 
    }
}
