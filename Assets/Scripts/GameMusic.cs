using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource _audioSource;
    private int _trackDelay = 0;
    private UiManager uiManager;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(MusicPlay());
        uiManager = GetComponent<UiManager>();
    }
    private void Update()
    {
        MusicPlay();
    }
    IEnumerator MusicPlay()
    {
        int musicIndex = 0;
        while(audioClips.Length > 0)
        {
            float waitTime = audioClips[musicIndex].length + _trackDelay;
            _audioSource.PlayOneShot(audioClips[musicIndex]);
            musicIndex++;
            if(musicIndex>=audioClips.Length)
            {
                musicIndex = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }           
        
    }

    private void StopMusic()
    {
       if(uiManager.isMuted == true)
       {
            _audioSource.Pause();
       }
    }
}
