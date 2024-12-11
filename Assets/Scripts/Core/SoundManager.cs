using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audio;

    private void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();

        //* Keep the sound manager alive when go to new scene
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        
        //* Destroy duplicate sound manager
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        audio.PlayOneShot(_sound);
    }
}
