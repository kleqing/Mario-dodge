using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollect : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private AudioClip collectSound; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundManager.Instance.PlaySound(collectSound);
            other.GetComponent<Health>().AddHealth(health);
            gameObject.SetActive(false);
        }
    }
}
