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

    [Header("Music")]
    [SerializeField] private Toggle _musicToggle;
    private bool isMuted;
    private AudioSource _audioSource;
    void Start()
    {
        _settingsButtonGame.onClick.AddListener(openOrCloseMenuGame);
        _topScoreButtonGame.onClick.AddListener(openOrCloseScoreGame);
        _backButton.onClick.AddListener(backToMenu);

        _audioSource = gameObject.GetComponent<AudioSource>();
        _musicToggle.onValueChanged.AddListener(this.offOnMusic);

        if(!PlayerPrefs.HasKey("Muted")){
            PlayerPrefs.SetInt("Muted", 1);
            isMuted = PlayerPrefs.GetInt("Muted") == 1;
            _audioSource.mute = isMuted;
            _musicToggle.isOn = isMuted;
        }else{
            isMuted = PlayerPrefs.GetInt("Muted") == 1;
            _audioSource.mute = isMuted;
            _musicToggle.isOn = isMuted;
        }
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

    private void offOnMusic(bool isOn)
    {
        isMuted = isOn;
        _audioSource.mute = isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    private void backToMenu()
    {
        SceneManager.LoadScene(0);
    }



}
