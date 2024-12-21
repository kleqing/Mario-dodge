using System;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    private Transform currentCheckpoint;
    private Health health;
    private UIManager uiManager;

    private void Awake()
    {
        health = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //* Check spawm point is exist
        if (currentCheckpoint == null)
        {
            //* Show game over screen
            uiManager.GameOver();
            return;
        }
        health.Respawn();
        transform.position = currentCheckpoint.position;
        //* Restore health and animation
        
        //* Move camera to player
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    
    //* Active checkpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Checkpoint")
        {
            currentCheckpoint = other.transform; //* Set current checkpoint to the one the player just touched
            SoundManager.Instance.PlaySound(sound);
            other.GetComponent<Collider2D>().enabled = false; //* Disable the collider of the checkpoint so the player can't touch it again
            other.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
