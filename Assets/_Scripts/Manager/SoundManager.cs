using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource worldAudioSource;
    [SerializeField] private List<AudioClip> gameplayClips = new();
    [SerializeField] private AudioClip gameOverClip;
    
    void Start()
    {
        worldAudioSource = GetComponent<AudioSource>();
        
        int index = Random.Range(0, gameplayClips.Count);
        worldAudioSource.clip = gameplayClips[index];
        
    }

    public void OnGameOver()
    {
        worldAudioSource.clip = gameOverClip;
        worldAudioSource.loop = false;
    }
    
}
