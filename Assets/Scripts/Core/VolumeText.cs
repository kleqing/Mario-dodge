using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    private Text txt;
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; //* Sound or Music

    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    
    private void UpdateVolume()
    {
        float volume = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volume.ToString();
    }
 
    private void Update()
    {
        UpdateVolume();
    }
}
 