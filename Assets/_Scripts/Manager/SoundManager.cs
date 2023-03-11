using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource worldAudioSource;
    [SerializeField] private List<AudioClip> gameplayClips = new();
    [SerializeField] private AudioClip gameOverClip;
    
    void Start()
    {
        int index = Random.Range(0, gameplayClips.Count);
        worldAudioSource.clip = gameplayClips[index];
        
    }

    void OnGameOver()
    {
        worldAudioSource.clip = gameOverClip;
        worldAudioSource.loop = false;
    }
    
}
