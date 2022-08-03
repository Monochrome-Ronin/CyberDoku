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
    
    void Start()
    {
        _settingsButtonGame.onClick.AddListener(openOrCloseMenuGame);
        _backButton.onClick.AddListener(backToMenu);
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
