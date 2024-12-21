using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audio;
    private AudioSource musicSource;

    private void Awake()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

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
        
        //* Load the current volume from the player prefs
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        audio.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "SoundVolume", _change, audio);
    }
    
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "MusicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float _change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1); //* Load the current volume from the player prefs
        currentVolume += _change;

        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        float newVolume = baseVolume * currentVolume;
        source.volume = newVolume;
        
        //* Save the current volume to the player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
