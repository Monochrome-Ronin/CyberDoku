using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUiManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _settingsButtonGame;

    [Header("Animators")]
    [SerializeField] private Animator _settingsAnimatorGame;

    [Header("Game Over")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;

    
    void Start()
    {
        _settingsButtonGame.onClick.AddListener(openOrCloseMenuGame);
        _backButton.onClick.AddListener(backToMenu);

        //Game Over buttons
        _restartButton.onClick.AddListener(RestartGame);
        _backToMenuButton.onClick.AddListener(BackToMenuGameOver);
    }

    private void openOrCloseMenuGame()
    {
        if (_settingsAnimatorGame.GetBool("isOpen") == false)
        {
            _settingsAnimatorGame.SetBool("isOpen", true);
        }
        else
        {
            _settingsAnimatorGame.SetBool("isOpen", false);
        }
    }

    private void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void RestartGame(){
        SceneManager.LoadScene(1);
    }

    private void BackToMenuGameOver(){
        SceneManager.LoadScene(0);
    }

}
