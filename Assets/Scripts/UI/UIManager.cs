using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("GameOver")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip sound;
    
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    # region GameOver
    public void GameOver()
    {
        gameOverScreen.SetActive(true); 
        SoundManager.Instance.PlaySound(sound);
    }
    
    //* Function to restart the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        Application.Quit(); //* Only works in build
        # if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //* Only works in editor
        # endif
    }
    # endregion

    #region Pause

    public void PauseGame(bool status)
    {
        //* If status is true, the game is paused
        pauseScreen.SetActive(status);
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ChangeSoundVolume()
    {
        SoundManager.Instance.ChangeSoundVolume(0.2f);
    }
        
    public void ChangeMusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(0.2f);
    }
    

    #endregion
}
