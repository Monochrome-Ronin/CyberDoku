using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUiManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _settingsButtonGame;
    [SerializeField] private Button _topScoreButtonGame;

    [Header("Animators")]
    [SerializeField] private Animator _settingsAnimatorGame;
    [SerializeField] private Animator _topScoreAnimatorGame;
    
    void Start()
    {
        _settingsButtonGame.onClick.AddListener(openOrCloseMenuGame);
        _topScoreButtonGame.onClick.AddListener(openOrCloseScoreGame);
        _backButton.onClick.AddListener(backToMenu);
    }

    private void openOrCloseScoreGame()
    {
        if (_topScoreAnimatorGame.GetBool("isOpen") == false)
        {
            _topScoreAnimatorGame.SetBool("isOpen", true);
        }
        else
        {
            _topScoreAnimatorGame.SetBool("isOpen", false);
        }
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



}
