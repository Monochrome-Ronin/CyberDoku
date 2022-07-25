using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _topScoreButton;

    [Header("Animators")]
    [SerializeField] private Animator _settingsAnimator;
    [SerializeField] private Animator _topScoreAnimator;

    [Header("Music")]
    private AudioSource _audioSource;
    private bool isMuted;
    [SerializeField] private Toggle _musicToggle;

    void Start()
    {
        _settingsButton.onClick.AddListener(openOrCloseMenu);
        _topScoreButton.onClick.AddListener(openOrCloseScore);
        _startButton.onClick.AddListener(startGame);

        _audioSource = gameObject.GetComponent<AudioSource>();
        _musicToggle.onValueChanged.AddListener(this.offOnMusic);

        if(!PlayerPrefs.HasKey("Muted")){
            Debug.Log("Ключа нету");
            PlayerPrefs.SetInt("Muted", 1);
            isMuted = PlayerPrefs.GetInt("Muted") == 1;
            _audioSource.mute = isMuted;
            _musicToggle.isOn = isMuted;
        }else{
            Debug.Log("Ключ есть " + PlayerPrefs.GetInt("Muted"));
            isMuted = PlayerPrefs.GetInt("Muted") == 1;
            _audioSource.mute = isMuted;
            _musicToggle.isOn = isMuted;
        }
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

    private void offOnMusic(bool isOn)
    {
        isMuted = isOn;
        _audioSource.mute = isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    private void startGame()
    {
        SceneManager.LoadScene(1);
    }



}
