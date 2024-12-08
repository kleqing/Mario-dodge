using System;
using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header ("Firetrap timer")]
    
    [SerializeField] private float damage;
    
    [SerializeField] private float ActivationTime;
    [SerializeField] private float ActiveTime;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isTrigger; //* Trigger the trap
    private bool isActive; //* Active the trap and hurt the player
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator ActivateTrap()
    {
        //* Turn the sprite red to notify when the trap is activated
        isTrigger = true;
        spriteRenderer.color = Color.red;
        
        //* Wait for the activation time, turn on animation and activate the trap
        yield return new WaitForSeconds(ActivationTime);
        spriteRenderer.color = Color.white;
        isActive = true;
        animator.SetBool("Active", true);
        
        //* Wait until the active time is over, then turn off the trap
        yield return new WaitForSeconds(ActiveTime);
        isActive = false;
        isTrigger = false;
        animator.SetBool("Active", true);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isTrigger)
            {
                StartCoroutine(ActivateTrap());
            }

            if (isActive)
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
