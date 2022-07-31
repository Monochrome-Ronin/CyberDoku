using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Music")]
    private bool isMuted;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Dropdown _musicDropDown;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    // Start is called before the first frame update
    void Start()
    {
        _musicToggle.onValueChanged.AddListener(this.OffOnMusic);
        _musicDropDown.onValueChanged.AddListener(SwapMusic);
        _volumeSlider.onValueChanged.AddListener(VolumeChange);

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

        if(!PlayerPrefs.HasKey("MusicIndex")){
            PlayerPrefs.SetInt("MusicIndex", 0);
            _musicDropDown.value = PlayerPrefs.GetInt("MusicIndex");
            _audioSource.clip = _audioClips[PlayerPrefs.GetInt("MusicIndex")];
            _audioSource.Play();
        }else{
            _musicDropDown.value = PlayerPrefs.GetInt("MusicIndex");
            _audioSource.clip = _audioClips[PlayerPrefs.GetInt("MusicIndex")];
            _audioSource.Play();
        }

        if(!PlayerPrefs.HasKey("Volume")){
            PlayerPrefs.SetFloat("Volume", 0.5f);
            _audioSource.volume = PlayerPrefs.GetFloat("Volume");
            _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }else{
            _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    private void OffOnMusic(bool isOn)
    {
        isMuted = isOn;
        _audioSource.mute = isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    private void SwapMusic(int musicIndex){
        if(_musicDropDown.value == 0) musicIndex = 0;
        if(_musicDropDown.value == 1) musicIndex = 1;
        if(_musicDropDown.value == 2) musicIndex = 2;
        _audioSource.clip = _audioClips[musicIndex];
        _audioSource.Play();
        PlayerPrefs.SetInt("MusicIndex", musicIndex);
    }

    private void VolumeChange(float vol){
        _audioSource.volume = _volumeSlider.value;
        vol = _audioSource.volume;
        PlayerPrefs.SetFloat("Volume", vol);
    }
}
