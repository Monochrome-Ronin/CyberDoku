using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Animator _settingsAnimator;
    void Start()
    {
        _settingsButton.onClick.AddListener(openOrCloseMenu);
        _startButton.onClick.AddListener(startGame);
    }

    private void openOrCloseMenu(){
        if(_settingsAnimator.GetBool("isOpen") == false){
            _settingsAnimator.SetBool("isOpen", true);
        }else{
            _settingsAnimator.SetBool("isOpen", false);
        }
    }

    private void startGame(){
        SceneManager.LoadScene(1);
    }



}
