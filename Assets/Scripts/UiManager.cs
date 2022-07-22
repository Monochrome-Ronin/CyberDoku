using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Animator _settingsAnimator;
    [SerializeField] private Button _topScoreButton;
    [SerializeField] private Animator _topScoreAnimator;
    void Start()
    {
        _settingsButton.onClick.AddListener(openOrCloseMenu);
        _topScoreButton.onClick.AddListener(openOrCloseScore);
        _startButton.onClick.AddListener(startGame);
    }

    private void openOrCloseScore()
    {
        if (_topScoreAnimator.GetBool("isOpen") == false)
        {
            _topScoreAnimator.SetBool("isOpen", true);
        }
        else
        {
            _topScoreAnimator.SetBool("isOpen", false);
        }
    }
    private void openOrCloseMenu()
    {
        if(_settingsAnimator.GetBool("isOpen") == false)
        {
            _settingsAnimator.SetBool("isOpen", true);
        }
        else
        {
            _settingsAnimator.SetBool("isOpen", false);
        }
    }

    private void startGame()
    {
        SceneManager.LoadScene(1);
    }



}
